using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class PlayerDeathUpdatePacket : IntersectPacket
    {
        public PlayerDeathUpdatePacket()
        {
        }

        [Key(0)]
        public Guid PlayerId { get; set; }

        [Key(1)]
        public bool IsDead { get; set; }

        public PlayerDeathUpdatePacket(Guid playerId, bool isDead)
        {
            PlayerId = playerId;
            IsDead = isDead;
        }
    }
}
