﻿using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.GameObjects;
using System;

namespace Intersect.Client.Interface.Game
{
    public abstract class ItemContainer
    {
        public ImagePanel ContentPanel;

        public Base Container;

        private Guid mCurrentItemId;

        private ItemDescriptionWindow mDescWindow;

        private int[] mStatBoost = new int[(int)Enums.Stats.StatCount];

        private bool mTexLoaded;

        protected int mIndex;

        public ImagePanel Pnl;

        public abstract string Filename { get; }
        public abstract string ContentName { get; }

        public ItemContainer(int index, Base container)
        {
            mIndex = index;
            Container = container;
        }

        public virtual void Setup()
        {
            Pnl = new ImagePanel(Container, Filename);
            Pnl.HoverEnter += pnl_HoverEnter;
            Pnl.HoverLeave += pnl_HoverLeave;
            Pnl.RightClicked += pnl_RightClicked;
            Pnl.Clicked += Pnl_Clicked;
            Pnl.DoubleClicked += Pnl_DoubleClicked;

            ContentPanel = new ImagePanel(Pnl, ContentName);
            ContentPanel.MouseInputEnabled = false;

            Pnl.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        protected virtual void Pnl_DoubleClicked(Base sender, ClickedEventArgs arguments) { }

        protected virtual void Pnl_Clicked(Base sender, ClickedEventArgs arguments) { }

        protected virtual void pnl_RightClicked(Base sender, ClickedEventArgs arguments) { }

        protected virtual void pnl_HoverLeave(Base sender, EventArgs arguments)
        {
            if (mDescWindow != null)
            {
                mDescWindow.Dispose();
                mDescWindow = null;
            }
        }

        protected virtual void pnl_HoverEnter(Base sender, EventArgs arguments)
        {
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

            var item = ItemBase.Get(mCurrentItemId);
            if (item == null)
            {
                return;
            }

            mDescWindow = new ItemDescriptionWindow(item, 1, Container.X, Container.Y, mStatBoost, item.Name);
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
            if (currentItemId != mCurrentItemId || !mTexLoaded)
            {
                mCurrentItemId = currentItemId;
                mStatBoost = statBoost;
                var item = ItemBase.Get(mCurrentItemId);
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

                mTexLoaded = true;
            }
        }

        public void SetPosition(int x, int y)
        {
            Pnl.SetPosition(x, y);
        }
    }
}
