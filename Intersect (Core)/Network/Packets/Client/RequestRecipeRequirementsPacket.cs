using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestRecipeRequirementsPacket : IntersectPacket
    {
        [Key(0)]
        public Guid RecipeId { get; set; }

        public RequestRecipeRequirementsPacket()
        {
        }

        public RequestRecipeRequirementsPacket(Guid recipeId)
        {
            RecipeId = recipeId;
        }
    }
}
