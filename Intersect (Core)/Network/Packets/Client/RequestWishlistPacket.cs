using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestWishlistPacket : IntersectPacket
    {
        public RequestWishlistPacket()
        {
        }
    }
}
