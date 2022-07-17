using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class MapGridPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public MapGridPacket()
        {
        }

        public MapGridPacket(Guid[,] grid, string[,] editorGrid, bool clearKnownMaps, string[,] mapNames)
        {
            Grid = grid;
            EditorGrid = editorGrid;
            ClearKnownMaps = clearKnownMaps;
            MapNames = mapNames;
        }

        [Key(0)]
        public Guid[,] Grid { get; set; }

        [Key(1)]
        public string[,] EditorGrid { get; set; }

        [Key(2)]
        public bool ClearKnownMaps { get; set; }

        [Key(3)]
        public string[,] MapNames { get; set; }

    }

}
