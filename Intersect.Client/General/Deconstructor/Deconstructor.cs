using Intersect.Client.Core;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using System.Collections.ObjectModel;

namespace Intersect.Client.General.Deconstructor
{
    public class Deconstructor
    {
        public int Fuel { get; set; }

        /// <summary>
        /// A reference to inventory slots of the player
        /// </summary>
        public ObservableCollection<int> Items { get; set; }

        public bool AddFuel { get; set; }

        public bool IsOpen { get; set; } = false;

        public bool Refresh { get; set; } = false;

        public Deconstructor()
        {
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

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Refresh = true;
        }

        public void Open()
        {
            IsOpen = true;
            Refresh = true;
            Interface.Interface.GameUi?.DeconstructorWindow?.Show();
        }

        public void Close()
        {
            IsOpen = false;
            Items.Clear();
            PacketSender.SendCloseDeconstructorPacket();
        }
    }
}
