using Intersect.GameObjects;
using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class PermabuffInstance
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public PermabuffInstance()
        {
        }

        public PermabuffInstance(Guid playerId, Guid itemId, bool used = true)
        {
            PlayerId = playerId;
            ItemId = itemId;
            Used = used;
        }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; private set; }

        [JsonIgnore, NotMapped]
        public virtual Player Player { get; private set; }

        public Guid ItemId { get; set; }

        [JsonIgnore, NotMapped]
        public ItemBase Item { get => ItemBase.Get(ItemId) ?? null; }

        public bool Used { get; set; }

        public void RemoveFromDb()
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                context.Player_Permabuffs.Remove(this);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }
    }
}
