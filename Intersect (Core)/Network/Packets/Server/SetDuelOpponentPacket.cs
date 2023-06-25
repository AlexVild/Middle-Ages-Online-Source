using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class SetDuelOpponentPacket : IntersectPacket
    {
        [Key(0)]
        public Guid[] Opponents { get; set; }

        public SetDuelOpponentPacket(Guid[] id)
        {
            Opponents = id;
        }

        //EF
        public SetDuelOpponentPacket()
        {
        }
    }
}
