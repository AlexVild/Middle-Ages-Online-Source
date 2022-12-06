using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestKillCountsPacket : IntersectPacket
    {
        public RequestKillCountsPacket()
        {
        }
    }
}
