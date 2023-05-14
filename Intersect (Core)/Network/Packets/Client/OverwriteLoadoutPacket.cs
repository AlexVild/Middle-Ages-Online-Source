using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Client
{

    [MessagePackObject]
    public class OverwriteLoadoutPacket : IntersectPacket
    {
        [Key(0)]
        public Guid LoadoutId { get; set; }

        public OverwriteLoadoutPacket(Guid loadoutId)
        {
            LoadoutId = loadoutId;
        }

        //EF
        public OverwriteLoadoutPacket()
        {
        }
    }
}
