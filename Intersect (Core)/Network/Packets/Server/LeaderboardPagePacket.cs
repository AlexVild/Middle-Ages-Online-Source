using MessagePack;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class LeaderboardPagePacket : IntersectPacket
    {
        [Key(0)]
        public List<RecordDto> Records;

        public LeaderboardPagePacket(List<RecordDto> records)
        {
            Records = records;
        }
    }
}
