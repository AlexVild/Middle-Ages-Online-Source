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

        public LeaderboardPagePacket(List<RecordDto> records, int currentPage)
        {
            Records = records;
            CurrentPage = currentPage;
        }
    }
}
