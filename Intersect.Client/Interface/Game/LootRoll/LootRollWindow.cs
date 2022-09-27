using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Interface.Game.LootRoll.Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Items;
using Intersect.Client.Localization;
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

        public LootRollWindow(Canvas gameCanvas)
        {
            mGameCanvas = gameCanvas;
            mBackground = new WindowControl(gameCanvas, string.Empty, false, "LootRollWindow", onClose: OnClose);
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
            //mBankAllButton.Clicked += DismissRemaining;

            mTakeAllButton = new Button(mBackground, "TakeAllButton")
            {
                Text = Strings.LootRoll.TakeAll
            };
            //mTakeAllButton.Clicked += DismissRemaining;

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            InitLootContainer();
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
                LootItems[i].Setup();

                mLootValues.Add(new Label(LootItems[i].Container, "MapItemValue"));
                mLootValues[i].Text = "";

                LootItems[i].Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

                LootItems[i].Container.IsHidden = true;
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
            if (mBackground.IsHidden)
            {
                return;
            }

            if (Loot == null)
            {
                Close();
                return;
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
            Globals.Me.ResetLoot();
            // TODO: Send packet
            mBackground.Hide();
        }

        public void Show()
        {
            mBackground.Show();
        }

        public void Hide()
        {
            mBackground.Hide();
        }

        #region handlers
        private void OnClose()
        {
            Close();
        }

        private void DismissClicked(Base sender, ClickedEventArgs arguments)
        {
            OnClose();
        }
        #endregion
    }
}
