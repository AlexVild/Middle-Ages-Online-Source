using Intersect.GameObjects;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestRecipesPacket : IntersectPacket
    {
        [Key(0)]
        public RecipeCraftType CraftType { get; set; }

        public RequestRecipesPacket(RecipeCraftType craftType)
        {
            CraftType = craftType;
        }
    }
}
