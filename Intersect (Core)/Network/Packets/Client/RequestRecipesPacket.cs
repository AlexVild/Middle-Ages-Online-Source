using Intersect.GameObjects;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestRecipesPacket : IntersectPacket
    {
        [Key(0)]
        public RecipeCraftType CraftType { get; set; }

        [Key(1)]
        public bool WithTutorial { get; set; }

        public RequestRecipesPacket(RecipeCraftType craftType, bool withTutorial = false)
        {
            CraftType = craftType;
            WithTutorial = withTutorial;
        }
    }
}
