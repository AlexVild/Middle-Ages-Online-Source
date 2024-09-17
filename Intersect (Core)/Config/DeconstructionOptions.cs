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

        public string ScrapItemId { get; set; } = "a85c1b15-97f5-4f2a-9dcc-070113882190";

        public float ScrapIncreasePerTier { get; set; } = 1.8f;

        public float ScrapWeaponModifier { get; set; } = 1.4f;

        public float ScrapArmorModifier { get; set; } = 1.1f;

        public float ScrapRareModifier { get; set; } = 1.8f;
        
        public float BaseScrapAmount { get; set; } = 10f;

        public float ScrapVariance { get; set; } = .08f;
    }
}
