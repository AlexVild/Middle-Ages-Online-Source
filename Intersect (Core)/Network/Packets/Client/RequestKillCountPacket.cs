using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestKillCountPacket : IntersectPacket
    {
        [Key(0)]
        public Guid NpcId { get; set; }

        public RequestKillCountPacket()
        {
        }

        public RequestKillCountPacket(Guid npcId)
        {
            NpcId = npcId;
        }
    }
}
