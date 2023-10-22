using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class DestroyItemPacket : SlotQuantityPacket
    {
        //Parameterless Constructor for MessagePack
        public DestroyItemPacket()
        {
            Slot = 0;
            Quantity = 0;
            CheckCanDrop = false;
        }

        public DestroyItemPacket(int slot, int quantity, bool checkCanDrop)
        {
            Slot = slot;
            Quantity = quantity;
            CheckCanDrop = checkCanDrop;
        }

        public DestroyItemPacket(int[] slots, int quantity, bool checkCanDrop)
        {
            Slot = 0;
            Slots = slots;
            Quantity = quantity;
            CheckCanDrop = checkCanDrop;
        }

        [Key(0)]
        public bool CheckCanDrop;
    }

}
