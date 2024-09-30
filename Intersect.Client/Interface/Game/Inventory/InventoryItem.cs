using System;

using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.Inventory
{

    public class InventoryItem
    {

        public ImagePanel Container;

        public Label EquipLabel;

        public ImagePanel EquipPanel;

        public bool IsDragging;

        //Dragging
        private bool mCanDrag;

        private long mClickTime;

        private Label mCooldownLabel;

        private int mCurrentAmt = 0;

        private Guid mCurrentItemId;

        private ItemDescriptionWindow mDescWindow;

        private Draggable mDragIcon;

        private bool mIconCd;

        //Drag/Drop References
        private InventoryWindow mInventoryWindow;

        private bool mIsEquipped;

        //Mouse Event Variables
        private bool mMouseOver;

        private int mMouseX = -1;

        private int mMouseY = -1;

        //Slot info
        private int mMySlot;

        private string mTexLoaded = "";

        public ImagePanel Pnl;

        bool ItemsBeingModified => Globals.Me.Enhancement.IsOpen || Globals.Me.UpgradeStation.IsOpen || Interface.GameUi.WeaponPickerOpen;

        bool InDeconstructor => Globals.Me.Deconstructor.IsOpen;

        public InventoryItem(InventoryWindow inventoryWindow, int index)
        {
            mInventoryWindow = inventoryWindow;
            mMySlot = index;
        }

        public void Setup()
        {
            Pnl = new ImagePanel(Container, "InventoryItemIcon");
            Pnl.HoverEnter += pnl_HoverEnter;
            Pnl.HoverLeave += pnl_HoverLeave;
            Pnl.RightClicked += pnl_RightClicked;
            Pnl.Clicked += pnl_Clicked;
            EquipPanel = new ImagePanel(Pnl, "InventoryItemEquippedIcon");
            EquipPanel.Texture = Graphics.Renderer.GetWhiteTexture();
            EquipLabel = new Label(Pnl, "InventoryItemEquippedLabel");
            EquipLabel.IsHidden = true;
            EquipLabel.Text = Strings.Inventory.equippedicon;
            EquipLabel.TextColor = new Color(0, 255, 255, 255);
            mCooldownLabel = new Label(Pnl, "InventoryItemCooldownLabel");
            mCooldownLabel.IsHidden = true;
            mCooldownLabel.TextColor = new Color(0, 255, 255, 255);
        }

        void pnl_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (InDeconstructor || ItemsBeingModified)
            {
                return;
            }
            mClickTime = Timing.Global.Milliseconds + 500;
        }

        void pnl_RightClicked(Base sender, ClickedEventArgs arguments)
        {
            if (Globals.GameShop != null)
            {
                Globals.Me.TrySellItem(mMySlot);
            }
            else if (Globals.InBank)
            {
                Globals.Me.TryDepositItem(mMySlot);
            }
            else if (Globals.InBag)
            {
                Globals.Me.TryStoreBagItem(mMySlot, -1);
            }
            else if (Globals.InTrade)
            {
                Globals.Me.TryTradeItem(mMySlot);
            }
            else if (Globals.Me.Deconstructor.IsOpen)
            {
                if (!Globals.Me.Deconstructor.AddingFuel && !Globals.Me.Deconstructor.TryAddItem(mMySlot))
                {
                    Globals.Me.Deconstructor.TryRemoveItem(mMySlot);
                }
                else if (Globals.Me.Deconstructor.AddingFuel)
                {
                    var inv = Globals.Me.Inventory;
                    var invItem = inv[mMySlot];
                    if (invItem.Quantity > 1)
                    {
                        if (Input.QuickModifierActive())
                        {
                            if (!Globals.Me.Deconstructor.TryAddFuel(mMySlot, invItem.Quantity))
                            {
                                Globals.Me.Deconstructor.TryRemoveItem(mMySlot);
                            }
                        }
                        else
                        {
                            var iBox = new InputBox(
                                Strings.Inventory.AddFuel,
                                Strings.Inventory.AddFuelPrompt.ToString(ItemBase.Get(invItem.ItemId).Name), true,
                                InputBox.InputType.NumericInput, UseItemForFuel, null, mMySlot, invItem.Quantity
                            );
                        }
                    }
                    else if (!Globals.Me.Deconstructor.TryAddFuel(mMySlot, 1))
                    {
                        Globals.Me.Deconstructor.TryRemoveItem(mMySlot);
                    }
                }
            }
            else if (Globals.Me.InCutscene() || ItemsBeingModified)
            {
                return;
            }
            else
            {
                Globals.Me.TryDropItem(mMySlot);
            }
        }

        void UseItemForFuel(object sender, EventArgs e)
        {
            var value = (int)((InputBox)sender).Value;
            Globals.Me.Deconstructor.TryAddFuel(mMySlot, value);
        }

        void pnl_HoverLeave(Base sender, EventArgs arguments)
        {
            Interface.GameUi.FocusedInventory = false;
            mMouseOver = false;
            mMouseX = -1;
            mMouseY = -1;
            if (mDescWindow != null)
            {
                mDescWindow.Dispose();
                mDescWindow = null;
            }
        }

        void pnl_HoverEnter(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }
            
            Interface.GameUi.FocusedInventory = true;

            mMouseOver = true;
            mCanDrag = true;
            if (Globals.InputManager.MouseButtonDown(GameInput.MouseButtons.Left))
            {
                mCanDrag = false;

                return;
            }

            if (InDeconstructor || ItemsBeingModified)
            {
                mCanDrag = false;
            }

            if (mDescWindow != null)
            {
                mDescWindow.Dispose();
                mDescWindow = null;
            }

            if (Globals.GameShop == null)
            {
                if (Globals.Me.Inventory[mMySlot]?.Base != null)
                {
                    mDescWindow = new ItemDescriptionWindow(
                        Globals.Me.Inventory[mMySlot].Base, Globals.Me.Inventory[mMySlot].Quantity, mInventoryWindow.X,
                        mInventoryWindow.Y, Globals.Me.Inventory[mMySlot].ItemProperties
                    );
                }
            }
            else
            {
                var invItem = Globals.Me.Inventory[mMySlot];

                if (Globals.GameShop.BuysItem(invItem.Base))
                {
                    ShopItem buysFor = Globals.GameShop.BuyingItems.Find(buyItem => invItem.ItemId == buyItem.ItemId);

                    // If the item has a specific currency set that it is selling for
                    if (buysFor != null)
                    {
                        var hoveredItem = ItemBase.Get(buysFor.CostItemId);
                        if (hoveredItem != null && Globals.Me.Inventory[mMySlot]?.Base != null)
                        {
                            mDescWindow = new ItemDescriptionWindow(
                                Globals.Me.Inventory[mMySlot].Base, Globals.Me.Inventory[mMySlot].Quantity,
                                mInventoryWindow.X, mInventoryWindow.Y, Globals.Me.Inventory[mMySlot].ItemProperties, "",
                                Strings.Shop.sellsfor.ToString(buysFor.CostItemQuantity, hoveredItem.Name)
                            );
                        }
                    }
                    else // Else, the default currency
                    {
                        var costItem = Globals.GameShop.DefaultCurrency;
                        if (invItem.Base != null && costItem != null && Globals.Me.Inventory[mMySlot]?.Base != null)
                        {
                            mDescWindow = new ItemDescriptionWindow(
                                Globals.Me.Inventory[mMySlot].Base, Globals.Me.Inventory[mMySlot].Quantity,
                                mInventoryWindow.X, mInventoryWindow.Y, Globals.Me.Inventory[mMySlot].ItemProperties, "",
                                Strings.Shop.sellsfor.ToString((int) Math.Ceiling(invItem.Base.Price * Globals.GameShop.BuyMultiplier), costItem.Name)
                            );
                        }
                    }
                }
                else if(invItem?.Base != null)
                {
                    mDescWindow = new ItemDescriptionWindow(
                        invItem.Base, invItem.Quantity, mInventoryWindow.X, mInventoryWindow.Y, invItem.ItemProperties,
                        "", Strings.Shop.wontbuy
                    );
                }
            }
        }

        public FloatRect RenderBounds()
        {
            var rect = new FloatRect()
            {
                X = Pnl.LocalPosToCanvas(new Point(0, 0)).X,
                Y = Pnl.LocalPosToCanvas(new Point(0, 0)).Y,
                Width = Pnl.Width,
                Height = Pnl.Height
            };

            return rect;
        }

        public void Update()
        {
            var equipped = false;
            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (Globals.Me.MyEquipment[i] == mMySlot)
                {
                    equipped = true;

                    break;
                }
            }

            var item = ItemBase.Get(Globals.Me.Inventory[mMySlot].ItemId);

            if (Globals.Me.Inventory[mMySlot].ItemId != mCurrentItemId ||
                Globals.Me.Inventory[mMySlot].Quantity != mCurrentAmt ||
                equipped != mIsEquipped ||
                item == null && mTexLoaded != "" ||
                item != null && mTexLoaded != item.Icon ||
                mIconCd != Globals.Me.ItemOnCd(mMySlot) ||
                Globals.Me.Deconstructor.Refresh ||
                Globals.Me.RefreshInventoryItems ||
                Globals.Me.ItemOnCd(mMySlot))
            {
                mCurrentItemId = Globals.Me.Inventory[mMySlot].ItemId;
                mCurrentAmt = Globals.Me.Inventory[mMySlot].Quantity;
                mIsEquipped = equipped;
                // Alex: Commented out to ALWAYS hide equip panel/label, in favor of displaying a texture for equipped items
                /*
                EquipPanel.IsHidden = !mIsEquipped;
                EquipLabel.IsHidden = !mIsEquipped;
                */
                
                // Alex: This is my addition - also note that InventoryWindow has some new logic as well in its update loop
                if (mIsEquipped)
                {
                    Container.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "inventoryitemequipped.png");
                }
                else
                {
                    Container.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "inventoryitem.png");
                }

                if (Globals.Me.Deconstructor.IsOpen)
                {
                    if (!Globals.Me.Deconstructor.AddingFuel && (item?.FuelRequired ?? 0) <= 0)
                    {
                        Container.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "character_resource_disabled_bg.png");
                    }
                    else if (Globals.Me.Deconstructor.AddingFuel && (item?.Fuel ?? 0) <= 0)
                    {
                        Container.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "character_resource_disabled_bg.png");
                    }
                }

                EquipPanel.IsHidden = true; // Alex: Don't want, at the moment
                EquipLabel.IsHidden = true;

                mCooldownLabel.IsHidden = true;
                if (item != null)
                {
                    var itemTex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Item, item.Icon);
                    if (itemTex != null)
                    {
                        Pnl.Texture = itemTex;
                        if (Globals.Me.ItemOnCd(mMySlot) || 
                            (!Globals.Me.Deconstructor.AddingFuel && Globals.Me.Deconstructor.Items.Contains(mMySlot)) || 
                            (Globals.Me.Deconstructor.AddingFuel && Globals.Me.Deconstructor.FuelItems.ContainsKey(mMySlot)) ||
                            (Globals.Me.Deconstructor.IsOpen && !Globals.Me.Deconstructor.AddingFuel && (item?.FuelRequired ?? 0) <= 0) || 
                            (Globals.Me.Deconstructor.IsOpen && Globals.Me.Deconstructor.AddingFuel && (item?.Fuel ?? 0) <= 0))
                        {
                            Pnl.RenderColor = new Color(100, item.Color.R, item.Color.G, item.Color.B);
                        }
                        else
                        {
                            Pnl.RenderColor = item.Color;
                        }
                    }
                    else
                    {
                        if (Pnl.Texture != null)
                        {
                            Pnl.Texture = null;
                        }
                    }

                    mTexLoaded = item.Icon;
                    mIconCd = Globals.Me.ItemOnCd(mMySlot);
                    if (mIconCd)
                    {
                        mCooldownLabel.IsHidden = false;
                        
                        var msRemaining = (float) Globals.Me.ItemCdRemainder(mMySlot);
                        mCooldownLabel.Text = TextUtils.CooldownText(msRemaining);
                    }
                }
                else
                {
                    if (Pnl.Texture != null)
                    {
                        Pnl.Texture = null;
                    }

                    mTexLoaded = "";
                }

                if (mDescWindow != null)
                {
                    mDescWindow.Dispose();
                    mDescWindow = null;
                    pnl_HoverEnter(null, null);
                }
            }

            if (!IsDragging)
            {
                if (mMouseOver)
                {
                    if (!Globals.InputManager.MouseButtonDown(GameInput.MouseButtons.Left) && !InDeconstructor && !ItemsBeingModified)
                    {
                        mCanDrag = true;
                        mMouseX = -1;
                        mMouseY = -1;
                        if (Timing.Global.Milliseconds < mClickTime)
                        {
                            Globals.Me.TryUseItem(mMySlot);
                            mClickTime = 0;
                        }
                    }
                    else
                    {
                        if (mCanDrag && Draggable.Active == null)
                        {
                            if (mMouseX == -1 || mMouseY == -1)
                            {
                                mMouseX = InputHandler.MousePosition.X - Pnl.LocalPosToCanvas(new Point(0, 0)).X;
                                mMouseY = InputHandler.MousePosition.Y - Pnl.LocalPosToCanvas(new Point(0, 0)).Y;
                            }
                            else
                            {
                                var xdiff = mMouseX -
                                            (InputHandler.MousePosition.X - Pnl.LocalPosToCanvas(new Point(0, 0)).X);

                                var ydiff = mMouseY -
                                            (InputHandler.MousePosition.Y - Pnl.LocalPosToCanvas(new Point(0, 0)).Y);

                                if (Math.Sqrt(Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2)) > 5)
                                {
                                    IsDragging = true;
                                    mDragIcon = new Draggable(
                                        Pnl.LocalPosToCanvas(new Point(0, 0)).X + mMouseX,
                                        Pnl.LocalPosToCanvas(new Point(0, 0)).X + mMouseY, Pnl.Texture, Pnl.RenderColor
                                    );
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (mDragIcon.Update())
                {
                    //Drug the item and now we stopped
                    IsDragging = false;
                    var dragRect = new FloatRect(
                        mDragIcon.X - (Container.Padding.Left + Container.Padding.Right) / 2,
                        mDragIcon.Y - (Container.Padding.Top + Container.Padding.Bottom) / 2,
                        (Container.Padding.Left + Container.Padding.Right) / 2 + Pnl.Width,
                        (Container.Padding.Top + Container.Padding.Bottom) / 2 + Pnl.Height
                    );

                    float bestIntersect = 0;
                    var bestIntersectIndex = -1;

                    //So we picked up an item and then dropped it. Lets see where we dropped it to.
                    //Check inventory first.
                    if (mInventoryWindow.RenderBounds().IntersectsWith(dragRect))
                    {
                        for (var i = 0; i < Options.MaxInvItems; i++)
                        {
                            if (mInventoryWindow.Items[i].RenderBounds().IntersectsWith(dragRect))
                            {
                                if (FloatRect.Intersect(mInventoryWindow.Items[i].RenderBounds(), dragRect).Width *
                                    FloatRect.Intersect(mInventoryWindow.Items[i].RenderBounds(), dragRect).Height >
                                    bestIntersect)
                                {
                                    bestIntersect =
                                        FloatRect.Intersect(mInventoryWindow.Items[i].RenderBounds(), dragRect).Width *
                                        FloatRect.Intersect(mInventoryWindow.Items[i].RenderBounds(), dragRect).Height;

                                    bestIntersectIndex = i;
                                }
                            }
                        }

                        if (bestIntersectIndex > -1)
                        {
                            if (mMySlot != bestIntersectIndex)
                            {
                                //Try to swap....
                                PacketSender.SendSwapInvItems(bestIntersectIndex, mMySlot);
                                Globals.Me.SwapItems(bestIntersectIndex, mMySlot);
                            }
                        }
                    }
                    else if (Interface.GameUi.Hotbar.RenderBounds().IntersectsWith(dragRect))
                    {
                        for (var i = 0; i < Options.MaxHotbar; i++)
                        {
                            if (Interface.GameUi.Hotbar.Items[i].RenderBounds().IntersectsWith(dragRect))
                            {
                                if (FloatRect.Intersect(
                                            Interface.GameUi.Hotbar.Items[i].RenderBounds(), dragRect
                                        )
                                        .Width *
                                    FloatRect.Intersect(Interface.GameUi.Hotbar.Items[i].RenderBounds(), dragRect)
                                        .Height >
                                    bestIntersect)
                                {
                                    bestIntersect =
                                        FloatRect.Intersect(Interface.GameUi.Hotbar.Items[i].RenderBounds(), dragRect)
                                            .Width *
                                        FloatRect.Intersect(Interface.GameUi.Hotbar.Items[i].RenderBounds(), dragRect)
                                            .Height;

                                    bestIntersectIndex = i;
                                }
                            }
                        }

                        if (bestIntersectIndex > -1)
                        {
                            Globals.Me.AddToHotbar((byte) bestIntersectIndex, 0, mMySlot);
                        }
                    }
                    else if (Globals.InBag)
                    {
                        var bagWindow = Interface.GameUi.GetBag();
                        if (bagWindow.RenderBounds().IntersectsWith(dragRect))
                        {
                            for (var i = 0; i < Globals.Bag.Length; i++)
                            {
                                if (bagWindow.Items[i].RenderBounds().IntersectsWith(dragRect))
                                {
                                    if (FloatRect.Intersect(bagWindow.Items[i].RenderBounds(), dragRect).Width *
                                        FloatRect.Intersect(bagWindow.Items[i].RenderBounds(), dragRect).Height >
                                        bestIntersect)
                                    {
                                        bestIntersect =
                                            FloatRect.Intersect(bagWindow.Items[i].RenderBounds(), dragRect).Width *
                                            FloatRect.Intersect(bagWindow.Items[i].RenderBounds(), dragRect).Height;

                                        bestIntersectIndex = i;
                                    }
                                }
                            }

                            if (bestIntersectIndex > -1)
                            {
                                Globals.Me.TryStoreBagItem(mMySlot, bestIntersectIndex);
                            }
                        }
                    }

                    mDragIcon.Dispose();
                }
            }

            if (mDescWindow != null)
            {
                mDescWindow.Update();
            }
        }
    }
}
