using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class SendOpenDeconstructorPacket : IntersectPacket
    {
        [Key(0)]
        public int CurrentFuel { get; set; }

        [Key(1)]
        public float FuelCostMultiplier { get; set; }

        public SendOpenDeconstructorPacket()
        {
        }

        public SendOpenDeconstructorPacket(int fuel, float multi)
        {
            CurrentFuel = fuel;
            FuelCostMultiplier = multi;
        }
    }
}
