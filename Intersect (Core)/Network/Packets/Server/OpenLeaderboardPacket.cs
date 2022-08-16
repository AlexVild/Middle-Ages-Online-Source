using Intersect.GameObjects.Events;
using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class OpenLeaderboardPacket : IntersectPacket
    {
        [Key(0)] 
        public RecordType RecordType { get; set; }
        
        [Key(1)]
        public string DisplayName { get; set; }
        
        [Key(2)]
        public RecordScoring ScoreType { get; set; }

        [Key(3)]
        public Guid RecordId { get; set; }

        public OpenLeaderboardPacket(RecordType recordType, string displayName, RecordScoring scoreType, Guid recordId)
        {
            RecordType = recordType;
            DisplayName = displayName;
            ScoreType = scoreType;
            RecordId = recordId;
        }
    }
}
