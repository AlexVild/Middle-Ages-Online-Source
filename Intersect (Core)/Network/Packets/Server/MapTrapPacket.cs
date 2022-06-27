using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MapTrapPacket : IntersectPacket
    {
        public MapTrapPacket() { } // ef

        public MapTrapPacket(Guid mapId, Guid trapId, Guid animationId, Guid ownerId, byte x, byte y, bool remove)
        {
            MapId = mapId;
            TrapId = trapId;
            AnimationId = animationId;
            OwnerId = ownerId;
            X = x;
            Y = y;
            Remove = remove;
        }

        [Key(0)]
        public Guid MapId { get; set; }

        [Key(1)]
        public Guid TrapId { get; set; }

        [Key(2)]
        public Guid AnimationId { get; set; }

        [Key(3)]
        public Guid OwnerId { get; set; }

        [Key(4)]
        public byte X { get; set; }

        [Key(5)]
        public byte Y { get; set; }

        [Key(6)]
        public bool Remove { get; set; }
    }
}
