using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Client
{

    [MessagePackObject]
    public class WishlistAddPacket : IntersectPacket
    {
        [Key(0)]
        public Guid CraftId { get; set; }

        public WishlistAddPacket()
        {
        }

        public WishlistAddPacket(Guid craftId)
        {
            CraftId = craftId;
        }
    }
}
