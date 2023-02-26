using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class CompleteUpgradePacket : IntersectPacket
    {
        [Key(0)]
        public Guid ItemId { get; set; }

        [Key(1)]
        public ItemProperties Properties { get; set; }

        public CompleteUpgradePacket(Guid itemId, ItemProperties properties)
        {
            ItemId = itemId;
            Properties = properties;
        }

        public CompleteUpgradePacket()
        {
        }
    }
}
