using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class TimerPacket : IntersectPacket
    {
        [Key(0)]
        public Guid DescriptorId;
        
        [Key(1)]
        public long Timestamp;

        [Key(2)]
        public long StartTime;

        // EF
        public TimerPacket()
        {
        }

        public TimerPacket(Guid descriptorId, long timestamp, long startTime)
        {
            DescriptorId = descriptorId;
            Timestamp = timestamp;
            StartTime = startTime;
        }
    }
}
