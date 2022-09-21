using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Utilities
{
    public static class LootTableHelpers
    {
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
    }
}
