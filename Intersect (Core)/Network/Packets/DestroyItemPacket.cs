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

        [Key(4)]
        public bool CheckCanDrop;
    }

}
