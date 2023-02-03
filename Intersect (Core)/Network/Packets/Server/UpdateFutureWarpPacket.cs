using MessagePack;
using Intersect.Enums;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class UpdateFutureWarpPacket : IntersectPacket
    {
        public UpdateFutureWarpPacket(System.Guid mapId, float x, float y, byte dir, MapInstanceType instanceType, Guid dungeonId)
        {
            NewMapId = mapId;
            X = x;
            Y = y;
            Dir = dir;
            InstanceType = instanceType;
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
