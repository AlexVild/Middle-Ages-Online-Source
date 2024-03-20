using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class MapTransitionReadyPacket : IntersectPacket
    {
        public MapTransitionReadyPacket()
        {
        }
    }
}
