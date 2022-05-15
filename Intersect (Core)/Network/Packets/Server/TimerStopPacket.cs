using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class TimerStopPacket : IntersectPacket
    {
        [Key(0)]
        public Guid DescriptorId;

        [Key(1)]
        public long ElapsedTime;

        // EF
        public TimerStopPacket()
        {
        }

        public TimerStopPacket(Guid descriptorId, long elapsedTime)
        {
            DescriptorId = descriptorId;
            ElapsedTime = elapsedTime;
        }
    }
}
