using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Networking;
using System;
using System.Collections.Generic;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public static class CharacterCosmeticsPanelController
    {
        public static bool RefreshCosmeticsPanel { get; set; } = false;

        /// <summary>
        /// A mapping of equipment slot -> cosmetic item ID
        /// </summary>
        public static Dictionary<int, List<Guid>> UnlockedCosmetics = new Dictionary<int, List<Guid>>();

        public static GameTexture CosmeticUnequippedTexture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "inventoryitem.png");
        public static GameTexture CosmeticEquippedTexture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "inventoryitemequipped.png");
    }

    public class CharacterCosmeticsPanel : CharacterWindowPanel
    {
        public CharacterPanelType Type = CharacterPanelType.Cosmetics;

        private ScrollControl CosmeticsContainer { get; set; }

        private ImagePanel SearchContainer { get; set; }
        private Label SearchLabel { get; set; }
        private ImagePanel SearchBg { get; set; }
        private TextBox SearchBar { get; set; }
        private string SearchTerm
        {
            get => SearchBar.Text;
            set => SearchBar.SetText(value);
        }
        private Button SearchClearButton { get; set; }

        private ScrollControl Components { get; set; }

        private ComponentList<GwenComponent> SelectionContainers { get; set; } = new ComponentList<GwenComponent>();

        public CharacterCosmeticsPanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_CosmeticArmors");

            SearchContainer = new ImagePanel(mBackground, "SearchContainer");
            SearchLabel = new Label(SearchContainer, "SearchLabel")
            {
                Text = "Search:"
            };
            SearchBg = new ImagePanel(SearchContainer, "SearchBg");
            SearchBar = new TextBox(SearchBg, "SearchBar");
            SearchBar.TextChanged += SearchBar_TextChanged;

            SearchClearButton = new Button(SearchContainer, "ClearButton")
            {
                Text = "CLEAR"
            };
            SearchClearButton.Clicked += SearchClearButton_Clicked;

            Components = new ScrollControl(mBackground, "ComponentContainer");

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            var helmets = new CosmeticSelectionComponent(Components, "HelmetSelection", "Helmets", "character_cosmetic_helmet.png", Options.HelmetIndex, SelectionContainers);
            var armors = new CosmeticSelectionComponent(Components, "ArmorSelection", "Armors", "character_cosmetic_armor.png", 1, SelectionContainers);
            var boots = new CosmeticSelectionComponent(Components, "BootSelection", "Boots", "character_cosmetic_boots.png", 4, SelectionContainers);

            var padding = 8;
            for(var i = 0; i < SelectionContainers.Count; i++)
            {
                var container = (CosmeticSelectionComponent)SelectionContainers[i];
                container.Initialize();

                var amt = padding + container.Height;
                container.Y += amt * i;
            }


            if (Globals.Me != null)
            {
                Globals.Me.CosmeticsUpdateDelegate = () =>
                {
                    UpdateEquippedStatus();
                };
            }
        }

        private void SearchBar_TextChanged(Base sender, EventArgs arguments)
        {
            foreach(var container in SelectionContainers)
            {
                ((CosmeticSelectionComponent)container).Search(SearchTerm);
            }
        }

        private void SearchClearButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            SearchTerm = string.Empty;
        }

        public override void Show()
        {
            PacketSender.SendRequestCosmetics();
            Interface.InputBlockingElements.Add(SearchBar);
            SearchTerm = string.Empty;

            base.Show(); 
        }

        public override void Hide()
        {
            Interface.InputBlockingElements.Remove(SearchBar);

            foreach (var el in SelectionContainers)
            {
                ((CosmeticSelectionComponent)el).ClearCosmetics();
            }

            base.Hide();
        }

        public override void Update()
        {
            if (CharacterCosmeticsPanelController.RefreshCosmeticsPanel)
            {
                foreach(var component in SelectionContainers)
                {
                    ((CosmeticSelectionComponent)component).Update();
                }

                CharacterCosmeticsPanelController.RefreshCosmeticsPanel = false;
            }
            // intentionally blank
        }

        private void UpdateEquippedStatus()
        {
            foreach (var component in SelectionContainers)
            {
                ((CosmeticSelectionComponent)component).UpdateEquippedStatus();
            }
        }
    }
}
