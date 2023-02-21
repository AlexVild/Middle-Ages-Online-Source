using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Utilities
{
    public static class EnhancementHelper
    {
        /// <summary>
        /// Determines whether a weapon's weapon type levels meet an enhancement's required weapon levels
        /// </summary>
        /// <param name="maxLevels">The dict of weapon type -> weapon level from some Item</param>
        /// <param name="validEnhancements">The dict of weapon type -> req. level from some Enhancement</param>
        /// <param name="failureReason">A string containing the failure reason, or empty string if none</param>
        /// <returns>Whether the enhancement can be applied to some weapon</returns>
        public static bool WeaponLevelRequirementMet(Dictionary<Guid, int> maxLevels, Dictionary<Guid, int> validEnhancements, out string failureReason)
        {
            failureReason = string.Empty;
            if (maxLevels == default || validEnhancements == default)
            {
                return false;
            }

            var matchingWeaponTypes = maxLevels.Keys.Union(validEnhancements.Keys).ToArray();
            if (matchingWeaponTypes.Length == 0)
            {
                failureReason = "This enhancement can not be applied to a weapon of this type.";
                return false;
            }

            foreach(var weaponType in matchingWeaponTypes)
            {
                if (!maxLevels.TryGetValue(weaponType, out int weaponLevel) || !validEnhancements.TryGetValue(weaponType, out var requiredLevel))
                {
                    continue;
                }
                
                if (weaponLevel >= requiredLevel)
                {
                    return true;
                }
            }

            failureReason = "This weapon is not high enough of a weapon level to receive this enhancement.";
            return false;
        }

        public static int GetEnhancementCostOnWeapon(ItemBase weapon, Guid[] enhancements, float multiplier)
        {
            if (weapon == null || enhancements.Length <= 0 || multiplier <= 0)
            {
                return 0;
            }

            var epCost = 0;
            foreach (var en in enhancements.Select(en => EnhancementDescriptor.Get(en)).ToArray())
            {
                epCost += en.RequiredEnhancementPoints;
            }

            // For now, we just use the most expensive EP cost out of the weapon's available weapon types
            var weaponEpCost = 0;
            foreach (var kv in weapon.MaxWeaponLevels)
            {
                var typeId = kv.Key;
                var weaponLvl = kv.Value;
                var weaponTypeDesc = WeaponTypeDescriptor.Get(typeId);

                if(!weaponTypeDesc.Unlocks.TryGetValue(weaponLvl, out var lvlDetails))
                {
                    continue;
                }

                weaponEpCost = Math.Max(weaponEpCost, lvlDetails.EnhancementCostPerPoint * epCost);
            }

            return (int)Math.Round(weaponEpCost * multiplier);
        }
    }
}
