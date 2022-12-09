using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Utilities
{
    public static class LootTableHelpers
    {
        public static List<BaseDrop> UnfoldDrops(List<BaseDrop> drops)
        {
            var currDrops = new List<BaseDrop>();
            if (drops == null)
            {
                return currDrops;
            }

            foreach(var drop in drops)
            {
                if (drop.LootTableId == Guid.Empty)
                {
                    currDrops.Add(drop);
                    continue;
                }
                var lootTable = LootTableDescriptor.Get(drop.LootTableId);
                currDrops.AddRange(UnfoldDrops(lootTable.Drops));
            }

            return currDrops;
        }

        public static double GetTotalWeight(List<BaseDrop> drops, bool expandTables = false)
        {
            if (drops == null)
            {
                return 0.0;
            }

            if (!expandTables)
            {
                return drops.Sum(drop => drop.Chance);
            }

            return drops.Sum(drop =>
            {
                if (drop.LootTableId == Guid.Empty)
                {
                    return drop.Chance;
                }
                else
                {
                    var table = LootTableDescriptor.Get(drop.LootTableId);
                    if (table == null)
                    {
                        return 0.0;
                    }
                    return GetTotalWeight(table.Drops, true);
                }
            });
        }

        public static string GetPrettyChance(double chance, double total)
        {
            // Makeup for the decimals allowed in the editor due to legacy
            uint iChance = (uint)Math.Abs((int)Math.Round(chance * 100));
            uint iTotal = (uint)Math.Abs(Math.Round(total * 100));

            var gcd = MathHelper.GCD(iChance, iTotal);

            return $"{iChance / gcd} / {iTotal / gcd}";
        }
    }
}
