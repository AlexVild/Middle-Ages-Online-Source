﻿using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Intersect.Server.Entities
{
    public class BankInterface
    {
        private Player mPlayer;

        private IList<Item> mBank;

        private Guild mGuild;

        private object mLock;

        private int mMaxSlots;

        private int mBankValue;

        public BankInterface(Player player, IList<Item> bank, object bankLock, Guild guild, int maxSlots)
        {
            mPlayer = player;
            mBank = bank;
            mGuild = guild;
            mLock = bankLock;
            mMaxSlots = maxSlots;
            mBankValue = 0;
        }

        public void SendOpenBank()
        {
            var items = new List<BankUpdatePacket>();
            mBankValue = 0;

            for (var slot = 0; slot < mMaxSlots; slot++)
            {
                if (mBank[slot] != null && mBank[slot].ItemId != Guid.Empty && mBank[slot].Quantity > 0)
                {
                    items.Add(
                        new BankUpdatePacket(
                            slot, mBank[slot].ItemId, mBank[slot].Quantity, mBank[slot].BagId,
                            mBank[slot].StatBuffs
                        )
                    );
                    var item = ItemBase.Get(mBank[slot].ItemId);
                    mBankValue += item.Price * mBank[slot].Quantity;
                }
                else
                {
                    items.Add(new BankUpdatePacket(slot, Guid.Empty, 0, null, null));
                }
            }

            mPlayer?.SendPacket(new BankPacket(false, mGuild != null, mMaxSlots, items.ToArray(), mBankValue));
        }

        //BankUpdatePacket
        public void SendBankUpdate(int slot, bool sendToAll = true)
        {
            if (sendToAll && mGuild != null)
            {
                mGuild.BankSlotUpdated(slot);
                return;
            }

            if (mBank[slot] != null && mBank[slot].ItemId != Guid.Empty && mBank[slot].Quantity > 0)
            {
                mPlayer?.SendPacket(
                    new BankUpdatePacket(
                        slot, mBank[slot].ItemId, mBank[slot].Quantity, mBank[slot].BagId,
                        mBank[slot].StatBuffs
                    )
                );
            }
            else
            {
                mPlayer?.SendPacket(new BankUpdatePacket(slot, Guid.Empty, 0, null, null));
            }

            var oldValue = mBankValue;
            mBankValue = CalculateBankValue();
            if (oldValue != mBankValue)
            {
                mPlayer?.SendPacket(new BankUpdateValuePacket(mBankValue));
            }
        }

        public void SendCloseBank()
        {
            mPlayer?.SendPacket(new BankPacket(true, false, -1, null, -1));
        }


        /// <summary>
        /// Retrieves a list of open bank slots for this instance.
        /// </summary>
        /// <returns>Returns a list of open slots in this bank instance.</returns>
        public List<int> FindOpenSlots()
        {
            var slots = new List<int>();
            for (var i = 0; i < mMaxSlots; i++)
            {
                var bankSlot = mBank[i];

                if (bankSlot != null && bankSlot.ItemId == Guid.Empty)
                {
                    slots.Add(i);
                }
            }

            return slots;
        }

        /// <summary>
        /// Retrieves a list of open bank slots for this instance.
        /// </summary>
        /// <returns>Returns the first open bank slot of this instance, or -1 if none are found.</returns>
        public int FindOpenSlot()
        {
            var slots = FindOpenSlots();
            if (slots.Count >= 1)
            {
                return slots.First();
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Finds a bank slot matching the desired item and quantity.
        /// </summary>
        /// <param name="itemId">The item Id to look for.</param>
        /// <param name="quantity">The quantity of the item to look for.</param>
        /// <returns>Returns a slot that contains the item, or -1 if none are found.</returns>
        public int FindItemSlot(Guid itemId, int quantity = 1)
        {
            var slots = FindItemSlots(itemId, quantity);
            if (slots.Count >= 1)
            {
                return slots.First();
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Finds all bank slots matching the desired item and quantity.
        /// </summary>
        /// <param name="itemId">The item Id to look for.</param>
        /// <param name="quantity">The quantity of the item to look for.</param>
        /// <returns>Returns a list of slots containing the requested item.</returns>
        public List<int> FindItemSlots(Guid itemId, int quantity = 1)
        {
            var slots = new List<int>();
            if (mBank == null)
            {
                return slots;
            }

            for (var i = 0; i < mMaxSlots; i++)
            {
                var item = mBank[i];
                if (item?.ItemId != itemId)
                {
                    continue;
                }

                if (item.Quantity >= quantity)
                {
                    slots.Add(i);
                }
            }

            return slots;
        }

        /// <summary>
        /// Checks whether or not the bank instance can store this item.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to check for.</param>
        /// <returns>Returns whether or not the bank instance can store this item.</returns>
        public bool CanStoreItem(Item item)
        {
            if (item.Descriptor != null)
            {
                // Is the item stackable?
                if (item.Descriptor.IsStackable)
                {
                    // Does the bank have this item already?
                    var slot = FindItemSlot(item.ItemId);
                    if (slot >= 0)
                    {
                        var existingSlot = mBank[slot];

                        // Can we blindly add more to this stack?
                        var untilFull = item.Descriptor.MaxBankStack - existingSlot.Quantity;
                        if (untilFull >= item.Quantity)
                        {
                            return true;
                        }

                        // Check to see if we have the inventory spaces required to hand these items out AFTER filling up the existing slot!
                        var toGive = item.Quantity - untilFull;
                        if (Math.Ceiling((double)toGive / item.Descriptor.MaxBankStack) <= FindOpenSlots().Count)
                        {
                            return true;
                        }
                    }
                    // User doesn't have this item yet.
                    else
                    {
                        // Does the user have enough free space for these stacks?
                        if (Math.Ceiling((double)item.Quantity / item.Descriptor.MaxBankStack) <= FindOpenSlots().Count)
                        {
                            return true;
                        }
                    }
                }
                // Not a stacking item, so can we contain the amount we want to give them?
                else
                {
                    if (FindOpenSlots().Count >= item.Quantity)
                    {
                        return true;
                    }
                }
            }

            // Nothing matches in here, give up!
            return false;
        }

        /// <summary>
        /// Find the amount of a specific item this bank instance has.
        /// </summary>
        /// <param name="itemId">The item Id to look for.</param>
        /// <returns>The amount of the requested item this bank instance contains.</returns>
        public int FindItemQuantity(Guid itemId)
        {
            if (mBank == null)
            {
                return 0;
            }

            long itemCount = 0;
            for (var i = 0; i < mMaxSlots; i++)
            {
                var item = mBank[i];
                if (item.ItemId == itemId)
                {
                    itemCount = item.Descriptor.Stackable ? itemCount += item.Quantity : itemCount += 1;
                }
            }

            // TODO: Stop using Int32 for item quantities
            return itemCount >= Int32.MaxValue ? Int32.MaxValue : (int)itemCount;
        }

        /// <summary>
        /// Checks whether or not this bank instance has enough items to accept a take operation.
        /// </summary>
        /// <param name="itemId">The ItemId to see if it can be taken away.</param>
        /// <param name="quantity">The quantity of above item to see if we can take away.</param>
        /// <returns>Whether or not the item can be taken away from the bank in the requested quantity.</returns>
        public bool CanTakeItem(Guid itemId, int quantity) => FindItemQuantity(itemId) >= quantity;

        public bool TryDepositItem(int slot, int amount, bool sendUpdate = true)
        {
            //Permission Check
            if (mGuild != null)
            {
                var rank = Options.Instance.Guild.Ranks[Math.Max(0, Math.Min(Options.Instance.Guild.Ranks.Length - 1, mPlayer.GuildRank))];
                if (!rank.Permissions.BankDeposit && mPlayer.GuildRank != 0)
                {
                    PacketSender.SendChatMsg(mPlayer, Strings.Guilds.NotAllowedDeposit.ToString(mGuild.Name), ChatMessageType.Bank, CustomColors.Alerts.Error);
                    return false;
                }
            }

            var itemBase = mPlayer.Items[slot].Descriptor;
            if (itemBase != null)
            {
                if (mPlayer.Items[slot].ItemId != Guid.Empty)
                {
                    if ((!itemBase.CanBank && mGuild == null) || (!itemBase.CanGuildBank && mGuild != null))
                    {
                        PacketSender.SendChatMsg(mPlayer, Strings.Items.nobank, ChatMessageType.Bank, CustomColors.Items.Bound);
                        return false;
                    }

                    lock (mLock)
                    {
                        // if stackable, make sure the user actually has this many items or more.
                        if (itemBase.IsStackable)
                        {
                            if (!mPlayer.CanTakeItem(itemBase.Id, amount))
                            {
                                amount = mPlayer.FindInventoryItemQuantity(itemBase.Id);
                            }
                        }
                        else
                        {
                            amount = 1;
                        }

                        if (CanStoreItem(new Item(itemBase.Id, amount)))
                        {
                            if (itemBase.IsStackable)
                            {
                                PutItem(new Item(itemBase.Id, amount), sendUpdate);
                                mPlayer.TryTakeItem(itemBase.Id, amount, ItemHandling.Normal, sendUpdate);

                                if (mGuild != null)
                                {
                                    DbInterface.Pool.QueueWorkItem(mGuild.Save);
                                }

                                return true;
                            }
                            else
                            {
                                PutItem(mPlayer.Items[slot], sendUpdate);

                                mPlayer.Items[slot].Set(Item.None);
                                mPlayer.EquipmentProcessItemLoss(slot);

                                if (sendUpdate)
                                {
                                    PacketSender.SendInventoryItemUpdate(mPlayer, slot);
                                }

                                if (mGuild != null)
                                {
                                    DbInterface.Pool.QueueWorkItem(mGuild.Save);
                                }

                                return true;
                            }
                        }
                        else
                        {
                            PacketSender.SendChatMsg(mPlayer, Strings.Banks.banknospace, ChatMessageType.Bank, CustomColors.Alerts.Error);
                        }  
                    }
                }
                else
                {
                    PacketSender.SendChatMsg(mPlayer, Strings.Banks.depositinvalid, ChatMessageType.Bank, CustomColors.Alerts.Error);
                }
            }

            return false;
        }

        public bool TryDepositItem(Item item, bool sendUpdate = true)
        {
            //Permission Check
            if (mGuild != null)
            {
                var rank = Options.Instance.Guild.Ranks[Math.Max(0, Math.Min(Options.Instance.Guild.Ranks.Length - 1, mPlayer.GuildRank))];
                if (!rank.Permissions.BankDeposit && mPlayer.GuildRank != 0)
                {
                    PacketSender.SendChatMsg(mPlayer, Strings.Guilds.NotAllowedDeposit.ToString(mGuild.Name), ChatMessageType.Bank, CustomColors.Alerts.Error);
                    return false;
                }
            }

            if (item == null)
            {
                return true;
            }

            var itemBase = item.Descriptor;

            if (itemBase == null)
            {
                return false;
            }

            if (item.ItemId != Guid.Empty)
            {
                if ((!itemBase.CanBank && mGuild == null) || (!itemBase.CanGuildBank && mGuild != null))
                {
                    PacketSender.SendChatMsg(mPlayer, Strings.Items.nobank, ChatMessageType.Bank, CustomColors.Items.Bound);
                    return false;
                }

                lock (mLock)
                {
                    if (CanStoreItem(item))
                    {
                        PutItem(item, sendUpdate);
                        return true;
                    }
                    else
                    {
                        PacketSender.SendChatMsg(mPlayer, Strings.Banks.banknospace, ChatMessageType.Bank, CustomColors.Alerts.Error);
                    }
                }
            }
            else
            {
                PacketSender.SendChatMsg(mPlayer, Strings.Banks.depositinvalid, ChatMessageType.Bank, CustomColors.Alerts.Error);
            }

            return false;
        }

        /// <summary>
        /// Puts an item in the bank. NOTE: This method MAKES ZERO CHECKS to see if this is possible!
        /// Use TryDepositItem where possible!
        /// </summary>
        /// <param name="item"></param>
        /// <param name="sendUpdate"></param>
        private void PutItem(Item item, bool sendUpdate)
        {
            // Decide how we're going to handle this item.
            var existingSlots = FindItemSlots(item.Descriptor.Id);
            var updateSlots = new List<int>();
            if (item.Descriptor.Stackable && existingSlots.Count > 0) // Stackable, but already exists in the inventory.
            {
                // So this gets complicated.. First let's hand out the quantity we can hand out before we hit a stack limit.
                var toGive = item.Quantity;
                foreach (var slot in existingSlots)
                {
                    if (toGive == 0)
                    {
                        break;
                    }

                    if (mBank[slot].Quantity >= item.Descriptor.MaxBankStack)
                    {
                        continue;
                    }

                    var canAdd = item.Descriptor.MaxBankStack - mBank[slot].Quantity;
                    if (canAdd > toGive)
                    {
                        mBank[slot].Quantity += toGive;
                        updateSlots.Add(slot);
                        toGive = 0;
                    }
                    else
                    {
                        mBank[slot].Quantity += canAdd;
                        updateSlots.Add(slot);
                        toGive -= canAdd;
                    }
                    
                }

                // Is there anything left to hand out? If so, hand out max stacks and what remains until we run out!
                if (toGive > 0)
                {
                    var openSlots = FindOpenSlots();
                    var total = toGive; // Copy this as we're going to be editing toGive.
                    for (var slot = 0; slot < Math.Ceiling((double)total / item.Descriptor.MaxBankStack); slot++)
                    {
                        var quantity = item.Descriptor.MaxBankStack <= toGive ?
                            item.Descriptor.MaxBankStack :
                            toGive;

                        toGive -= quantity;
                        mBank[openSlots[slot]].Set(new Item(item.ItemId, quantity));
                        updateSlots.Add(openSlots[slot]);
                    }
                }
            }
            else if (!item.Descriptor.Stackable && item.Quantity > 1) // Not stackable, but multiple items.
            {
                var openSlots = FindOpenSlots();
                for (var slot = 0; slot < item.Quantity; slot++)
                {
                    mBank[openSlots[slot]].Set(new Item(item.ItemId, 1));
                    updateSlots.Add(openSlots[slot]);
                }
            }
            else // Hand out without any special treatment. Either a single item or a stackable item we don't have yet.
            {
                // If the item is not stackable, or the amount is below our stack cap just blindly hand it out.
                if (!item.Descriptor.Stackable || item.Quantity < item.Descriptor.MaxBankStack)
                {
                    var newSlot = FindOpenSlot();
                    mBank[newSlot].Set(item);
                    updateSlots.Add(newSlot);
                }
                // The item is above our stack cap.. Let's start handing them phat stacks out!
                else
                {
                    var toGive = item.Quantity;
                    var openSlots = FindOpenSlots();
                    for (var slot = 0; slot < Math.Ceiling((double)item.Quantity / item.Descriptor.MaxBankStack); slot++)
                    {
                        var quantity = item.Descriptor.MaxBankStack <= toGive ?
                            item.Descriptor.MaxBankStack :
                            toGive;

                        toGive -= quantity;
                        mBank[openSlots[slot]].Set(new Item(item.ItemId, quantity));
                        updateSlots.Add(openSlots[slot]);
                    }
                }

            }

            if (mPlayer?.ItemsDiscovered.Add(new ItemDiscoveryInstance(mPlayer.Id, item.ItemId)) ?? false)
            {
                Logging.Log.Debug($"Added item to {mPlayer.Name}'s discovered list");
            }

            // Do we need to update the bank display?
            if (sendUpdate)
            {
                foreach (var slot in updateSlots)
                {
                    SendBankUpdate(slot);
                }

                if (mGuild != null)
                {
                    DbInterface.Pool.QueueWorkItem(mGuild.Save);
                }
            }
        }

        public void WithdrawItem(int slot, int amount)
        {
            //Permission Check
            if (mGuild != null)
            {
                var rank = Options.Instance.Guild.Ranks[Math.Max(0, Math.Min(Options.Instance.Guild.Ranks.Length - 1, mPlayer.GuildRank))];
                if (!rank.Permissions.BankRetrieve && mPlayer.GuildRank != 0)
                {
                    PacketSender.SendChatMsg(mPlayer, Strings.Guilds.NotAllowedWithdraw.ToString(mGuild.Name), ChatMessageType.Bank, CustomColors.Alerts.Error);
                    return;
                }
            }

            Debug.Assert(ItemBase.Lookup != null, "ItemBase.Lookup != null");
            Debug.Assert(mBank != null, "Bank != null");
            Debug.Assert(mPlayer.Items != null, "Inventory != null");

            var bankSlotItem = mBank[slot];
            if (bankSlotItem == null)
            {
                return;
            }

            var itemBase = bankSlotItem.Descriptor;
            var inventorySlot = -1;
            if (itemBase == null)
            {
                return;
            }

            if (bankSlotItem.ItemId != Guid.Empty)
            {
                lock (mLock)
                {
                    if (itemBase.IsStackable)
                    {
                        if (!CanTakeItem(itemBase.Id, amount))
                        {
                            amount = FindItemQuantity(itemBase.Id);
                        }
                    }
                    else
                    {
                        amount = 1;
                    }

                    if (!mPlayer.CanGiveItem(itemBase.Id, amount))
                    {
                        PacketSender.SendChatMsg(mPlayer, Strings.Banks.inventorynospace, ChatMessageType.Inventory, CustomColors.Alerts.Error);
                        return;
                    }

                    var toTake = amount;
                    if (itemBase.IsStackable)
                    {
                        mPlayer.TryGiveItem(itemBase.Id, amount, ItemHandling.Normal, false, true);

                        // Go through our bank and take what we need!
                        foreach (var s in FindItemSlots(itemBase.Id))
                        {
                            // Do we still have items to take? If not leave the loop!
                            if (toTake == 0)
                            {
                                break;
                            }

                            if (mBank[s].Quantity >= toTake)
                            {
                                TakeItem(s, toTake, true);
                                toTake = 0;
                            }
                            else // Take away the entire quantity of the item and lower our items that we still need to take!
                            {
                                toTake -= mBank[s].Quantity;
                                TakeItem(s, mBank[s].Quantity, true);
                            }
                        }
                    }
                    else
                    {
                        mPlayer.TryGiveItem(mBank[slot]);
                        mBank[slot].Set(Item.None);
                        SendBankUpdate(slot);
                    }

                    if (mGuild != null)
                    {
                        DbInterface.Pool.QueueWorkItem(mGuild.Save);
                    }
                }
            }
            else
            {
                PacketSender.SendChatMsg(mPlayer, Strings.Banks.withdrawinvalid, ChatMessageType.Bank, CustomColors.Alerts.Error);
            }
        }

        /// <summary>
        /// Take an item away from the bank, or an amount of it if they have more. NOTE: This method MAKES ZERO CHECKS to see if this is possible!
        /// Use WithdrawItem where possible!
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="amount"></param>
        /// <param name="sendUpdate"></param>
        private void TakeItem(int slot, int amount, bool sendUpdate = true)
        {
            if (mBank[slot].Quantity > amount) // This slot contains more than what we're trying to take away here. Update the quantity.
            {
                mBank[slot].Quantity -= amount;
            }
            else // Take the entire thing away!
            {
                mBank[slot].Set(Item.None);
            }

            if (sendUpdate)
            {
                SendBankUpdate(slot);
            }
        }

        public void SwapBankItems(int item1, int item2, bool sendUpdate = true)
        {
            //Permission Check
            if (mGuild != null)
            {
                var rank = Options.Instance.Guild.Ranks[Math.Max(0, Math.Min(Options.Instance.Guild.Ranks.Length - 1, mPlayer.GuildRank))];
                if (!rank.Permissions.BankMove && mPlayer.GuildRank != 0)
                {
                    PacketSender.SendChatMsg(mPlayer, Strings.Guilds.NotAllowedSwap.ToString(mGuild.Name), ChatMessageType.Bank, CustomColors.Alerts.Error);
                    return;
                }
            }

            Item tmpInstance = null;
            lock (mLock)
            {
                if (mBank != null)
                {
                    tmpInstance = mBank[item2].Clone();
                }

                if (mBank[item1] != null)
                {
                    mBank[item2].Set(mBank[item1]);
                }
                else
                {
                    mBank[item2].Set(Item.None);
                }

                if (tmpInstance != null)
                {
                    mBank[item1].Set(tmpInstance);
                }
                else
                {
                    mBank[item1].Set(Item.None);
                }

                if (mGuild != null)
                {
                    DbInterface.Pool.QueueWorkItem(mGuild.Save);
                }
            }

            if (sendUpdate)
            {
                SendBankUpdate(item1);
                SendBankUpdate(item2);
            }
        }

        public void SortBank()
        {
            // This really bubbles my sorts - sort() isn't implemented on ILists, and I was having some horrible things happen if I tried converting mBank to some sortable list, then
            // back again. So tada!
            for (var slot = 0; slot < mMaxSlots - 1; slot++)
            {
                for (var compareSlot = 0; compareSlot < mMaxSlots - slot - 1; compareSlot++)
                {
                    if (CompareBankItems(mBank[compareSlot], mBank[compareSlot + 1]) == -1) // Descending order - hence "-1" here
                    {
                        SwapBankItems(compareSlot, compareSlot + 1, false); // Don't update the client while we're doing this - WAY faster this way, then we can tell them afterward.
                    }
                }
            }

            if (mPlayer != null)
            {
                PacketSender.SendPlaySound(mPlayer, Options.BankSortSound);
            }
            SendOpenBank(); // Refresh the entire bank, so the Client knows what's up.
        }

        private static int CompareBankItems(Item currentItem, Item comparedItem)
        {
            var x = ItemBase.Get(currentItem.ItemId);
            var y = ItemBase.Get(comparedItem.ItemId);

            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                } else
                {
                    return -1;
                }
            } else
            {
                if (y == null)
                {
                    return 1;
                } else
                {
                    if (x.ItemType == y.ItemType)
                    {
                        return x.Name.CompareTo(y.Name) * -1;
                    } else
                    {
                        switch (x.ItemType)
                        {
                            case ItemTypes.Currency:
                                return 1;
                            case ItemTypes.Equipment:
                                if (y.ItemType == ItemTypes.Currency)
                                {
                                    return -1;
                                } else
                                {
                                    return 1;
                                }
                            case ItemTypes.Bag:
                                if (y.ItemType == ItemTypes.Currency || y.ItemType == ItemTypes.Equipment)
                                {
                                    return -1;
                                }
                                else
                                {
                                    return 1;
                                }
                            case ItemTypes.Event:
                                if (y.ItemType == ItemTypes.Currency 
                                    || y.ItemType == ItemTypes.Equipment
                                    || y.ItemType == ItemTypes.Bag)
                                {
                                    return -1;
                                }
                                else
                                {
                                    return 1;
                                }
                            case ItemTypes.Spell:
                                if (y.ItemType == ItemTypes.Currency
                                    || y.ItemType == ItemTypes.Equipment
                                    || y.ItemType == ItemTypes.Bag
                                    || y.ItemType == ItemTypes.Event)
                                {
                                    return -1;
                                }
                                else
                                {
                                    return 1;
                                }
                            case ItemTypes.None:
                                if (y.ItemType != ItemTypes.Consumable)
                                {
                                    return -1;
                                }
                                else
                                {
                                    return 1;
                                }
                            case ItemTypes.Consumable:
                            case ItemTypes.Cosmetic:
                            default:
                                return -1;
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            SendCloseBank();
            mPlayer.GuildBank = false;
            mPlayer.BankInterface = null;
        }

        public int CalculateBankValue()
        {
            var bankVal = 0;
            for (var slot = 0; slot < mMaxSlots - 1; slot++)
            {
                if (mBank[slot] != null && mBank[slot].ItemId != Guid.Empty)
                {
                    bankVal += ItemBase.Get(mBank[slot].ItemId).Price * mBank[slot].Quantity;
                }
            }

            return bankVal;
        }
    }
}
