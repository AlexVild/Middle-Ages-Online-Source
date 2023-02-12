using Intersect.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Entities
{
    public class Deconstructor
    {
        public float FuelMultiplier { get; set; }

        public List<Item> Items { get; set; }

        public Deconstructor(float multiplier)
        {
            FuelMultiplier = multiplier;
        }
    }
}
