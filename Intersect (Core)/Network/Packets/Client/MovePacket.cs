using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class MovePacket : AbstractTimedPacket
    {
        //Parameterless Constructor for MessagePack
        public MovePacket()
        {
        }

        public MovePacket(Guid mapId, byte x, byte y, byte dir, byte faceDir)
        {
            MapId = mapId;
            X = x;
            Y = y;
            Dir = dir;
            FaceDir = faceDir;
        }

        public MovePacket(Guid mapId, byte x, byte y, byte dir)
        {
            MapId = mapId;
            X = x;
            Y = y;
            Dir = dir;
            FaceDir = null;
        }

        [Key(3)]
        public Guid MapId { get; set; }

        [Key(4)]
        public byte X { get; set; }

        [Key(5)]
        public byte Y { get; set; }

        [Key(6)]
        public byte Dir { get; set; }

        [Key(7)]
        public byte? FaceDir { get; set; }

    }

}
