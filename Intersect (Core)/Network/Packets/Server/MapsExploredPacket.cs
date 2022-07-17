using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MapsExploredPacket : IntersectPacket
    {
        [Key(0)]
        public List<Guid> Maps;

        public MapsExploredPacket() { } // EF

        public MapsExploredPacket(List<Guid> maps)
        {
            Maps = maps;
        }
    }
}
