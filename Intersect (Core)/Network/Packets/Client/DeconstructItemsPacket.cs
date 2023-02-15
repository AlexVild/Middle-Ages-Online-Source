using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class DeconstructItemsPacket : IntersectPacket
    {
        [Key(0)]
        public int[] Slots { get; set; }

        // EF
        public DeconstructItemsPacket()
        {
        }

        public DeconstructItemsPacket(int[] slots)
        {
            Slots = slots;
        }
    }
}
