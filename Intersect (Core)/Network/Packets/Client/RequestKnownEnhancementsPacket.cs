using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestKnownEnhancementsPacket : IntersectPacket
    {
        // EF
        public RequestKnownEnhancementsPacket()
        {
        }
    }
}
