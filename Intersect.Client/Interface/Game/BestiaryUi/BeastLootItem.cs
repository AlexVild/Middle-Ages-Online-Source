using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Enums;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.BestiaryUi
{
    public class BeastLootItem
    {

        public ImagePanel ContentPanel;

        private Base Container;

        private Guid ItemId;

        private int Index;

        public ImagePanel Pnl;

        private ItemDescriptionWindow DescWindow;

        public string Name;

        public string DropChance;

        public double TableChance;

        public BeastLootItem(int index, Base container, Guid spellId, string dropChance, double tableChance = 0)
        {
            Index = index;
            Container = container;
            ItemId = spellId;
            DropChance = dropChance;
            TableChance = tableChance;
        }

        public void Setup()
        {
            Pnl = new ImagePanel(Container, "BeastLootItem");

            ContentPanel = new ImagePanel(Pnl, "BeastLootIcon");
            ContentPanel.MouseInputEnabled = false;

            Pnl.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            Pnl.HoverEnter += Pnl_HoverEnter;
            Pnl.HoverLeave += Pnl_HoverLeave;

            Update();
        }

        private void Pnl_HoverLeave(Base sender, EventArgs arguments)
        {
            if (DescWindow != null)
            {
                DescWindow.Dispose();
                DescWindow = null;
            }
        }

        private void Pnl_HoverEnter(Base sender, EventArgs arguments)
        {
            if (DescWindow != null)
            {
                DescWindow.Dispose();
                DescWindow = null;
            }

            var mousePos = Globals.InputManager.GetMousePosition();
            DescWindow = new ItemDescriptionWindow(ItemBase.Get(ItemId), 
                1, 
                (int)mousePos.X, 
                (int)mousePos.Y, 
                null,
                dropChance: DropChance,
                tableChance: TableChance);
        }

        public void SetPosition(float x, float y)
        {
            Pnl.SetPosition(x, y);
        }

        public void Update()
        {
            var item = ItemBase.Get(ItemId);
            if (item != null)
            {
                var itemTex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Item, item.Icon);
                if (itemTex != null)
                {
                    ContentPanel.Show();
                    ContentPanel.Texture = itemTex;
                    Name = item.Name;
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
        }
    }
}
