using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestLabelsPacket : IntersectPacket
    {
        public RequestLabelsPacket() { }
    }
}
