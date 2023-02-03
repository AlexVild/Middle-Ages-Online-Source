using MessagePack;
using Intersect.Enums;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class MapTransitionReadyPacket : IntersectPacket
    {
        public MapTransitionReadyPacket(System.Guid mapId, float x, float y, byte dir, MapInstanceType mapInstanceType, Guid dungeonId)
        {
            NewMapId = mapId;
            X = x;
            Y = y;
            Dir = dir;
            InstanceType = mapInstanceType;
            DungeonId = dungeonId;
        }

        [Key(0)]
        public System.Guid NewMapId { get; set; }

        [Key(1)]
        public float X { get; set; }

        [Key(2)]
        public float Y { get; set; }

        [Key(3)]
        public byte Dir { get; set; }

        [Key(4)]
        public MapInstanceType InstanceType { get; set; }

        [Key(5)]
        public Guid DungeonId { get; set; }
    }
}
