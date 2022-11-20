using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class ResourceInfoRequestPacket : IntersectPacket
    {
        [Key(0)]
        public int Tool { get; set; }

        public ResourceInfoRequestPacket()
        {
        }

        public ResourceInfoRequestPacket(int tool)
        {
            Tool = tool;
        }
    }
}
