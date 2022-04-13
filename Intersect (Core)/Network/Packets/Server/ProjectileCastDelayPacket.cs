using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ProjectileCastDelayPacket : IntersectPacket
    {
        // EF
        public ProjectileCastDelayPacket() { }

        public ProjectileCastDelayPacket(long time)
        {
            DelayTime = time;
        }

        [Key(0)]
        public long DelayTime { get; set; }
    }
}
