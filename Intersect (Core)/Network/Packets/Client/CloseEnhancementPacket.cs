using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class CloseEnhancementPacket : IntersectPacket
    {
        public CloseEnhancementPacket()
        {
        }
    }
}
