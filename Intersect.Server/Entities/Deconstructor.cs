using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Networking;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Entities
{
    public class Deconstructor
    {
        public float FuelMultiplier { get; set; }

        public List<Item> Items { get; set; }

        public Player Owner { get; set; }

        public bool BankingDisabled { get; set; }

        public Deconstructor(float multiplier, Player owner, bool bankingDisabled)
        {
            FuelMultiplier = multiplier;
            Owner = owner;
            BankingDisabled = bankingDisabled;
        }

        /// <summary>
        /// Creates fuel out of valid items.
        /// </summary>
        /// <param name="fuelItems">A map of invSlot => quantity, of items</param>
        /// <returns>The amount of new fuel created</returns>
        public int CreateFuel(Dictionary<int, int> fuelItems)
        {
            int fuelCreated = 0;
            foreach (var kv in fuelItems)
            {
                var invSlot = kv.Key;
                var quantity = kv.Value;

                var slot = Owner.Items.ElementAtOrDefault(invSlot);
                if (slot == default || slot.ItemId == Guid.Empty)
                {
                    continue;
                }

                var item = ItemBase.Get(slot.ItemId);
                if (item.Fuel == 0)
                {
                    continue;
                }

                if (!Owner.TryTakeItem(slot, quantity, Enums.ItemHandling.Normal, sendUpdate: false))
                {
                    continue;
                }

                fuelCreated += item.Fuel * quantity;
            }

            Owner.Fuel += fuelCreated;
            PacketSender.SendInventory(Owner);
            PacketSender.SendFuelPacket(Owner);

            return fuelCreated;
        }

        public void DeconstructItemsInSlots(int[] slots)
        {
            int fuelRequired = 0;
            List<InventorySlot> slotsToRemoveFrom = new List<InventorySlot>();

            if (slots?.Length <= 0)
            {
                return;
            }

            // First, calculate fuel cost and assemble the list of InventorySlots
            foreach (var slot in slots)
            {   
                var invSlot = Owner.Items.ElementAtOrDefault(slot);
                if (invSlot == default || invSlot.ItemId == Guid.Empty)
                {
                    continue;
                }

                var item = ItemBase.Get(invSlot.ItemId);
                if (item.FuelRequired <= 0)
                {
                    continue;
                }

                fuelRequired += item.FuelRequired;
                slotsToRemoveFrom.Add(invSlot);
            }

            fuelRequired = (int)Math.Round(fuelRequired * FuelMultiplier);
            if (fuelRequired > Owner.Fuel)
            {
                PacketSender.SendChatMsg(Owner, "You don't have enough fuel to deconstruct these items!", Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
                return;
            }

            // Now, for each item we successfully deconstruct (remove), add to our loot table/weapon progress
            List<LootRoll> deconstructedLoot = new List<LootRoll>();
            Dictionary<Guid, long> expEarned = new Dictionary<Guid, long>();
            List<Guid> enhancementsLearned = new List<Guid>();
            List<Guid> cosmeticsEarned = new List<Guid>();
            foreach (var slot in slotsToRemoveFrom)
            {
                var item = ItemBase.Get(slot.ItemId);
                if (!Owner.TryTakeItem(slot, 1, Enums.ItemHandling.Normal, sendUpdate: false))
                {
                    fuelRequired -= item.FuelRequired;
                    continue;
                }


                if (item.EquipmentSlot == Options.WeaponIndex)
                {
                    var earned = Owner.AddCraftWeaponExp(item, Options.Instance.DeconstructionOpts.DeconstructionExpMod, false);
                    foreach (var kv in earned)
                    {
                        if (!expEarned.ContainsKey(kv.Key))
                        {
                            expEarned[kv.Key] = kv.Value;
                        }
                        else
                        {
                            expEarned[kv.Key] += kv.Value;
                        }
                    }
                }
                deconstructedLoot.AddRange(item.DeconstructRolls);

                if (item.StudySuccessful(Owner.GetLuckModifier()) && !enhancementsLearned.Contains(item.StudyEnhancement))
                {
                    enhancementsLearned.Add(item.StudyEnhancement);
                }
                
                if (item.ArmorCosmeticUnlocked(Owner.GetLuckModifier()) && !cosmeticsEarned.Contains(item.Id))
                {
                    cosmeticsEarned.Add(item.Id);
                }
            }

            // Remove fuel after going through each deconstructed item
            Owner.Fuel -= fuelRequired;

            foreach (var kv in expEarned)
            {
                var weaponType = kv.Key;
                var exp = kv.Value;
                if (Owner.TrackedWeaponType == weaponType)
                {
                    Owner.TrackWeaponTypeProgress(weaponType);
                    PacketSender.SendExperience(Owner);
                }

                var name = WeaponTypeDescriptor.Get(weaponType)?.DisplayName ?? WeaponTypeDescriptor.GetName(weaponType);

                PacketSender.SendExpToast(Owner, $"{exp} {name} EXP", false, false, true);
            }

            var newEnhancement = false;
            foreach (var enhancement in enhancementsLearned.Distinct())
            {
                if (Owner.TryUnlockEnhancement(enhancement))
                {
                    newEnhancement = true;
                }
            }
            
            var newCosmetic = false;
            foreach (var cosmetic in cosmeticsEarned.Distinct())
            {
                // This function will alert the client
                if (Owner.TryChangeCosmeticUnlockStatus(cosmetic, true))
                {
                    newCosmetic = true;
                }
            }

            // If the client needs to know we've learned a new enhancement
            if (newEnhancement)
            {
                PacketSender.SendKnownEnhancementUpdate(Owner);
            }

            if (newCosmetic)
            {
                PacketSender.SendUnlockedCosmeticsPacket(Owner);
            }

            // We are now waiting on the loot roll instead of the deconstructor being closed
            PacketSender.SendInventory(Owner);
            Owner.SendPacket(new CloseDeconstructorPacket());
            Owner.OpenLootRoll(Owner.DeconstructorEventId, deconstructedLoot);
            Owner.CloseDeconstructor();
            PacketSender.SendOpenLootPacketTo(Owner, "Deconstruction", GameObjects.Events.LootAnimType.Deconstruct, BankingDisabled);
        }
    }
}
