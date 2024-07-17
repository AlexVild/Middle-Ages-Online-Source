using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestChallengeBonusesPacket : IntersectPacket
    {
        public RequestChallengeBonusesPacket()
        {
        }
    }
}
