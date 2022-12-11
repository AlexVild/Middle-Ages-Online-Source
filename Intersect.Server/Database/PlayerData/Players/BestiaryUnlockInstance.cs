using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class BestiaryUnlockInstance : IPlayerOwned
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        // EF
        public BestiaryUnlockInstance() { }

        public BestiaryUnlockInstance(Guid playerId, Guid npcId, BestiaryUnlock unlock, bool isUnlocked = true)
        {
            PlayerId = playerId;
            NpcId = npcId;
            Unlocked = isUnlocked;
            UnlockType = (int)unlock;
        }

        public int UnlockType { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; private set; }

        [JsonIgnore, NotMapped]
        public virtual Player Player { get; private set; }

        public Guid NpcId { get; set; }

        [JsonIgnore, NotMapped]
        public NpcBase Npc { get => NpcBase.Get(NpcId) ?? null; }

        public bool Unlocked { get; set; }
    }
}
