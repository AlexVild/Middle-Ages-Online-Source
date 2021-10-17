using System;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class BankSortPacket : IntersectPacket
    {
        [Key(0)]
        public string sortType = "standard";

        public BankSortPacket()
        {
            // Empty constructor for messagepack   
        }
    }
}