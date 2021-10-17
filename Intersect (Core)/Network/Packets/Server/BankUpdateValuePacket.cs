using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class BankUpdateValuePacket : IntersectPacket
    {
        BankUpdateValuePacket()
        {

        }

        public BankUpdateValuePacket(int bankValue)
        {
            BankValue = bankValue;
        }

        [Key(0)]
        public int BankValue { get; set; }
    }
}
