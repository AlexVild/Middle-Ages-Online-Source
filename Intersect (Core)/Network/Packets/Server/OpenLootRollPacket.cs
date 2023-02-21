using Intersect.GameObjects;
using Intersect.GameObjects.Events;
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
        public ItemProperties ItemProperties { get; set; }
    }

    [MessagePackObject]
    public class OpenLootRollPacket : IntersectPacket
    {
        // ef
        public OpenLootRollPacket() { }

        public OpenLootRollPacket(List<Loot> loot, string title, LootAnimType animType)
        {
            Loot = new List<Loot>();

            if (loot != null)
            {
                Loot.AddRange(loot);
            }

            Title = title;
            AnimationType = animType;
        }

        [Key(0)]
        public List<Loot> Loot { get; set; }
        
        [Key(1)]
        public string Title { get; set; }

        [Key(2)]
        public LootAnimType AnimationType { get; set; }
    }
}
