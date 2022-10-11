using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class RespawnFinishedPacket : IntersectPacket
    {
        public RespawnFinishedPacket()
        {
        }
    }
}
