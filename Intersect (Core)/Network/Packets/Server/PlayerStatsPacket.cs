using Intersect.Enums;
using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class PlayerStatsPacket : EntityStatsPacket
    {
        PlayerStatsPacket() { } // MP

        public PlayerStatsPacket(Guid id, EntityTypes type, Guid mapId, int[] stats, int[] trueStats, Guid[] activePassives) : base(id, type, mapId, stats)
        {
            TrueStats = trueStats;
            ActivePassives = activePassives;
        }
        
        [Key(4)]
        public int[] TrueStats { get; set; }

        [Key(5)]
        public Guid[] ActivePassives { get; set; }
    }
}
