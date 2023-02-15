using Intersect.Enums;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ItemProperties : IntersectPacket
    {
        public ItemProperties()
        {
        }

        public ItemProperties(ItemProperties other)
        {
            if (other == default)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Array.Copy(other.StatModifiers, StatModifiers, (int)Stats.StatCount);
        }

        [Key(0)]
        public int[] StatModifiers { get; set; } = new int[(int)Stats.StatCount];
    }
}
