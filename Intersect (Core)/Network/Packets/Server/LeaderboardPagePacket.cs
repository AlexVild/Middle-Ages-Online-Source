using MessagePack;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class LeaderboardPagePacket : IntersectPacket
    {
        [Key(0)]
        public List<RecordDto> Records;

        [Key(1)]
        public int CurrentPage;

        [Key(2)]
        public string HighlightedPlayer;

        public LeaderboardPagePacket(List<RecordDto> records, int currentPage, string highlightedPlayer)
        {
            Records = records;
            CurrentPage = currentPage;
            HighlightedPlayer = highlightedPlayer;
        }
    }
}
