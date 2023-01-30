using MessagePack;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class UpgradeStatPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public UpgradeStatPacket()
        {
        }

        public UpgradeStatPacket(byte stat)
        {
            Stat = stat;
        }

        [Key(0)]
        public byte Stat { get; set; }

    }

    [MessagePackObject]
    public class UpgradeStatsPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public UpgradeStatsPacket()
        {
        }

        public UpgradeStatsPacket(List<UpgradeStatPacket> stats)
        {
            Stats = stats;
        }

        [Key(0)]
        public List<UpgradeStatPacket> Stats { get; set; }

    }

}
