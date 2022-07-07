using System;

using Intersect.Enums;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class EntityMovePacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public EntityMovePacket()
        {
        }

        public EntityMovePacket(Guid id, EntityTypes type, Guid mapId, byte x, byte y, byte dir, bool correction)
        {
            Id = id;
            Type = type;
            MapId = mapId;
            X = x;
            Y = y;
            Direction = dir;
            Correction = correction;
            FaceDirection = Direction;
        }

        public EntityMovePacket(Guid id, EntityTypes type, Guid mapId, byte x, byte y, byte direction, bool correction, byte faceDirection, bool combatMode) : this(id, type, mapId, x, y, direction, correction)
        {
            FaceDirection = faceDirection;
            CombatMode = combatMode;
        }

        [Key(0)]
        public Guid Id { get; set; }

        [Key(1)]
        public EntityTypes Type { get; set; }

        [Key(2)]
        public Guid MapId { get; set; }

        [Key(3)]
        public byte X { get; set; }

        [Key(4)]
        public byte Y { get; set; }

        [Key(5)]
        public byte Direction { get; set; }

        [Key(6)]
        public bool Correction { get; set; }

        [Key(7)]
        public byte FaceDirection { get; set; }
        
        [Key(8)]
        public bool CombatMode { get; set; }
    }

}
