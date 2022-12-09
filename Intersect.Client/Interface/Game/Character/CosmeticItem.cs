using System;
using System.Collections.Generic;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Networking;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Character
{
    public class CosmeticItem
    {

        public ImagePanel ContentPanel;

        private Base CosmeticContainer;

        private Guid CurrentItemId;

        private int Index;
        
        private int SlotType;

        public ImagePanel Pnl;

        private bool IsEquipped;

        public string Name;

        public CosmeticItem(int index, Base characterWindow, Guid itemId, int slot)
        {
            Index = index;
            CosmeticContainer = characterWindow;
            CurrentItemId = itemId;
            SlotType = slot;
        }

        public void Setup()
        {
            Pnl = new ImagePanel(CosmeticContainer, "CosmeticItem");
            Pnl.Clicked += Pnl_Clicked;

            ContentPanel = new ImagePanel(Pnl, "CosmeticIcon");
            ContentPanel.MouseInputEnabled = false;

            Pnl.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            Update();
        }

        public void SetPosition(float x, float y)
        {
            Pnl.SetPosition(x, y);
        }

        private void Pnl_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Audio.AddGameSound("al_cloth-heavy.wav", false);
            if (IsEquipped)
            {
                PacketSender.SendCosmeticChange(Guid.Empty, SlotType);
            }
            else
            {
                PacketSender.SendCosmeticChange(CurrentItemId, SlotType);
            }
        }

        public void Update()
        {
            var item = ItemBase.Get(CurrentItemId);
            if (item != null)
            {
                var itemTex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Item, item.Icon);
                if (itemTex != null)
                {
                    ContentPanel.Show();
                    ContentPanel.Texture = itemTex;
                    ContentPanel.RenderColor = item.Color;
                    Name = string.IsNullOrEmpty(item.CosmeticDisplayName) ? item.Name : item.CosmeticDisplayName;
                    Pnl.SetToolTipText(Name);

                    UpdateEquipped();

                    Pnl.Texture = IsEquipped ? CharacterCosmeticsPanelController.CosmeticEquippedTexture : CharacterCosmeticsPanelController.CosmeticUnequippedTexture;
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

        public void UpdateEquipped()
        {
            IsEquipped = Globals.Me?.Cosmetics[SlotType] == CurrentItemId;
            Pnl.Texture = IsEquipped ? CharacterCosmeticsPanelController.CosmeticEquippedTexture : CharacterCosmeticsPanelController.CosmeticUnequippedTexture;
        }
    }
}
