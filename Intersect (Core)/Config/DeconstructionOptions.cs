using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Config
{
    public class DeconstructionOptions
    {
        public string AddFuelSound { get; set; } = "al_forging.wav";

        public float DeconstructionExpMod { get; set; } = 0.75f;

        public string DisenhanceItemSound = "al_buy_item.wav";

        public float RemoveEnhancementCostMod { get; set; } = 0.75f;
    }
}
