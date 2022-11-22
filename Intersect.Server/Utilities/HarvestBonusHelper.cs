using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Server.Entities;
using Intersect.Server.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Utilities
{
    public static class HarvestBonusHelper
    {
        public static long GetAmountInGroupHarvested(Player player, Guid resourceId)
        {
            if (player == null)
            {
                return 0;
            }

            var resource = ResourceBase.Get(resourceId);

            if (resource == null)
            {
                return 0;
            }

            var playerRecord = player.PlayerRecords.Find(record => record.Type == RecordType.ResourceGathered && record.RecordId == resourceId);

            // Handle resource groups
            if (!string.IsNullOrEmpty(resource.ResourceGroup))
            {
                var resources = Globals.CachedResources.Where(rsc => resource.ResourceGroup == rsc.ResourceGroup).ToArray();
                long totalHarvested = 0;
                foreach (var res in resources)
                {
                    var tmpRecord = player.PlayerRecords.Find(record => record.Type == RecordType.ResourceGathered && record.RecordId == res.Id);
                    if (tmpRecord == default)
                    {
                        continue;
                    }
                    totalHarvested += tmpRecord.Amount;
                }

                return totalHarvested;
            }
            else
            {
                if (playerRecord == default)
                {
                    return 0;
                }
                return playerRecord.Amount;
            }
        }

        public static long GetHarvestsUntilNextBonus(Player player, Guid resourceId)
        {
            if (player == null) return 0;

            long currentHarvests = GetAmountInGroupHarvested(player, resourceId);

            return GetHarvestsUntilNextBonus(currentHarvests);
        }

        public static long GetHarvestsUntilNextBonus(long currentHarvests)
        {
            var intervals = Options.Instance.CombatOpts.HarvestBonusIntervals.ToList();
            if (currentHarvests >= intervals.Last())
            {
                return 0;
            }

            return intervals.Find(x => currentHarvests < x) - currentHarvests;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="resourceId"></param>
        /// <returns>-1 if the player has not unlocked a bonus level yet</returns>
        public static int GetHarvestBonusLevel(Player attacker, Guid resourceId)
        {
            long amtHarvested = GetAmountInGroupHarvested(attacker, resourceId);
            if (amtHarvested <= 0)
            {
                return -1;
            }

            var intervals = Options.Instance.CombatOpts.HarvestBonusIntervals.ToList();

            for (int i = 0; i < intervals.Count - 1; i++)
            {
                if (amtHarvested >= intervals[i] && amtHarvested < intervals[i + 1])
                {
                    return i;
                }
                else if (amtHarvested > intervals[i] && i == intervals.Count - 2)
                {
                    return i + 1;
                }
            }

            return -1;
        }

        public static double CalculateHarvestBonus(Player attacker, Guid resourceId)
        {
            if (attacker == null)
            {
                return 0.0;
            }

            var intervals = Options.Instance.CombatOpts.HarvestBonusIntervals.ToList();
            var bonuses = Options.Instance.CombatOpts.HarvestBonuses.ToList();
            if (bonuses.Count != intervals.Count)
            {
                Logging.Log.Error($"You fucked up the server config for harvest bonuses. Count is {bonuses.Count}, must be {intervals.Count}");
                return 0.0f;
            }

            var bonusLevel = GetHarvestBonusLevel(attacker, resourceId);
            if (bonusLevel < 0)
            {
                return 0;
            }
            else
            {
                return bonuses[bonusLevel];
            }
        }
    }
}
