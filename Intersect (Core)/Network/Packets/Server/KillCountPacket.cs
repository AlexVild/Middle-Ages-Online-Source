using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class KillCountPacket : IntersectPacket
    {
        [Key(0)]
        public Guid NpcId { get; set; }

        [Key(1)]
        public long KillCount { get; set; }

        public KillCountPacket()
        {
        }

        public KillCountPacket(Guid npcId, long killCount)
        {
            NpcId = npcId;
            KillCount = killCount;
        }
    }
}
