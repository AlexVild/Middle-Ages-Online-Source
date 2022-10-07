using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ServerDisposedPacket : IntersectPacket
    {
        // EF
        public ServerDisposedPacket() { }
    }
}
