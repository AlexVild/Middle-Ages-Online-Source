using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestUnlockedRecipesPacket : IntersectPacket
    {
        public RequestUnlockedRecipesPacket() { }
    }
}
