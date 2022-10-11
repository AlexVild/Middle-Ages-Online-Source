using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestRespawnPacket : IntersectPacket
    {
        public RequestRespawnPacket()
        {
        }
    }
}
