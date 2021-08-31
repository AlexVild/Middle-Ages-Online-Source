using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class ClassInfoPacket : IntersectPacket
    {
        // Nothing to send, just triggers some fetching on the server

        public ClassInfoPacket() { }
    }
}
