using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.BestiaryUi
{
    public class BeastSpellItem
    {

        public ImagePanel ContentPanel;

        private Base Container;

        private Guid SpellId;

        private int Index;

        public ImagePanel Pnl;

        private SpellDescriptionWindow DescWindow;

        private SpellBase Spell;

        public string Name;

        public BeastSpellItem(int index, Base container, Guid spellId)
        {
            Index = index;
            Container = container;
            SpellId = spellId;
        }

        public void Setup()
        {
            Pnl = new ImagePanel(Container, "BeastSpellItem");

            ContentPanel = new ImagePanel(Pnl, "BeastSpellIcon");
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
            DescWindow = new SpellDescriptionWindow(SpellId, (int)mousePos.X, (int)mousePos.Y);
        }

        public void SetPosition(float x, float y)
        {
            Pnl.SetPosition(x, y);
        }

        public void Update()
        {
            var spell = SpellBase.Get(SpellId);
            if (spell != null)
            {
                var itemTex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Spell, spell.Icon);
                if (itemTex != null)
                {
                    ContentPanel.Show();
                    ContentPanel.Texture = itemTex;
                    Name = spell.Name;
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
