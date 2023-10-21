using Intersect.GameObjects.Events;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class OpenWeaponPickerPacket : IntersectPacket
    {
        [Key(0)]
        public WeaponPickerResult ResultType { get; set; }

        // EF
        public OpenWeaponPickerPacket()
        {
        }

        public OpenWeaponPickerPacket(WeaponPickerResult resultType)
        {
            ResultType = resultType;
        }
    }
}
