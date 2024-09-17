using Intersect.GameObjects;
using Intersect.Server.Database;
using Intersect.Server.Entities;
using Intersect.Server.Entities.Events;
using Intersect.Server.Maps;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Server.Utilities
{
    public static class LootTableServerHelpers
    {
        /// <summary>
        /// Generates a dictionary containing items mapped ot their highest possible roll numbers, for DnD-style rolling of a table.
        /// </summary>
        /// <param name="drops"></param>
        /// <returns></returns>
        public static Dictionary<int, Item> GenerateDropTable(List<BaseDrop> drops, Player forPlayer)
        {
            if (forPlayer == null) {  return null; }

            List<BaseDrop> items = FlattenDropTable(drops, forPlayer);
            // Stores drop tables by their maximum roll
            var dropTable = new Dictionary<int, Item>();
            items.Sort((a, b) =>
            {
                return a.Chance.CompareTo(b.Chance);
            });

            for (var n = 0; n < items.Count; n++)
            {
                var item = items[n];
                var lastWeight = dropTable.Keys.LastOrDefault();
                if (item == null)
                {
                    continue;
                }

                var itemBase = ItemBase.Get(item.ItemId);
                // something ain't right
                if (itemBase == null && item.ItemId != Guid.Empty)
                {
                    continue;
                }

                // Build the weighted drop table
                if (item.ItemId == Guid.Empty) // "none" item, put it on the table
                {
                    dropTable.Add((int)(lastWeight + item.Chance * 100), null);
                }
                else
                {
                    // Quantity modifiers
                    var quantity = item.Quantity;
                    dropTable.Add((int)(lastWeight + item.Chance * 100), new Item(item.ItemId, quantity));
                }
            }

            return dropTable;
        }

        public static Item GetItemFromTable(Dictionary<int, Item> dropTable)
        {
            if (dropTable == null)
            {
                return null;
            }

            var maxRoll = dropTable.Keys.LastOrDefault();
            if (maxRoll <= 0)
            {
                return null;
            }
            //Npc drop rates
            var randomChance = Randomization.Next(1, maxRoll + 1);
            var rolledItem = dropTable.Where(kv => kv.Key >= randomChance).FirstOrDefault().Value;
            if (rolledItem == default)
            {
                return null;
            }
            return rolledItem;
        }

        private static List<BaseDrop> FlattenDropTable(List<BaseDrop> drops, Player forPlayer)
        {
            var flattenedDrops = new List<BaseDrop>();
            if (drops == null)
            {
                return flattenedDrops;
            }
            foreach (BaseDrop drop in drops)
            {
                if (drop.LootTableId == default)
                {
                    flattenedDrops.Add(drop);
                    continue;
                }
                var table = LootTableDescriptor.Get(drop.LootTableId);
                if (table == null)
                {
                    continue;
                }
                // If the player doesn't meet the drop conditions, don't add the table to the flattened list
                if (forPlayer != null && !Conditions.MeetsConditionLists(table.DropConditions, forPlayer, null))
                {
                    continue;
                }
                flattenedDrops.AddRange(FlattenDropTable(table.Drops, forPlayer));
            }

            return flattenedDrops;
        }

        public static void SpawnItemsOnMap(List<Item> rolledItems, Guid mapId, Guid mapInstanceId, int X, int Y, Guid lootOwner, bool sendUpdate = true, long ownershipTime = -1)
        {
            foreach (var rolledItem in rolledItems)
            {
                if (rolledItem == null)
                {
                    continue;
                }
                // Set the attributes for this item.
                rolledItem.Set(new Item(rolledItem.ItemId, rolledItem.Quantity, rolledItem.ItemProperties));

                // Spawn the actual item!
                if (MapController.TryGetInstanceFromMap(mapId, mapInstanceId, out var instance))
                {
                    instance.SpawnItem(X, Y, rolledItem, rolledItem.Quantity, lootOwner, sendUpdate, ownershipTimeOverride: ownershipTime);
                }
            }
        }
    }
}
