using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class TradeAcceptedPacket : IntersectPacket
    {
        public TradeAcceptedPacket()
        {
        }
    }
}
