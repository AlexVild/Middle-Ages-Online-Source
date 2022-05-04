using Intersect.GameObjects.Timers;
using MessagePack;

namespace Intersect.Network.Packets.Editor
{
    [MessagePackObject]
    public class CreateTimerPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public CreateTimerPacket()
        {
        }

        public CreateTimerPacket(TimerOwnerType ownerType)
        {
            OwnerType = ownerType;
        }

        [Key(0)]
        public TimerOwnerType OwnerType { get; set; }
    }
}
