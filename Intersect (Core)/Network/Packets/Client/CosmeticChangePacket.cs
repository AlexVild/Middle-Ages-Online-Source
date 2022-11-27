using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{

    [MessagePackObject]
    public class CosmeticChangePacket : IntersectPacket
    {
        public CosmeticChangePacket()
        {
        }

        [Key(0)]
        public Guid ItemId { get; set; }
        
        [Key(1)]
        public string Slot { get; set; }

        public CosmeticChangePacket(Guid itemId, string slot)
        {
            ItemId = itemId;
            Slot = slot;
        }
    }
}
