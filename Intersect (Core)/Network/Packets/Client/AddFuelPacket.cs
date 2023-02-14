using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class AddFuelPacket : IntersectPacket
    {
        [Key(0)]
        public Dictionary<int, int> SlotsAndQuantities { get; set; }

        public AddFuelPacket()
        {
        }

        public AddFuelPacket(Dictionary<int, int> slotsAndQuantities)
        {
            SlotsAndQuantities = slotsAndQuantities;
        }
    }
}
