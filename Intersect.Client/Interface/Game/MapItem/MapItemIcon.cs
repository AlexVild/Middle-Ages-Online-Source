using System;

using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Items;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.Inventory
{

    public class MapItemIcon
    {

        public GameTexture StandardFrameBg { get; set; }
        private GameTexture _rareFrameBg;
        private GameTexture _rareFrameBgFlash { get; set; }

        readonly long FlashRate = 300;
        long LastUpdate = 0;
        public bool IsFlashing = false;

        public GameTexture RareFrameBg => IsFlashing ? _rareFrameBgFlash : _rareFrameBg;

        public ImagePanel Container;

        public MapItemInstance MyItem;

        public Guid MapId;

        public int TileIndex;
    
        public ImagePanel Pnl;

        private Base mMapItemWindow;

        private ItemDescriptionWindow mDescWindow;

        public MapItemIcon(Base window)
        {
            mMapItemWindow = window;
            StandardFrameBg = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "inventoryitem.png");
            _rareFrameBg = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "mapicon_rare.png");
            _rareFrameBgFlash = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "mapicon_rare_flash.png");
        }

        public void Setup()
        {
            Pnl = new ImagePanel(Container, "MapItemIcon");
            Pnl.HoverEnter += pnl_HoverEnter;
            Pnl.HoverLeave += pnl_HoverLeave;
            Pnl.Clicked += pnl_Clicked;
            
            LastUpdate = Timing.Global.Milliseconds + FlashRate;
        }

        void pnl_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (MyItem == null || TileIndex < 0 || TileIndex >= Options.MapWidth * Options.MapHeight)
            {
                return;
            }

            Globals.Me.TryPickupItem(MapId, TileIndex, MyItem.UniqueId);
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
                ItemBase.Get(MyItem.ItemId), MyItem.Quantity, mMapItemWindow.X,
                mMapItemWindow.Y, MyItem.ItemProperties
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

                if (item.RareDrop)
                {
                    Container.Texture = RareFrameBg;
                    if (LastUpdate < Timing.Global.Milliseconds)
                    {
                        IsFlashing = !IsFlashing;
                        LastUpdate = Timing.Global.Milliseconds + FlashRate;
                    }
                }
                else
                {
                    Container.Texture = StandardFrameBg;
                }
            }
            else
            {
                Container.Texture = StandardFrameBg;
                if (Pnl.Texture != null)
                {
                    Pnl.Texture = null;
                }
            }

            mDescWindow?.Update();
        }
    }

}
