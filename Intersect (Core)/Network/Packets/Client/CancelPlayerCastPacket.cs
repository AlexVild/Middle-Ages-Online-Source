using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{

    [MessagePackObject]
    public class CancelPlayerCastPacket : IntersectPacket
    {
        [Key(0)]
        public Guid PlayerId;

        public CancelPlayerCastPacket(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}
