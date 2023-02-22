using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class ApplyEnhancementsPacket : IntersectPacket
    {
        [Key(0)]
        public Guid[] Enhancements { get; set; }

        public ApplyEnhancementsPacket(Guid[] enhancements)
        {
            Enhancements = enhancements;
        }

        public ApplyEnhancementsPacket()
        {
        }
    }
}
