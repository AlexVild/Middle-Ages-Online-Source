using Intersect.GameObjects;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.Server.Database.PlayerData
{
    public class ItemDiscoveryInstance : IPlayerOwned
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Player Player { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }

        public Guid ItemId { get; set; }

        public ItemBase Item => ItemBase.Get(ItemId);

        public ItemDiscoveryInstance(Guid playerId, Guid itemId)
        {
            PlayerId = playerId;
            ItemId = itemId;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ItemDiscoveryInstance;
            if (other == null)
            {
                return false;
            }
            return PlayerId == other.PlayerId && ItemId == other.ItemId;
        }

        public override int GetHashCode()
        {
            return ItemId.GetHashCode();
        }

        public ItemDiscoveryInstance()
        {
        }
    }
}
