using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class UnlockedRecipesPacket : IntersectPacket
    {
        [Key(0)]
        public List<Guid> UnlockedRecipes { get; set; }

        public UnlockedRecipesPacket()
        {
        }

        public UnlockedRecipesPacket(List<Guid> unlockedRecipes)
        {
            UnlockedRecipes = unlockedRecipes;
        }
    }
}
