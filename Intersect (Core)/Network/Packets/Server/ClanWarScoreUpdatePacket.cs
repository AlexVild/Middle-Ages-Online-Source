using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ClanWarScoreUpdatePacket : IntersectPacket
    {
        // EF
        public ClanWarScoreUpdatePacket()
        {
        }

        public ClanWarScoreUpdatePacket(ClanWarScore[] scores)
        {
            Scores = scores;
        }

        [Key(0)]
        public ClanWarScore[] Scores { get; set; }
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
}
