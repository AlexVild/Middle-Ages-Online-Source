using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class WeaponPickerResponsePacket : IntersectPacket
    {
        [Key(0)]
        public int SelectedSlot { get; set; }

        // EF
        public WeaponPickerResponsePacket() { }

        public WeaponPickerResponsePacket(int selectedSlot)
        {
            SelectedSlot = selectedSlot;
        }
    }
}
