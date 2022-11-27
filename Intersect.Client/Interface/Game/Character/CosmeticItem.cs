using System;
using System.Collections.Generic;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Networking;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Character
{
    public class CosmeticItem
    {

        public ImagePanel ContentPanel;

        private Base CharacterWindow;

        private Guid CurrentItemId;

        private ItemDescriptionWindow DescWindow;

        private int[] mStatBoost = new int[(int)Enums.Stats.StatCount];

        private bool TexLoaded;

        private int Index;

        public ImagePanel Pnl;

        public CosmeticItem(int index, Base characterWindow)
        {
            Index = index;
            CharacterWindow = characterWindow;
        }

        public void Setup()
        {
            Pnl.HoverEnter += pnl_HoverEnter;
            Pnl.HoverLeave += pnl_HoverLeave;
            Pnl.Clicked += Pnl_Clicked;

            ContentPanel = new ImagePanel(Pnl, "CosmeticIcon");
            ContentPanel.MouseInputEnabled = false;
        }

        private void Pnl_Clicked(Base sender, ClickedEventArgs arguments)
        {
            // Intentionally Blank
        }

        void pnl_HoverLeave(Base sender, EventArgs arguments)
        {
            if (DescWindow != null)
            {
                DescWindow.Dispose();
                DescWindow = null;
            }
        }

        void pnl_HoverEnter(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }

            if (Globals.InputManager.MouseButtonDown(GameInput.MouseButtons.Left))
            {
                return;
            }

            if (DescWindow != null)
            {
                DescWindow.Dispose();
                DescWindow = null;
            }

            var item = ItemBase.Get(CurrentItemId);
            if (item == null)
            {
                return;
            }

            DescWindow = new ItemDescriptionWindow(item, 1, CharacterWindow.X, CharacterWindow.Y, mStatBoost, item.Name);
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

        public void Update(Guid currentItemId, int[] statBoost)
        {
            if (currentItemId != CurrentItemId || !TexLoaded)
            {
                CurrentItemId = currentItemId;
                mStatBoost = statBoost;
                var item = ItemBase.Get(CurrentItemId);
                if (item != null)
                {
                    var itemTex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Item, item.Icon);
                    if (itemTex != null)
                    {
                        ContentPanel.Show();
                        ContentPanel.Texture = itemTex;
                        ContentPanel.RenderColor = item.Color;
                    }
                    else
                    {
                        ContentPanel.Hide();
                    }
                }
                else
                {
                    ContentPanel.Hide();
                }

                TexLoaded = true;
            }
        }

    }
}
