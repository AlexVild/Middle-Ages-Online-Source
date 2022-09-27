using MessagePack;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class LootRollUpdatePacket : IntersectPacket
    {
        // EF
        public LootRollUpdatePacket() { }

        public LootRollUpdatePacket(List<Loot> loot)
        {
            UpdatedLoot = new List<Loot>();

            if (loot != null)
            {
                UpdatedLoot.AddRange(loot);
            }
        }

        [Key(0)]
        public List<Loot> UpdatedLoot;
    }
}
