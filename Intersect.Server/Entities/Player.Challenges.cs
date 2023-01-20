using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using Intersect.Network.Packets.Server;
using Intersect.Server.Core;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities.Events;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Newtonsoft.Json;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public List<ChallengeInstance> Challenges { get; set; } = new List<ChallengeInstance>();

        public List<WeaponMasteryInstance> WeaponMasteries { get; set; } = new List<WeaponMasteryInstance>();

        [NotMapped, JsonIgnore]
        public List<WeaponMasteryInstance> ActiveMasteries => WeaponMasteries.Where(mastery => mastery.IsActive).ToList();

        public bool TryGetChallenge(Guid id, out ChallengeInstance challenge)
        {
            challenge = Challenges.Find(c => c.ChallengeId == id);
            return challenge != default;
        }

        public bool TryGetMastery(Guid weaponTypeId, out WeaponMasteryInstance mastery)
        {
            mastery = WeaponMasteries.Find(c => c.WeaponTypeId == weaponTypeId);
            return mastery != default;
        }

        public void SetMasteryProgress()
        {
            var weapon = GetEquippedWeapon();
            if (weapon == default)
            {
                return;
            }

            DeactivateAllMasteries();

            // Instantiate new mastery tracks/challenges in response to this change
            List<string> newChallenges = new List<string>();
            foreach (var weaponType in weapon.WeaponTypes)
            {
                if (!TryGetMastery(weaponType, out var mastery))
                {
                    WeaponMasteries.Add(new WeaponMasteryInstance(Id, weaponType, 0, true));
                }

                mastery.IsActive = true;

                if (!mastery.TryGetCurrentUnlock(out var unlock))
                {
                    continue;
                }
                
                if (mastery.ExpRemaining < unlock.RequiredExp)
                {
                    continue;
                }

                // The rest of this is done in case of the event the underlying WeaponTypeDescriptor changes - we want
                // the players mastery progress to update if need be
                if (!mastery.TryGetCurrentChallenges(out var currentChallenges))
                {
                    LevelUpMastery(mastery);
                    continue;
                }
                if (ChallengesComplete(currentChallenges))
                {
                    LevelUpMastery(mastery);
                }
            }
            SendChallengeUpdate(false, newChallenges);
        }

        public void ProgressMastery(long exp, Guid weaponType)
        {
            if (!TryGetMastery(weaponType, out var mastery))
            {
                return;
            }
            mastery.IsActive = true;

            // Are we done with this mastery track?
            var maxLevel = mastery.WeaponType.MaxLevel;
            if (mastery.Level >= maxLevel)
            {
                if (mastery.Level > maxLevel)
                {
                    mastery.Level = maxLevel;
                }
                
                return;
            }

            // First, attempt to give EXP to the specified mastery
            if (TryGainMasteryExp(exp, mastery))
            {
                return;
            }

            // Otherwise, do we have any challenges that need completing?
            if (!mastery.TryGetCurrentChallenges(out var currentChallenges))
            {
                // No challenges? Well, level up!
                LevelUpMastery(mastery);
            }

            // Make sure our active challenges are up to date and, if not, alert the player
            List<string> newChallenges = new List<string>();
            foreach (var challengeId in currentChallenges)
            {
                if (!TryAddNewChallenge(challengeId))
                {
                    continue;
                }
                newChallenges.Add(ChallengeDescriptor.GetName(challengeId));
            }
            SendChallengeUpdate(false, newChallenges);

            // If we're not done yet, then we can't level up yet!
            if (!ChallengesComplete(currentChallenges))
            {
                return;
            }

            LevelUpMastery(mastery);
        }

        public void LevelUpMastery(WeaponMasteryInstance mastery)
        {
            if (mastery == null)
            {
                return;
            }

            var weaponType = mastery.WeaponType;
            if (weaponType == default)
            {
#if DEBUG
                throw new InvalidOperationException($"No valid weapon type found for mastery attempting level up: {mastery.WeaponTypeId}");
#else
                return;
#endif
            }

            // We're already maxed
            if (mastery.Level == weaponType.MaxLevel)
            {
                Logging.Log.Debug($"Attempted to level up a max level mastery for {Name} in mastery {weaponType.Name}");
                return;
            }

            mastery.Level = MathHelper.Clamp(mastery.Level + 1, 0, mastery.WeaponType?.MaxLevel ?? 0);
            if (mastery.Level == weaponType.MaxLevel)
            {
                SendMasteryUpdate(true, weaponType.Name);
                return;
            }
            else
            {
                SendMasteryUpdate(false, weaponType.Name);
            }

            if (!mastery.TryGetCurrentUnlock(out var unlock))
            {
                mastery.ExpRemaining = -1;
            }

            mastery.ExpRemaining = 0;
        }

        public bool TryGainMasteryExp(long exp, WeaponMasteryInstance mastery)
        {
            var requiredExp = 0;
            if (mastery.TryGetCurrentUnlock(out var unlock))
            {
                requiredExp = unlock.RequiredExp;
            }
            if (mastery == null || mastery.ExpRemaining >= requiredExp)
            {
                mastery.ExpRemaining = requiredExp;
                return false;
            }

            mastery.ExpRemaining += exp;

            if (mastery.ExpRemaining < requiredExp)
            {
                return true;
            }

            // We have reached our EXP threshold, and should continue progress processing
            mastery.ExpRemaining = unlock.RequiredExp;
            return false;
        }

        public bool TryAddNewChallenge(Guid challengeId)
        {
            if (TryGetChallenge(challengeId, out _))
            {
                return false;
            }

            var challenge = new ChallengeInstance(Id, challengeId, false, 0, true);
            Challenges.Add(challenge);
            return true;
        }

        public bool ChallengesComplete(List<Guid> challengeIds)
        {
            return challengeIds.All((challengeId) =>
            {
                if (!TryGetChallenge(challengeId, out var challenge))
                {
                    if (!TryAddNewChallenge(challengeId))
                    {
                        return true;
                    }
                    SendChallengeUpdate(false, ChallengeDescriptor.GetName(challengeId));
                    return false;
                }

                return challenge.Complete;
            });
        }

        public void DeactivateAllMasteries()
        {
            foreach (var mastery in WeaponMasteries.Where(m => m.IsActive))
            {
                mastery.IsActive = false;
            }
        }
    }
}
