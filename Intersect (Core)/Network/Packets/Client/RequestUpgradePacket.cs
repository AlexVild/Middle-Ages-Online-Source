using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestUpgradePacket : IntersectPacket
    {
        [Key(0)]
        public Guid CraftId { get; set; }

        public RequestUpgradePacket(Guid craftId)
        {
            CraftId = craftId;
        }
    }
}
