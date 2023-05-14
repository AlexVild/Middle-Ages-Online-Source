using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RemoveLoadoutPacket : IntersectPacket
    {
        [Key(0)]
        public Guid LoadoutId { get; set; }

        public RemoveLoadoutPacket(Guid loadoutId)
        {
            LoadoutId = loadoutId;
        }

        public RemoveLoadoutPacket()
        {
        }
    }
}
