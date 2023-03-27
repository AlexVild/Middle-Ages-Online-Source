using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class KnownEnhancementsPacket : IntersectPacket
    {
        [Key(0)]
        public Guid[] KnownEnhancements { get; set; }

        public KnownEnhancementsPacket()
        {
        }

        public KnownEnhancementsPacket(Guid[] knownEnhancements)
        {
            KnownEnhancements = knownEnhancements;
        }
    }
}
