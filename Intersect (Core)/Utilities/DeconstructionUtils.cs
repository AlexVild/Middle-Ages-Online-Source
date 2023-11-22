using System;

namespace Intersect.Utilities
{
    public static class DeconstructionUtils
    {
        public const int BASE_FUEL_COST = 10;
        public const float BASE_FUEL_DIVISOR = 3.5f;
        public const float BASE_FUEL_MULTIPLIER = 1.75f;

        public static int AverageFuelCostAtTier(int tier)
        {
            if (tier <= 1)
            {
                return BASE_FUEL_COST;
            }
            return (int)Math.Floor(AverageFuelCostAtTier(tier - 1) * BASE_FUEL_MULTIPLIER);
        }

        public static int FuelGivenAtTier(int tier)
        {
            return (int)Math.Floor(AverageFuelCostAtTier(tier) / BASE_FUEL_DIVISOR);
        }

        public static long GetDeconExp(long craftExp)
        {
            return (long)Math.Ceiling(craftExp * Options.Instance.DeconstructionOpts.DeconstructionExpMod);
        }
    }
}
