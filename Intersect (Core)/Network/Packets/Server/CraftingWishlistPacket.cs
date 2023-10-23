using MessagePack;
using System;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class CraftingWishlistPacket : IntersectPacket
    {
        [Key(0)]
        public Guid[] Wishlist { get; set; }

        public CraftingWishlistPacket()
        {
        }

        public CraftingWishlistPacket(Guid[] wishlist)
        {
            Wishlist = wishlist;
        }


        public CraftingWishlistPacket(List<Guid> wishlist)
        {
            if (wishlist == default)
            {
                return;
            }
            Wishlist = wishlist.ToArray();
        }
    }
}
