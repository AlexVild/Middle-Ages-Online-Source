using MessagePack;
using System;

namespace Intersect.Network.Packets.Editor
{
    [MessagePackObject]
    public class MapCopyPacket : IntersectPacket
    {
        // EF
        public MapCopyPacket()
        {
        }

        public MapCopyPacket(Guid mapId, Guid copyingMapId)
        {
            MapId = mapId;
            CopyingMapId = copyingMapId;
        }

        [Key(0)]
        public Guid MapId { get; set; }

        [Key(1)]
        public Guid CopyingMapId { get; set; }
    }
}
