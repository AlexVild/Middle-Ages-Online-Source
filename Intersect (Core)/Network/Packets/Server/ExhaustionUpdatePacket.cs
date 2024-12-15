using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ExhaustionUpdatePacket : IntersectPacket
    {
        [Key(0)]
        public Guid EntityId { get; set; }

        [Key(1)]
        public long ExhaustionEndTime { get; set; }

        public ExhaustionUpdatePacket(Guid entityId, long exhaustionEndTime)
        {
            EntityId = entityId;
            ExhaustionEndTime = exhaustionEndTime;
        }
    }
}
