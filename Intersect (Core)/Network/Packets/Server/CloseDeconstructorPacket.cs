using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class CloseDeconstructorPacket : IntersectPacket
    {
        public CloseDeconstructorPacket()
        {
        }
    }
}
