using Intersect.GameObjects;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class Loot
    {
        [Key(0)]
        public Guid ItemId { get; set; }

        [Key(1)]
        public int Quantity { get; set; }

        [Key(2)]
        public int[] StatBuffs { get; set; }
    }

    [MessagePackObject]
    public class OpenLootRollPacket : IntersectPacket
    {
        // ef
        public OpenLootRollPacket() { }

        public OpenLootRollPacket(List<Loot> loot, string title)
        {
            Loot = new List<Loot>();

            if (loot != null)
            {
                Loot.AddRange(loot);
            }

            Title = title;
        }

        [Key(0)]
        public List<Loot> Loot { get; set; }
        
        [Key(1)]
        public string Title { get; set; }
    }
}
