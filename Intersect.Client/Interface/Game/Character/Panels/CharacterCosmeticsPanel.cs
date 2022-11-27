using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public static class CharacterCosmeticsPanelController
    {
        public static bool RefreshCosmeticsPanel { get; set; } = false;

        /// <summary>
        /// A mapping of equipment slot -> cosmetic item ID
        /// </summary>
        public static Dictionary<int, List<Guid>> UnlockedCosmetics = new Dictionary<int, List<Guid>>();
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

        private bool RefreshContainers { get; set; }

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
        }

        private void SearchBar_TextChanged(Base sender, EventArgs arguments)
        {
            RefreshContainers = true;
        }

        private void SearchClearButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            SearchTerm = string.Empty;
        }

        public override void Show()
        {
            PacketSender.SendRequestCosmetics();
            Interface.InputBlockingElements.Add(SearchBar);

            base.Show(); 
        }

        public override void Hide()
        {
            Interface.InputBlockingElements.Remove(SearchBar);

            base.Hide();
        }

        public override void Update()
        {
            // intentionally blank
        }
    }
}
