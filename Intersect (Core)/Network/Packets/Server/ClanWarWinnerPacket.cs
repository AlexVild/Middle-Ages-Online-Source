using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ClanWarWinnerPacket : IntersectPacket
    {
        // EF
        public ClanWarWinnerPacket()
        {
        }

        public ClanWarWinnerPacket(string clanWarWinnerId)
        {
            ClanWarWinner = clanWarWinnerId;
        }

        [Key(0)]
        public string ClanWarWinner { get; set; }
    }
}
