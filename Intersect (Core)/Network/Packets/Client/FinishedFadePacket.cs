using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class FinishedFadePacket : IntersectPacket
    {
        // EF
        public FinishedFadePacket()
        {
        }
    }
}
