using Intersect.GameObjects.Events;
using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class TerritoriesPacket : IntersectPacket
    {
        public TerritoriesPacket()
        {
        }

        public TerritoriesPacket(TerritoryUpdatePacket[] territories)
        {
            Territories = territories;
        }

        [Key(0)]
        public TerritoryUpdatePacket[] Territories { get; set; }
    }

    [MessagePackObject]
    public class TerritoryUpdatePacket : IntersectPacket
    {
        // EF
        public TerritoryUpdatePacket()
        {
        }

        public TerritoryUpdatePacket(Guid mapId, Guid descriptorId, string owner, string conquerer, TerritoryState state)
        {
            MapId = mapId;
            DescriptorId = descriptorId;
            Owner = owner;
            State = state;
            Conquerer = conquerer;
        }

        [Key(0)]
        public Guid MapId { get; set; }

        [Key(1)]
        public Guid DescriptorId { get; set; }

        [Key(2)]
        public string Owner { get; set; }

        [Key(3)]
        public string Conquerer { get; set; }

        [Key(4)]
        public TerritoryState State { get; set; }
    }
}
