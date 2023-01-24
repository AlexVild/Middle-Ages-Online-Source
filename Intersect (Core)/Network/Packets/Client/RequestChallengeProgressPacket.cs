using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestChallengeProgressPacket : IntersectPacket
    {
        public RequestChallengeProgressPacket()
        {
        }
    }
}
