using System;
using System.Collections.Generic;

using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.GameObjects;
using Intersect.Client.Networking;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Items;
using System.Linq;
using Intersect.Client.Utilities;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game.Bank
{

    public partial class BankWindow
    {

        private static int sItemXPadding = 4;

        private static int sItemYPadding = 4;

        public List<BankItem> Items = new List<BankItem>();

        //Controls
        private WindowControl mBankWindow;

        private ScrollControl mItemContainer;

        private List<Label> mValues = new List<Label>();

        private Button mSortButton;

        private Label mValueLabel;

        //Location
        public int X;

        public int Y;

        private bool mOpen;

        //Init
        public BankWindow(Canvas gameCanvas)
        {
            mBankWindow = new WindowControl(gameCanvas, Globals.GuildBank ? Strings.Guilds.Bank.ToString(Globals.Me?.Guild) : Strings.Bank.title.ToString(), false, "BankWindow");
            mBankWindow.DisableResizing();
            Interface.InputBlockingElements.Add(mBankWindow);

            mItemContainer = new ScrollControl(mBankWindow, "ItemContainer");
            mItemContainer.EnableScroll(false, true);

            mSortButton = new Button(mBankWindow, "SortButton");
            mSortButton.SetText(Strings.Bank.sort);
            mSortButton.Clicked += sort_Clicked;

            mValueLabel = new Label(mBankWindow, "ValueLabel");
            mValueLabel.SetText(Strings.Bank.bankvalue.ToString(Strings.FormatQuantityAbbreviated(Globals.BankValue)));
            mValueLabel.SetToolTipText(Strings.Bank.bankvalue.ToString(Globals.BankValue.ToString("N0").Replace(",", Strings.Numbers.comma)));
            
            mBankWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            _BankWindow();

            InitItemContainer();
            Close();
        }

        public void Close()
        {
            mBankWindow.IsHidden = true;
            mOpen = false;
            _Close();
        }

        public void Open()
        {
            // Hide unavailable bank slots
            var currentBankSlots = Math.Max(0, Globals.BankSlots);
            // For any slot beyond the current bank's maximum slots
            for (var i = currentBankSlots; i < Options.Instance.Bank.MaxSlots; i++)
            {
                var bankItem = Items[i];
                var bankLabel = mValues[i];

                bankItem.Container.Hide();
                bankLabel.Hide();

                // Position this invisible BankItem at 0,0 so the scrollbar doesn't think we have more slots than we do
                SetItemPosition(i);
            }

            mBankWindow.IsHidden = false;
            mOpen = true;
            FillSortedBank();
        }

        public bool IsVisible()
        {
            return !mBankWindow.IsHidden;
        }

        public void Update()
        {
            if (mBankWindow.IsHidden == true)
            {
                if (mOpen)
                {
                    Interface.GameUi.NotifyCloseBank();
                }

                return;
            }

            UpdateBank();
            mValueLabel.SetText(Strings.Bank.bankvalue.ToString(Strings.FormatQuantityAbbreviated(Globals.BankValue)));
            mValueLabel.SetToolTipText(Strings.Bank.bankvaluefull.ToString(Globals.BankValue.ToString("N0").Replace(",", Strings.Numbers.comma)));
            X = mBankWindow.X;
            Y = mBankWindow.Y;
            for (var i = 0; i < Math.Min(Globals.BankSlots, Options.Instance.Bank.MaxSlots); i++)
            {
                var bankItem = Items[i];
                var bankLabel = mValues[i];
                var globalBankItem = SortedBank[i]?.Item;

                bankItem.Container.Show();
                SetItemPosition(i);
                if (globalBankItem != null && globalBankItem.ItemId != Guid.Empty)
                {
                    var item = ItemBase.Get(globalBankItem.ItemId);
                    if (item != null)
                    {
                        bankItem.Pnl.IsHidden = false;
                        if (item.IsStackable)
                        {
                            bankLabel.IsHidden = globalBankItem.Quantity <= 1;
                            bankLabel.Text = Strings.FormatQuantityAbbreviated(globalBankItem.Quantity);
                        }
                        else
                        {
                            bankLabel.IsHidden = true;
                        }

                        if (bankItem.IsDragging)
                        {
                            bankItem.Pnl.IsHidden = true;
                            bankLabel.IsHidden = true;
                        }

                        bankItem.Update();
                    }
                }
                else
                {
                    bankItem.Pnl.IsHidden = true;
                    bankLabel.IsHidden = true;
                }
            }
        }

        void sort_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mBankWindow.IsHidden) return;

            PacketSender.SendBankSortPacket();
        }

        private void InitItemContainer()
        {
            for (var slotIndex = 0; slotIndex < Options.Instance.Bank.MaxSlots; slotIndex++)
            {
                var bankItem = new BankItem(this, slotIndex);

                bankItem.Container = new ImagePanel(mItemContainer, "BankItem");
                bankItem.Setup();

                var bankLabel = new Label(bankItem.Container, "BankItemValue");
                bankLabel.Text = "";

                bankItem.Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                
                Items.Add(bankItem);
                mValues.Add(bankLabel);
            }
        }

        /// <summary>
        /// Sets the item's position based on whether it's hidden or not
        /// </summary>
        /// <param name="i">Index of the item slot</param>
        private void SetItemPosition(int i)
        {
            var item = Items[i];

            // If the item is hidden, the player doesn't have that slot unlocked - move it to the top so that the scrollbar doesn't lie to the player
            if (item.Container.IsHidden)
            {
                item.Container.SetPosition(0, 0);
                return;
            }

            var xPadding = item.Container.Margin.Left + item.Container.Margin.Right;
            var yPadding = item.Container.Margin.Top + item.Container.Margin.Bottom;

            item.Container.SetPosition(
                i %
                (mItemContainer.Width / (item.Container.Width + xPadding)) *
                (item.Container.Width + xPadding) +
                xPadding,
                i /
                (mItemContainer.Width / (item.Container.Width + xPadding)) *
                (item.Container.Height + yPadding) +
                yPadding
            );
        }

        public FloatRect RenderBounds()
        {
            var rect = new FloatRect()
            {
                X = mBankWindow.LocalPosToCanvas(new Point(0, 0)).X - sItemXPadding / 2,
                Y = mBankWindow.LocalPosToCanvas(new Point(0, 0)).Y - sItemYPadding / 2,
                Width = mBankWindow.Width + sItemXPadding,
                Height = mBankWindow.Height + sItemYPadding
            };

            return rect;
        }

    }


    public partial class BankWindow
    {
        private TextBox mSearch;
        public List<BankSlot> SortedBank;
        private bool RefreshBank = false;

        public class BankSlot
        {
            public int SlotId { get; set; }
            public Item Item { get; set; }

            public BankSlot(int slotId, Item item)
            {
                SlotId = slotId;
                Item = item;
            }
        }

        private void _BankWindow()
        {
            mSearch = new TextBox(mBankWindow, "SearchBox");
            mSearch.TextChanged += mSearch_textChanged;
        }

        private void FillSortedBank()
        {
            SortedBank = new List<BankSlot>();
            for (var i = 0; i < Globals.Bank.Length; i++)
            {
                var item = Globals.Bank[i];
                SortedBank.Add(new BankSlot(i, item));
            }
        }

        private void UpdateBank()
        {
            if (!RefreshBank)
            {
                return;
            }

            FillSortedBank();
            if (!string.IsNullOrEmpty(mSearch.Text))
            {
                SortedBank = SortedBank
                    .Select((bankSlot) =>
                    {
                        var slotItem = bankSlot.Item;
                        if (slotItem == null)
                        {
                            return null;
                        }
                        var item = ItemBase.Get(slotItem.ItemId);
                        if (!ItemIsSearchable(item))
                        {
                            return null;
                        }
                        return bankSlot;
                    })
                    .OrderByDescending(bankItem => bankItem != null && ItemIsSearchable(ItemBase.Get(bankItem.Item.ItemId)))
                    .ToList();
            }
            RefreshBank = false;
        }

        private bool ItemIsSearchable(ItemBase item)
        {
            string itemType = Enum.GetName(typeof(ItemTypes), item.ItemType).ToLower();
            return SearchHelper.IsSearchable(item?.Name, mSearch.Text) || SearchHelper.IsSearchable(itemType, mSearch.Text);
        }

        public void _Close()
        {
            if (mSearch == null)
            {
                return;
            }
            mSearch.Text = string.Empty;
        }

        private void mSearch_textChanged(Base control, EventArgs args)
        {
            mItemContainer.ScrollToTop();
            InitRefreshBank();
        }

        public void InitRefreshBank()
        {
            RefreshBank = true;
        }
    }
}
