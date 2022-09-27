using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.LootRoll
{
    using System;
    using global::Intersect.Client.Framework.File_Management;
    using global::Intersect.Client.Framework.GenericClasses;
    using global::Intersect.Client.Framework.Gwen.Control;
    using global::Intersect.Client.Framework.Gwen.Control.EventArguments;
    using global::Intersect.Client.Framework.Gwen.Input;
    using global::Intersect.Client.Framework.Input;
    using global::Intersect.Client.General;
    using global::Intersect.Client.Interface.Game.DescriptionWindows;
    using global::Intersect.Client.Items;
    using global::Intersect.Client.Networking;
    using global::Intersect.GameObjects;
    using global::Intersect.Network.Packets.Client;

    namespace Intersect.Client.Interface.Game.Inventory
    {

        public class LootRollIcon
        {

            public ImagePanel Container;

            public Item MyItem;

            public int LootIndex;

            public ImagePanel Pnl;

            private Base mBackground;

            private ItemDescriptionWindow mDescWindow;

            public LootRollIcon(Base window)
            {
                mBackground = window;
            }

            public void Setup(int idx)
            {
                Pnl = new ImagePanel(Container, "LootRollIcon");
                Pnl.HoverEnter += pnl_HoverEnter;
                Pnl.HoverLeave += pnl_HoverLeave;
                Pnl.Clicked += pnl_Clicked;
                LootIndex = idx;
            }

            void pnl_Clicked(Base sender, ClickedEventArgs arguments)
            {
                if (MyItem == null)
                {
                    return;
                }

                PacketSender.SendLootUpdateRequest(LootUpdateType.TakeAt, LootIndex);
            }

            void pnl_HoverLeave(Base sender, EventArgs arguments)
            {
                if (mDescWindow != null)
                {
                    mDescWindow.Dispose();
                    mDescWindow = null;
                }
            }

            void pnl_HoverEnter(Base sender, EventArgs arguments)
            {
                if (MyItem == null)
                {
                    return;
                }

                if (InputHandler.MouseFocus != null)
                {
                    return;
                }

                if (Globals.InputManager.MouseButtonDown(GameInput.MouseButtons.Left))
                {
                    return;
                }

                if (mDescWindow != null)
                {
                    mDescWindow.Dispose();
                    mDescWindow = null;
                }
                mDescWindow = new ItemDescriptionWindow(
                    ItemBase.Get(MyItem.ItemId), MyItem.Quantity, mBackground.X,
                    mBackground.Y, MyItem.StatBuffs
               );
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
                if (MyItem == null)
                {
                    return;
                }

                var item = ItemBase.Get(MyItem.ItemId);
                if (item != null)
                {
                    var itemTex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Item, item.Icon);
                    if (itemTex != null)
                    {
                        Pnl.RenderColor = item.Color;
                        Pnl.Texture = itemTex;
                    }
                    else
                    {
                        if (Pnl.Texture != null)
                        {
                            Pnl.Texture = null;
                        }
                    }
                }
                else
                {
                    if (Pnl.Texture != null)
                    {
                        Pnl.Texture = null;
                    }

                }

                if (mDescWindow != null)
                {
                    mDescWindow.Dispose();
                    mDescWindow = null;
                    pnl_HoverEnter(null, null);
                }
            }
        }

    }

}
