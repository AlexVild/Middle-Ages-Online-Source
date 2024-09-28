using Intersect.GameObjects;
using Intersect.Server.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class WeaponMasteryInstance : IPlayerOwned
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        [JsonIgnore, NotMapped]
        public Player Player { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }

        [Column("WeaponType")]
        public Guid WeaponTypeId { get; set; }

        [NotMapped, JsonIgnore]
        public WeaponTypeDescriptor WeaponType => WeaponTypeDescriptor.Get(WeaponTypeId);

        public int Level { get; set; }

        [Column("ExpRemaining")]
        public long Exp { get; set; }

        public bool IsActive { get; set; }

        public WeaponMasteryInstance()
        {
        }

        public WeaponMasteryInstance(Guid playerId, Guid weaponTypeId, int level, bool isActive)
        {
            PlayerId = playerId;
            var descriptor = WeaponTypeDescriptor.Get(weaponTypeId);

            if (descriptor == null)
            {
                return;
            }
            
            WeaponTypeId = weaponTypeId;
            Level = level;
            IsActive = isActive;

            Exp = 0;
        }

        public bool TryGetCurrentWeaponLevelProperties(out WeaponLevel unlock)
        {
            unlock = default;
            if (WeaponType == default)
            {
                return false;
            }

            if (!WeaponType.Unlocks?.TryGetValue(Level + 1, out unlock) ?? true)
            {
                return false;
            }

            return true;
        }

        public bool TryGetCurrentChallenges(out List<Guid> challengeIds)
        {
            challengeIds = new List<Guid>();
            if (!TryGetCurrentWeaponLevelProperties(out var unlock))
            {
                return false;
            }

            challengeIds = unlock.ChallengeIds;
            return true;
        }

        public List<Guid> GetAllChallengeIds()
        {
            List<Guid> challengeIds = new List<Guid>();
            if (WeaponType == default)
            {
                return challengeIds;
            }

            foreach (var unlock in WeaponType.Unlocks)
            {
                challengeIds.AddRange(unlock.Value.ChallengeIds);
            }

            return challengeIds;
        }

        public void RemoveFromDb()
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                context.Player_Weapon_Masteries.Remove(this);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }
    }
}
