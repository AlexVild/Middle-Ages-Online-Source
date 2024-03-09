using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Server.Entities
{
    public class EnhancementInterface
    {
        public Player Owner { get; set; }

        public Guid CurrencyId { get; set; }
        
        public float CostMultiplier { get; set; }

        public EnhancementInterface(Player owner, Guid currencyId, float costMultiplier)
        {
            Owner = owner;
            CurrencyId = currencyId;
            CostMultiplier = costMultiplier;
        }

        public bool TryApplyEnhancementsToWeapon(Guid[] enhancementIds, bool sendUiPacket = true, Item weapon = null)
        {
            if (enhancementIds == null || enhancementIds.Length == 0 || Owner == null || !Owner.Online)
            {
                return false;
            }

            if (weapon == null)
            {
                if (!Owner.TryGetWeaponPicked(out weapon))
                {
                    PacketSender.SendChatMsg(Owner, Strings.Enhancements.NoWeapon, Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
                    return false;
                }
            }

            if (!ValidateEnhancements(enhancementIds, weapon, !sendUiPacket))
            {
                PacketSender.SendPlaySound(Owner, Options.UIDenySound);
                return false;
            }

            // Clone the weapon to begin applying changes to it
            var enhancedItem = weapon.Clone();

            // Apply each requested enhancement!
            foreach (var enhancementId in enhancementIds)
            {
                var desc = EnhancementDescriptor.Get(enhancementId);
                ApplyItemEnhancements(desc.StatMods, enhancedItem.ItemProperties.StatEnhancements);
                ApplyItemEnhancements(desc.VitalMods, enhancedItem.ItemProperties.VitalEnhancements);
                ApplyItemEnhancements(desc.EffectMods, enhancedItem.ItemProperties.EffectEnhancements);
                ApplySpellEnhancements(desc.SpellEnhancements, enhancedItem.ItemProperties.SpellEnhancements);

                enhancedItem.ItemProperties.AppliedEnhancementIds.Add(enhancementId);
            }

            // Set the owner as the enhancer
            enhancedItem.ItemProperties.EnhancedBy = Owner.Name;
            
            // Apply enhancements to the existing item
            weapon.Set(enhancedItem);

            PacketSender.SendInventory(Owner);
            Owner.ProcessEquipmentUpdated(true);

            if (sendUiPacket)
            {
                Owner.SendPacket(new EnhancementEndPacket(weapon.ItemProperties));
            }

            return true;
        }

        private bool ValidateEnhancements(Guid[] enhancementIds, Item weapon, bool ignoreCurrencyCost)
        {
            // Do we have enough EP?
            var epAvailable = weapon.Descriptor.EnhancementThreshold - EnhancementHelper.GetEpUsed(weapon.ItemProperties.AppliedEnhancementIds.ToArray());
            var epRequested = EnhancementHelper.GetEpUsed(enhancementIds);
            if (epAvailable < epRequested)
            {
                PacketSender.SendEventDialog(Owner, "The requested enhancements would go over the item's modification threshold.", string.Empty, Guid.Empty);
                return false;
            }

            // Are any of the enhancements we're trying to apply too high-level for the weapon/not apply to the weapon?
            foreach (var enhancementId in enhancementIds)
            {
                var enhancement = EnhancementDescriptor.Get(enhancementId);
                if (!EnhancementHelper.WeaponLevelRequirementMet(weapon.Descriptor.MaxWeaponLevels, enhancement.ValidWeaponTypes, out var failureReason))
                {
                    PacketSender.SendEventDialog(Owner, failureReason, string.Empty, Guid.Empty);
                    return false;
                }
            }

            // Does the owner meet at least _one_ of the weapon's max levels?
            
            /* Commenting this out -- I don't think I like the functionality, especially since now a player can enhance non-equipped weapons
            var ownerMeetsWeaponLevel = weapon.Descriptor.MaxWeaponLevels.Any((kv) =>
            {
                return Owner.TryGetMastery(kv.Key, out var mastery) && mastery.Level >= kv.Value;
            });
            if (!ownerMeetsWeaponLevel)
            {
                List<string> validMasteries = new List<string>();
                foreach (var kv in weapon.Descriptor.MaxWeaponLevels)
                {
                    var masteryId = kv.Key;
                    var maxLevel = kv.Value;
                    validMasteries.Add($"Lvl {maxLevel} {WeaponTypeDescriptor.Get(masteryId)?.VisibleName ?? "NOT FOUND"}");
                }

                if (validMasteries.Count > 1)
                {
                    PacketSender.SendEventDialog(Owner, Strings.Enhancements.InsufficientMasteries.ToString(string.Join(", ", validMasteries)), string.Empty, Guid.Empty);
                }
                else
                {
                    PacketSender.SendEventDialog(Owner, Strings.Enhancements.InsufficientMastery.ToString(string.Join(", ", validMasteries)), string.Empty, Guid.Empty);
                }
                return false;
            }*/

            // Do we have enough guap?

            if (!ignoreCurrencyCost)
            {
                var price = EnhancementHelper.GetEnhancementCostOnWeapon(weapon.Descriptor, enhancementIds, CostMultiplier);
                var moneySlot = Owner.FindInventoryItemSlot(CurrencyId, price);
                if (!Owner.TryTakeItem(moneySlot, price, Enums.ItemHandling.Normal, true))
                {
                    PacketSender.SendEventDialog(Owner, $"You don't have enough {ItemBase.GetName(CurrencyId)} to make the selected enhancements.", string.Empty, Guid.Empty);
                    return false;
                }
            }

            return true;
        }

        private static void ApplyItemEnhancements<T>(List<Enhancement<T>> newEnhancements, int[] itemEnhancements) where T : Enum
        {
            // For each of the new potential enhancements...
            foreach (var enhancement in newEnhancements)
            {
                // Get the randomized range val
                var modVal = Randomization.Next(enhancement.MinValue, enhancement.MaxValue + 1);

                // And apply it to the enhancement array
                var modIdx = Convert.ToInt32(enhancement.EnhancementType); // convert generic enum to int
                if (modIdx < 0 || modIdx >= itemEnhancements.Length)
                {
                    continue;
                }

                itemEnhancements[modIdx] += modVal;
            }
        }

        private static void ApplySpellEnhancements(List<SpellEnhancementDescriptor> newEnhancements, List<SpellEnhancement> spellEnhancements)
        {
            // For each of the new potential enhancements...
            foreach (var enhancement in newEnhancements.ToArray())
            {
                // Get the randomized range val
                var modVal = Randomization.Next(enhancement.MinValue, enhancement.MaxValue + 1);

                var existingSpell = spellEnhancements.Find(spell => spell.SpellId == enhancement.SpellId);
                if (existingSpell != default)
                {
                    existingSpell.Value += modVal;
                    continue;
                }

                spellEnhancements.Add(new SpellEnhancement(enhancement.SpellId, modVal));
            }
        }

        public bool TryRemoveEnhancementsOnItem(bool sendUiPackets = true, bool ignoreCurrency = false, Item item = null)
        {
            if (item == null)
            {
                if (!Owner?.TryGetWeaponPicked(out item) ?? false)
                {
                    return false;
                }
            }

            if (!ignoreCurrency)
            {
                var price = EnhancementHelper.GetEnhancementCostOnWeapon(item.Descriptor, item.ItemProperties.AppliedEnhancementIds.ToArray(), (CostMultiplier * Options.Instance.DeconstructionOpts.RemoveEnhancementCostMod));
                var moneySlot = Owner.FindInventoryItemSlot(Guid.Parse(Options.Instance.PlayerOpts.GoldGuid), price);
                if (!Owner.TryTakeItem(moneySlot, price, Enums.ItemHandling.Normal, true))
                {
                    PacketSender.SendEventDialog(Owner, $"You don't have enough Gold to remove this item's enhancements.", string.Empty, Guid.Empty);
                    return false;
                }
            }

            var removalItem = item.Clone();
            removalItem.ItemProperties.AppliedEnhancementIds.Clear();
            removalItem.ItemProperties.EnhancedBy = string.Empty;

            for (var i = 0; i < removalItem.ItemProperties.StatEnhancements.Length; i++)
            {
                removalItem.ItemProperties.StatEnhancements[i] = default;
            }

            for (var i = 0; i < removalItem.ItemProperties.VitalEnhancements.Length; i++)
            {
                removalItem.ItemProperties.VitalEnhancements[i] = default;
            }

            for (var i = 0; i < removalItem.ItemProperties.EffectEnhancements.Length; i++)
            {
                removalItem.ItemProperties.EffectEnhancements[i] = default;
            }

            removalItem.ItemProperties.SpellEnhancements.Clear();

            item.Set(removalItem);

            PacketSender.SendInventory(Owner);
            Owner.ProcessEquipmentUpdated(true);
            // Just re-send the open command to refresh the UI

            if (sendUiPackets)
            {
                PacketSender.SendOpenEnhancementWindow(Owner, CurrencyId, CostMultiplier);
                PacketSender.SendPlaySound(Owner, Options.Instance.DeconstructionOpts.DisenhanceItemSound);
            }

            return true;
        }

        public void RerollWeapon(Item weapon, Guid[] appliedEnhancementIds)
        {
            TryRemoveEnhancementsOnItem(false, true, weapon);
            TryApplyEnhancementsToWeapon(appliedEnhancementIds, false, weapon);

            PacketSender.SendChatMsg(Owner, Strings.Enhancements.ServerReset, ChatMessageType.Notice, sendToast: true);
        }
    }
}
