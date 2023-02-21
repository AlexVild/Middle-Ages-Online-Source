using Intersect.Client.Core;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intersect.Client.General.Deconstructor
{
    public class Deconstructor
    {
        public int Fuel { get; set; }

        /// <summary>
        /// A reference to inventory slots of the player
        /// </summary>
        public ObservableCollection<int> Items { get; set; }
        
        public Dictionary<int, int> FuelItems { get; set; }

        public bool WaitingOnServer { get; set; }

        public bool AddFuel { get; set; }

        public bool IsOpen { get; set; } = false;

        public bool Refresh { get; set; } = false;

        public bool AddingFuel { get; set; } = false;

        public float FuelCostMod { get; set; } = 1.0f;

        public long FuelAddedFlashStamp { get; set; } = 0L;
        readonly long FuelAddFlashTime = 2000;
        long FuelAddLastFlash { get; set; } = 0L;
        public bool FuelFlashing { get; set; } = false;
        readonly long FuelFlashRate = 200;

        public int RequiredFuel => (int)Math.Floor(FuelCostMod * Items.Aggregate(0, (reqFuel, invSlot) => reqFuel + ItemBase.Get((Globals.Me?.Inventory[invSlot]?.ItemId) ?? Guid.Empty)?.FuelRequired ?? 0));

        public Deconstructor()
        {
            FuelItems = new Dictionary<int, int>();

            Items = new ObservableCollection<int>();
            Items.CollectionChanged += Items_CollectionChanged;
        }

        public bool TryAddItem(int invIdx)
        {
            if (Globals.Me == default)
            {
                return false;
            }

            var item = ItemBase.Get(Globals.Me.Inventory[invIdx].ItemId);
            if (item == default)
            {
                return false;
            }

            if (item.FuelRequired <= 0)
            {
                Audio.AddGameSound(Options.UIDenySound, false);
                ChatboxMsg.AddMessage(new ChatboxMsg("This item can not be deconstructed.", CustomColors.Alerts.Error, ChatMessageType.Notice));
                return false;
            }

            if (Items.Contains(invIdx))
            {
                return false;
            }

            Audio.AddGameSound("al_cloth-heavy.wav", false);
            Items.Add(invIdx);
            return true;
        }

        public bool TryRemoveItem(int invIdx)
        {
            if (!Items.Remove(invIdx))
            {
                return false;
            }

            Audio.AddGameSound("al_cloth-heavy.wav", false);
            return true;
        }

        public bool TryAddFuel(int invIdx, int quantity)
        {
            if (Globals.Me == default)
            {
                return false;
            }

            var item = ItemBase.Get(Globals.Me.Inventory[invIdx].ItemId);
            if (item == default)
            {
                return false;
            }

            if (item.Fuel <= 0)
            {
                Audio.AddGameSound(Options.UIDenySound, false);
                ChatboxMsg.AddMessage(new ChatboxMsg("This item can not be used as fuel.", CustomColors.Alerts.Error, ChatMessageType.Notice));
                return false;
            }

            FuelItems[invIdx] = quantity;
            Audio.AddGameSound("al_cloth-heavy.wav", false);
            Refresh = true;
            return true;
        }

        public bool TryRemoveFuel(int invIdx)
        {
            if (!FuelItems.ContainsKey(invIdx))
            {
                return false;
            }

            FuelItems.Remove(invIdx);
            Audio.AddGameSound("al_cloth-heavy.wav", false);
            Refresh = true;
            return true;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Refresh = true;
        }

        public void Open(float costMod)
        {
            FuelCostMod = costMod;
            IsOpen = true;
            Refresh = true;
            Interface.Interface.GameUi?.DeconstructorWindow?.Show();
            Interface.Interface.GameUi?.GameMenu.OpenInventory();
            WaitingOnServer = false; // just force this
        }

        public void Close(bool sendPacket = true)
        {
            AddingFuel = false;
            Items.Clear();
            FuelItems.Clear();
            IsOpen = false;
            Refresh = false;
            Globals.Me.RefreshInventoryItems = true;
            if (sendPacket)
            {
                PacketSender.SendCloseDeconstructorPacket();
            }
        }

        public void OpenFuelAddition()
        {
            AddingFuel = true;
            Refresh = true;
        }

        public void PlayFuelAddEffect()
        {
            Flash.FlashScreen(1000, new Color(200, 145, 62), 180);
            Audio.AddGameSound(Options.Instance.DeconstructionOpts.AddFuelSound, false);

            FuelFlashing = true;
            FuelAddedFlashStamp = Timing.Global.Milliseconds + FuelAddFlashTime;
            FuelAddLastFlash = Timing.Global.Milliseconds;
        }

        public void HandleFuelFlash()
        {
            var now = Timing.Global.Milliseconds;
            if (FuelAddedFlashStamp < now)
            {
                FuelFlashing = false;
                return;
            }

            if (now - FuelAddLastFlash > FuelFlashRate)
            {
                FuelAddLastFlash = now;
                FuelFlashing = !FuelFlashing;
            }
        }

        public void CloseFuelAddition()
        {
            AddingFuel = false;
            FuelItems.Clear();
            Refresh = true;
        }
    }
}
