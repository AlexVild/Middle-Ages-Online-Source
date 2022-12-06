using MessagePack;
using System;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class KillCountsPacket : IntersectPacket
    {
        [Key(0)]
        public Dictionary<Guid, long> KillCounts { get; set; }

        public KillCountsPacket(Dictionary<Guid, long> killCounts)
        {
            KillCounts = killCounts;
        }
    }
}
