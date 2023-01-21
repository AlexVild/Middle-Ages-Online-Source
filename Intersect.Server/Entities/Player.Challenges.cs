using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.GameObjects;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Newtonsoft.Json;

namespace Intersect.Server.Entities
{
    public class ChallengeProgress
    {
        public Guid ChallengeId { get; set; }
        public ChallengeDescriptor Descriptor { get; set; }
        public ChallengeInstance Instance { get; set; }
        public ChallengeType Type => Descriptor.Type;
        
        private int _reps { get; set; }
        public int Reps
        {
            get => _reps;
            set
            {
                _reps = value;
                if (RepsChanged != null) RepsChanged(value, Descriptor.Reps);
            }
        }

        private int _sets { get; set; }
        public int Sets 
        {
            get => _sets;
            set
            {
                _sets = value;
                if (SetsChanged != null) SetsChanged(value, Descriptor.Sets);
            }
        }

        int NumParam { get; set; }
        Guid IdParam { get; set; }
        Player Player { get; set; }

        public delegate void ChallengeProgressUpdate(int updateVal, int requiredVal);
        public event ChallengeProgressUpdate SetsChanged;
        public event ChallengeProgressUpdate RepsChanged;

        public ChallengeProgress(ChallengeInstance instance, Player player)
        {
            Instance = instance;
            ChallengeId = instance.ChallengeId;
            Descriptor = ChallengeDescriptor.Get(ChallengeId);
            Player = player;

            if (Descriptor == null)
            {
#if DEBUG
                throw new ArgumentNullException(nameof(Descriptor));
#else
                return;
#endif
            }

            Reps = 0;
            NumParam = Descriptor.Param;
            IdParam = Descriptor.ChallengeParamId;
            
            Sets = Instance.Progress;

            if (Sets == Descriptor.Sets)
            {
                Instance.Complete = true;
            }

            RepsChanged += ChallengeProgress_RepsChanged;
            SetsChanged += ChallengeProgress_SetsChanged;
        }

        private void ChallengeProgress_RepsChanged(int reps, int required)
        {
            if (Instance.Complete || reps < Descriptor.Reps)
            {
                return;
            }

            Sets++;
        }

        private void ChallengeProgress_SetsChanged(int sets, int required)
        {
            if (Instance.Complete)
            {
                return;
            }

            Instance.Progress++;

            RepsChanged -= ChallengeProgress_RepsChanged;
            Reps = 0;
            RepsChanged += ChallengeProgress_RepsChanged;

            // If we're not done yet, inform the player of their new progress
            if (Sets < Descriptor.Sets)
            {
                PacketSender.SendChatMsg(Player,
                    Strings.Player.ChallengeProgress.ToString(Descriptor?.Name ?? "NOT FOUND", sets, required),
                    Enums.ChatMessageType.Experience,
                    sendToast: true);
                return;
            }

            // Otherwise, mark this challenge as complete, which will allow the weapon mastery track to progress on the next
            // ProgressMastery() call
            Instance.Progress = Descriptor.Sets;
            Instance.Complete = true;
        }
    }

    public partial class Player : AttackingEntity
    {
        [NotMapped, JsonIgnore]
        public List<ChallengeProgress> ChallengesInProgress { get; set; } = new List<ChallengeProgress>();

        public List<ChallengeInstance> Challenges { get; set; } = new List<ChallengeInstance>();

        public List<WeaponMasteryInstance> WeaponMasteries { get; set; } = new List<WeaponMasteryInstance>();

        [NotMapped, JsonIgnore]
        public List<WeaponMasteryInstance> ActiveMasteries => WeaponMasteries.Where(mastery => mastery.IsActive).ToList();

        [NotMapped, JsonIgnore]
        public bool WeaponMaxedReminder = false;

        public bool TryGetChallenge(Guid id, out ChallengeInstance challenge)
        {
            challenge = Challenges.Find(c => c.ChallengeId == id);
            return challenge != default;
        }

        public void TrackChallenges(List<Guid> challengeIds)
        {
            var challenges = Challenges.Where(c => challengeIds.Contains(c.ChallengeId));

            ChallengesInProgress.Clear();
            foreach (var instance in challenges) 
            {
                // If the challenge is already complete, don't track it
                if (instance.Complete)
                {
                    continue;
                }
                
                var progress = new ChallengeProgress(instance, this);
                ChallengesInProgress.Add(progress);
            }
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

            WeaponMaxedReminder = false;

            DeactivateAllMasteries();

            // Instantiate new mastery tracks/challenges in response to this change
            List<string> newChallenges = new List<string>();
            List<Guid> challengeInstanceIds = new List<Guid>();
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

                // Otherwise, initialize challenges
                if (!mastery.TryGetCurrentChallenges(out var masteryChallenges))
                {
                    LevelUpMastery(mastery);
                    continue;
                }
                if (ChallengesComplete(masteryChallenges))
                {
                    LevelUpMastery(mastery);
                }
                challengeInstanceIds.AddRange(masteryChallenges);
            }
            SendChallengeUpdate(false, newChallenges);
            TrackChallenges(challengeInstanceIds);
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

            if (mastery.TryGetCurrentUnlock(out var currentUnlock))
            {
                foreach (var challengeId in currentUnlock.ChallengeIds)
                {
                    var challenge = ChallengeDescriptor.Get(challengeId);
                    if (challenge.SpellUnlockId != Guid.Empty)
                    {
                        TryAddSkillToBook(challenge.SpellUnlockId);
                        PacketSender.SendChatMsg(this,
                            Strings.Player.MasterySkillUnlock.ToString(SpellBase.GetName(challenge.SpellUnlockId)),
                            Enums.ChatMessageType.Spells,
                            CustomColors.General.GeneralCompleted);
                    }
                }
            }

            mastery.Level = MathHelper.Clamp(mastery.Level + 1, 0, mastery.WeaponType?.MaxLevel ?? 0);
            if (mastery.Level == weaponType.MaxLevel)
            {
                mastery.ExpRemaining = -1;
                SendMasteryUpdate(true, weaponType.Name);
                return;
            }
            else
            {
                mastery.ExpRemaining = 0;
                SendMasteryUpdate(false, weaponType.Name);

                // Is this weapon at the end of its progress cycle?
                var weapon = GetEquippedWeapon();
                if (weapon.MaxWeaponLevels.TryGetValue(weaponType.Id, out var maxWeaponLevel) && maxWeaponLevel == mastery.Level)
                {
                    SendWeaponMaxedMessage(weaponType);
                }
            }
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

            // Is the current weapon at the end of its progress cycle?
            var equippedWeapon = GetEquippedWeapon();
            if (equippedWeapon != null &&
                equippedWeapon.MaxWeaponLevels != null &&
                equippedWeapon.MaxWeaponLevels.TryGetValue(mastery.WeaponTypeId, out var maxWeaponLevel))
            {
                if (maxWeaponLevel <= mastery.Level)
                {
                    SendWeaponMaxedMessage(mastery.WeaponType);
                    return false;
                }
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

        void SendWeaponMaxedMessage(WeaponTypeDescriptor weaponType)
        {
            if (WeaponMaxedReminder)
            {
                return;
            }
            PacketSender.SendChatMsg(this,
                Strings.Player.WeaponFinished.ToString(weaponType.Name ?? "NOT FOUND"),
                Enums.ChatMessageType.Experience,
                CustomColors.General.GeneralWarning);

            WeaponMaxedReminder = true;
        }
    }
}
