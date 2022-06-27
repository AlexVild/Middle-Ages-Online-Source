using Intersect.GameObjects.Timers;
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

        [Key(3)]
        public TimerType Type;

        [Key(4)]
        public string DisplayName;

        [Key(5)]
        public bool ContinueAfterExpiration;

        // EF
        public TimerPacket()
        {
        }

        public TimerPacket(Guid descriptorId, long timestamp, long startTime, TimerType type, string displayName, bool continueAfterExpiration)
        {
            DescriptorId = descriptorId;
            Timestamp = timestamp;
            StartTime = startTime;
            Type = type;
            DisplayName = displayName;
            ContinueAfterExpiration = continueAfterExpiration;
        }
    }
}
