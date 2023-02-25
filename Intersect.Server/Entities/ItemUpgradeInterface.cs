using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using Intersect.GameObjects.Events;
using Intersect.Server.Database;
using Intersect.Server.Entities.Events;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Entities
{
    public sealed partial class ItemUpgradeInterface
    {
        public Guid CurrencyId { get; set; }

        private ItemBase Currency => ItemBase.Get(CurrencyId);

        public float CostModifier { get; set; }

        private Player Owner { get; set; }

        public ItemUpgradeInterface(Guid currencyId, float costModifier, Player owner)
        {
            CurrencyId = currencyId;
            CostModifier = costModifier;
            Owner = owner;
        }

        public bool TryUpgradeItem(Item item, Guid craftId, out string failureReason)
        {
            failureReason = string.Empty;

            if (item == default)
            {
                failureReason = "Invalid upgrade item!";
                return false;
            }

            var itemDesc = item.Descriptor;
            if (itemDesc == default)
            {
                failureReason = "Invalid descriptor for equipped weapon!";
                return false;
            }

            if (!itemDesc.WeaponUpgrades.ContainsKey(craftId))
            {
                failureReason = "The requested upgrade can not be applied to the given item!";
                return false;
            }

            var craft = CraftBase.Get(craftId);
            if (craft == default)
            {
                failureReason = "The requested craft is invalid!";
                return false;
            }

            var upgradeItem = ItemBase.Get(craft.ItemId);
            if (upgradeItem == default)
            {
                failureReason = "The upgraded item does not exist!";
                return false;
            }

            // Make sure crafting requirements are met
            if (!Conditions.MeetsConditionLists(craft.Requirements, Owner, null))
            {
                failureReason = "You do not meet the upgrade's crafting requirements!";
                return false;
            }

            // Make sure we have any required recipe unlocked
            if (craft.Recipe != Guid.Empty && !Owner.UnlockedRecipeIds.Contains(craft.Recipe))
            {
                var recipe = RecipeDescriptor.GetName(craft.Recipe);
                failureReason = $"This upgrade requires the following recipe: {recipe}!";
                return false;
            }

            // Make sure the player has the required items
            if (!Owner.CheckHasCraftIngredients(craft.Id, Player.GetAllItemsAndQuantities(Owner.Items)))
            {
                failureReason = "You lack the items required to craft this upgrade!";
                return false;
            }

            // Do we have the money?
            var price = (int)Math.Floor(itemDesc.WeaponUpgrades[craftId] * CostModifier);
            var moneySlot = Owner.FindInventoryItemSlot(CurrencyId, price);
            if (!Owner.TryTakeItem(moneySlot, price, ItemHandling.Normal, true))
            {
                PacketSender.SendEventDialog(Owner, $"You don't have enough {ItemBase.GetName(CurrencyId)} to make this upgrade.", string.Empty, Guid.Empty);
                return false;
            }

            // Process craft materials
            var invbackup = new List<Item>();
            foreach (var backupItem in Owner.Items)
            {
                invbackup.Add(backupItem.Clone());
            }
            foreach (var c in craft.Ingredients)
            {
                // If we fail to take any of the items...
                if (!Owner.TryTakeItem(c.ItemId, c.Quantity))
                {
                    // Refund the backups
                    for (var i = 0; i < invbackup.Count; i++)
                    {
                        Owner.Items[i].Set(invbackup[i]);
                    }

                    // and alert the client
                    PacketSender.SendInventory(Owner);

                    failureReason = "You lack the items required to craft this upgrade!";
                    return false;
                }
            }

            string itemName = ItemBase.GetName(craft.ItemId);
            PacketSender.SendChatMsg(
                Owner, Strings.Crafting.crafted.ToString(itemName), ChatMessageType.Crafting,
                CustomColors.Alerts.Success
            );

            // Update our record of how many of this item we've crafted
            long recordCrafted = Owner.IncrementRecord(RecordType.ItemCrafted, craftId);
            if (Options.SendCraftingRecordUpdates && recordCrafted % Options.CraftingRecordUpdateInterval == 0)
            {
                Owner.SendRecordUpdate(Strings.Records.itemcrafted.ToString(recordCrafted, itemName));
            }

            // Start any related common events
            if (craft.Event != null)
            {
                Owner.EnqueueStartCommonEvent(craft.Event);
            }

            // Upgrade the item to the new item!
            if (Owner.EquippedItems.Contains(item))
            {
                // If the item was equipped, unequip it, upgrade it, and re-equip it
                var slotIdx = -1;
                if (item.Descriptor.EquipmentSlot >= 0 && item.Descriptor.EquipmentSlot < Owner.Equipment.Length)
                {
                    slotIdx = Owner.Equipment[item.Descriptor.EquipmentSlot];
                }

                Owner.UnequipItem(item.ItemId, false);
                
                var newItem = new Item(craft.ItemId, 1, item.ItemProperties);
                item.Set(newItem);
                
                Owner.EquipItem(newItem.Descriptor, slotIdx);
            }
            else
            {
                // Otherwise, just upgrade the item
                var newItem = new Item(craft.ItemId, 1, item.ItemProperties);
                item.Set(newItem);
            }

            Owner.StartCommonEventsWithTrigger(CommonEventTrigger.InventoryChanged);

            // Send the final inv update so that they can see their new item
            PacketSender.SendInventory(Owner);
            return true;
        }
    }
}
