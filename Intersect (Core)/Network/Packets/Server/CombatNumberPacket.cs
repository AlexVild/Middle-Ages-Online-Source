using Intersect.GameObjects.Events;
using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class CombatNumberPacket : IntersectPacket
    {
        [Key(0)]
        public CombatNumberType Type { get; set; }
        
        [Key(1)]
        public int Value { get; set; }

        [Key(2)]
        public Guid Target { get; set; }

        [Key(3)]
        public Guid VisibleTo { get; set; }

        [Key(4)]
        public int X { get; set; }

        [Key(5)]
        public int Y { get; set; }

        [Key(6)]
        public Guid MapId { get; set; }

        [Key(7)]
        public int Threshold { get; set; }

        public CombatNumberPacket(CombatNumberType type, int value, Guid target, Guid visibleTo, int x, int y, Guid mapId, int threshold)
        {
            Type = type;
            Value = value;
            Target = target;
            VisibleTo = visibleTo;
            X = x;
            Y = y;
            MapId = mapId;
            Threshold = threshold;
        }
    }
}
