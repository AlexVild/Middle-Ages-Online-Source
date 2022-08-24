using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestLeaderboardPacket : IntersectPacket
    {
        [Key(0)]
        public int Page { get; set; }

        [Key(1)]
        public RecordType Type { get; set; }

        [Key(2)]
        public RecordScoring ScoreType { get; set; }

        [Key(3)]
        public Guid RecordId { get; set; }

        [Key(4)]
        public string Term { get; set; }

        [Key(5)]
        public LeaderboardDisplayMode DisplayMode { get; set; }

        public RequestLeaderboardPacket(int page, RecordType type, RecordScoring scoreType, Guid recordId, string term, LeaderboardDisplayMode displayMode)
        {
            Page = page;
            Type = type;
            ScoreType = scoreType;
            RecordId = recordId;
            Term = term;
            DisplayMode = displayMode;
        }
    }
}
