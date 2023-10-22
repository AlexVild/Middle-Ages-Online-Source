using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class DepositItemPacket : SlotQuantityPacket
    {
        //Parameterless Constructor for MessagePack
        public DepositItemPacket() : base(0, 0)
        {
        }

        public DepositItemPacket(int slot, int quantity) : base(slot, quantity)
        {
        }

        public DepositItemPacket(int[] slots, int quantity) : base(slots, quantity)
        {
        }

    }

}
