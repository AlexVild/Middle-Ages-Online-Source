using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestTerritorySyncPacket : IntersectPacket
    {
        public RequestTerritorySyncPacket()
        {
        }

        public RequestTerritorySyncPacket(Guid mapId, Guid territoryId)
        {
            MapId = mapId;
            TerritoryId = territoryId;
        }

        [Key(0)]
        public Guid MapId { get; set; }

        [Key(1)]
        public Guid TerritoryId { get; set; }
    }
}
