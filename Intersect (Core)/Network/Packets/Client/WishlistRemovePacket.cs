using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class WishlistRemovePacket : IntersectPacket
    {
        [Key(0)]
        public Guid CraftId { get; set; }

        public WishlistRemovePacket()
        {
        }

        public WishlistRemovePacket(Guid craftId)
        {
            CraftId = craftId;
        }
    }
}
