using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Interface.Game.LootRoll.Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Interface.ScreenAnimations;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Network.Packets.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.LootRoll
{
    public class LootRollWindow : Base
    {
        private Canvas mGameCanvas;

        private WindowControl mBackground;

        private ScrollControl mLootContainer;
        private List<Label> mLootValues = new List<Label>();
        public List<LootRollIcon> LootItems = new List<LootRollIcon>();

        private Button mTakeAllButton;
        private Button mBankAllButton;
        private Button mDismissRemainingButton;

        private Label mValueLabel;

        private List<Item> Loot => Globals.Me?.RolledLoot ?? null;

        // Context Menu
        private Framework.Gwen.Control.Menu mContextMenu;

        private MenuItem mDismissOption;
        private MenuItem mBankOption;
        private MenuItem mTakeOption;

        private LootChest LootChestAnim;

        private int mSelectedItemIdx;

        public LootRollWindow(Canvas gameCanvas)
        {
            mGameCanvas = gameCanvas;
            mBackground = new WindowControl(gameCanvas, string.Empty, false, "LootRollWindow", onClose: Close);
            mBackground.IsClosable = false;

            mLootContainer = new ScrollControl(mBackground, "LootContainer");
            mLootContainer.EnableScroll(false, true);

            mDismissRemainingButton = new Button(mBackground, "DismissRemainingButton")
            {
                Text = Strings.LootRoll.DismissRemaining
            };
            mDismissRemainingButton.Clicked += DismissClicked;

            mBankAllButton = new Button(mBackground, "BankAllButton")
            {
                Text = Strings.LootRoll.BankAll
            };
            mBankAllButton.Clicked += BankAllClicked;

            mTakeAllButton = new Button(mBackground, "TakeAllButton")
            {
                Text = Strings.LootRoll.TakeAll
            };
            mTakeAllButton.Clicked += TakeAllClicked;

            mContextMenu = new Framework.Gwen.Control.Menu(gameCanvas, "LootRollContextMenu");
            mContextMenu.IsHidden = true;
            mContextMenu.IconMarginDisabled = true;

            mContextMenu.Children.Clear();

            mTakeOption = mContextMenu.AddItem(Strings.LootRoll.TakeItem);
            mTakeOption.Clicked += TakeItem_Clicked;

            mBankOption = mContextMenu.AddItem(Strings.LootRoll.BankItem);
            mBankOption.Clicked += BankItem_Clicked;

            mDismissOption = mContextMenu.AddItem(Strings.LootRoll.DismissItem);
            mDismissOption.Clicked += DismissOption_Clicked;

            mContextMenu.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            InitLootContainer();

            LootChestAnim = new LootChest(() =>
            {
                Flash.FlashScreen(1000, new Color(255, 201, 226, 158), 150);
                mBackground.Show();
            });
        }

        public void SetTitle(string title)
        {
            mBackground.Title = title;
        }

        private void InitLootContainer()
        {
            for (var i = 0; i < Options.Instance.LootRollOpts.MaximumLootItems; i++)
            {
                LootItems.Add(new LootRollIcon(mBackground));
                LootItems[i].Container = new ImagePanel(mLootContainer, "LootRollIcon");
                LootItems[i].Setup(i);

                mLootValues.Add(new Label(LootItems[i].Container, "MapItemValue"));
                mLootValues[i].Text = "";

                LootItems[i].Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

                LootItems[i].Container.IsHidden = true;
                LootItems[i].Pnl.UserData = i;
                LootItems[i].Pnl.RightClicked += item_RightClicked;
            }
        }

        private void SetItemPosition(int i)
        {
            var xPadding = LootItems[i].Container.Margin.Left + LootItems[i].Container.Margin.Right;
            var yPadding = LootItems[i].Container.Margin.Top + LootItems[i].Container.Margin.Bottom;
            LootItems[i]
                .Container.SetPosition(
                    i %
                    (mLootContainer.Width / (LootItems[i].Container.Width + xPadding)) *
                    (LootItems[i].Container.Width + xPadding) +
                    xPadding,
                    i /
                    (mLootContainer.Width / (LootItems[i].Container.Width + xPadding)) *
                    (LootItems[i].Container.Height + yPadding) +
                    yPadding
                );
        }

        public void Update()
        {
            HandleInputBlocking();

            if (Loot == null)
            {
                if (mBackground.IsVisible)
                {
                    mBackground.Hide();
                }
                return;
            }
            else if (mBackground.IsHidden)
            {
                if (LootChestAnim.Done)
                {
                    LootChestAnim.ResetAnimation();
                }
                LootChestAnim.Draw();
            }

            var idx = 0;
            foreach (var loot in Loot)
            {
                // Skip rendering this item if we're already past the cap we are allowed to display.
                if (idx > Options.Loot.MaximumLootWindowItems - 1)
                {
                    continue;
                }

                var finalItem = loot.Base;
                if (finalItem != null)
                {
                    LootItems[idx].MyItem = loot;
                    LootItems[idx].Pnl.IsHidden = false;
                    if (finalItem.IsStackable)
                    {
                        mLootValues[idx].IsHidden = loot.Quantity <= 1;
                        mLootValues[idx].Text = Strings.FormatQuantityAbbreviated(loot.Quantity);
                    }
                    else
                    {
                        mLootValues[idx].IsHidden = true;
                    }
                    idx++;
                }
                else
                {
                    LootItems[idx].MyItem = null;
                    LootItems[idx].Pnl.IsHidden = true;
                    mLootValues[idx].IsHidden = true;
                }
            }

            for (var slot = 0; slot < Options.Loot.MaximumLootWindowItems; slot++)
            {
                if (slot > idx - 1)
                {
                    LootItems[slot].Container.IsHidden = true;
                    LootItems[slot].Container.SetPosition(0, 0);
                }
                else
                {
                    LootItems[slot].Container.IsHidden = false;
                    SetItemPosition(slot);
                }

                LootItems[slot].Update();
            }
        }

        private void HandleInputBlocking()
        {
            if (mBackground.IsHidden)
            {
                Interface.InputBlockingElements.Remove(this);
                return;
            }
            else if (!Interface.InputBlockingElements.Contains(this))
            {
                Interface.InputBlockingElements.Add(this);
            }
        }

        public void Close()
        {
            PacketSender.SendLootUpdateRequest(LootUpdateType.DismissAll);
        }

        public void DismissItem(object sender, EventArgs e)
        {
            var input = (InputBox)sender;
            var idx = input.UserData;
            PacketSender.SendLootUpdateRequest(LootUpdateType.DismissAt, mSelectedItemIdx);
        }
        
        #region handlers
        public void DismissAll(object sender, EventArgs e)
        {
            Close();
        }

        private void DismissClicked(Base sender, ClickedEventArgs arguments)
        {
            _ = new InputBox(
                Strings.LootRoll.DismissTitle, Strings.LootRoll.DismissPrompt, true, InputBox.InputType.YesNo,
                DismissAll, null, null
            );
        }

        private void BankAllClicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendLootUpdateRequest(LootUpdateType.BankAll);
        }

        private void TakeAllClicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendLootUpdateRequest(LootUpdateType.TakeAll);
        }

        private void DismissOption_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mSelectedItemIdx >= 0 && mSelectedItemIdx < Loot.Count)
            {
                var item = Loot[mSelectedItemIdx];
                _ = new InputBox(
                    Strings.LootRoll.DismissItemTitle, Strings.LootRoll.DismissItemPrompt.ToString(item?.Base?.Name), true, InputBox.InputType.YesNo,
                    DismissItem, null, mSelectedItemIdx
                );
            }
        }

        private void BankItem_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mSelectedItemIdx >= 0 && mSelectedItemIdx < Loot.Count)
            {
                PacketSender.SendLootUpdateRequest(LootUpdateType.BankAt, mSelectedItemIdx);
            }
        }

        private void TakeItem_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mSelectedItemIdx >= 0 && mSelectedItemIdx < Loot.Count)
            {
                PacketSender.SendLootUpdateRequest(LootUpdateType.TakeAt, mSelectedItemIdx);
            }
        }

        private void item_RightClicked(Base sender, ClickedEventArgs arguments)
        {
            var panel = (ImagePanel)sender;
            mSelectedItemIdx = (int)panel.UserData;
            mContextMenu.IsHidden = false;
            mContextMenu.SetSize(mContextMenu.Width, mContextMenu.Height);
            mContextMenu.Open(Framework.Gwen.Pos.None);
            mContextMenu.MoveTo(mContextMenu.X, mContextMenu.Y);
        }
        #endregion
    }
}
