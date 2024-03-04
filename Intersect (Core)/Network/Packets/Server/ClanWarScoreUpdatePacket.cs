using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ClanWarScoreUpdatePacket : IntersectPacket
    {
        // EF
        public ClanWarScoreUpdatePacket()
        {
        }

        public ClanWarScoreUpdatePacket(ClanWarScore[] scores, ClanWarMapUpdate[] mapUpdates)
        {
            Scores = scores;
            MapUpdates = mapUpdates;
        }

        [Key(0)]
        public ClanWarScore[] Scores { get; set; }

        [Key(1)]
        public ClanWarMapUpdate[] MapUpdates { get; set; }
    }

    [MessagePackObject]
    public class ClanWarScore
    {
        // EF
        public ClanWarScore()
        {
        }

        public ClanWarScore(string guild, int score)
        {
            Guild = guild;
            Score = score;
        }

        [Key(0)]
        public string Guild { get; set; }

        [Key(1)]
        public int Score { get; set; }
    }

    [MessagePackObject]
    public class ClanWarMapUpdate
    {
        public ClanWarMapUpdate()
        {
        }

        public ClanWarMapUpdate(Guid mapId, string owner)
        {
            MapId = mapId;
            Owner = owner;
        }

        [Key(0)]
        public Guid MapId { get; set; }

        [Key(1)]
        public string Owner { get; set; }
    }
}
