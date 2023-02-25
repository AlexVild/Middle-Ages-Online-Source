using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class CompleteUpgradePacket : IntersectPacket
    {
        [Key(0)]
        Guid ItemId { get; set; }

        [Key(1)]
        ItemProperties Properties { get; set; }

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
