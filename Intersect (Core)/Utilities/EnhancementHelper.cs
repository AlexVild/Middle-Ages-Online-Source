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
    }
}
