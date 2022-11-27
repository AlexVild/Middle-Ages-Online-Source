using Intersect.GameObjects;
using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class CosmeticInstance
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        // EF
        public CosmeticInstance() { }

        public CosmeticInstance(Guid playerId, Guid itemId, bool unlocked = true)
        {
            PlayerId = playerId;
            ItemId = itemId;
            Unlocked = unlocked;
        }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; private set; }

        [JsonIgnore, NotMapped]
        public virtual Player Player { get; private set; }

        public Guid ItemId { get; set; }

        [JsonIgnore, NotMapped]
        public ItemBase Item { get => ItemBase.Get(ItemId) ?? null; }

        public bool Unlocked { get; set; }
    }
}
