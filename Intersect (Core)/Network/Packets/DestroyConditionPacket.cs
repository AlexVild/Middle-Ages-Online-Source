using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class DestroyConditionPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public DestroyConditionPacket()
        {
            CanDestroy = false;
            Index = 0;
        }

        public DestroyConditionPacket(bool canDestroy, int index)
        {
            CanDestroy = canDestroy;
            Index = index;
        }

        [Key(0)]
        public bool CanDestroy;

        [Key(1)]
        public int Index;
    }

}
