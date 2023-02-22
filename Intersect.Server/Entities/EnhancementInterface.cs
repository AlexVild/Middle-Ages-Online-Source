using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;
using System;
using System.Collections.Generic;

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

        public bool TryApplyEnhancementsToWeapon(Guid[] enhancementIds)
        {
            if (enhancementIds == null || enhancementIds.Length == 0 || Owner == null || !Owner.Online)
            {
                return false;
            }

            if (!Owner.TryGetEquippedItem(Options.WeaponIndex, out var weapon))
            {
                PacketSender.SendChatMsg(Owner, Strings.Enhancements.NoWeapon, Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
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

                enhancedItem.ItemProperties.AppliedEnhancementIds.Add(enhancementId);
            }

            // Set the owner as the enhancer
            enhancedItem.ItemProperties.EnhancedBy = Owner.Name;
            
            // Apply enhancements to the existing item
            weapon.Set(enhancedItem);

            PacketSender.SendInventory(Owner);
            Owner.ProcessEquipmentUpdated(true);
            Owner.SendPacket(new EnhancementEndPacket());

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
    }
}
