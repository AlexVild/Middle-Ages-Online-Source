using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestInstanceLeavePacket : IntersectPacket
    {
        public RequestInstanceLeavePacket()
        {
        }
    }
}
