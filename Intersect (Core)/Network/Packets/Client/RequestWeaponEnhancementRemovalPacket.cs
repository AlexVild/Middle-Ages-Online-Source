using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestWeaponEnhancementRemovalPacket : IntersectPacket
    {
        public RequestWeaponEnhancementRemovalPacket()
        {
        }
    }
}
