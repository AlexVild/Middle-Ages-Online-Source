using System;
using System.Collections.Generic;
using System.Text;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Character.Equipment;
using Intersect.Client.Interface.Game.Character.StatPanel;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Character
{
    public class CharacterWindowPanelController
    {
        private ImagePanel Container { get; set; }
        private ImagePanel PanelContainer { get; set; }
        private Button PanelHideButton;

        private List<Button> PanelSelectors { get; set; }
        private Button StatsButton { get; set; }

        private CharacterWindowMAO Parent { get; set; }

        private CharacterWindowPanel CurrentPanel { get; set; }

        private CharacterStatsWindow StatsPanel { get; set; }

        public CharacterWindowPanelController(Canvas gameCanvas, CharacterWindowMAO parent)
        {
            Parent = parent;

            Container = new ImagePanel(gameCanvas, "CharacterWindowPanelContainer");

            StatsButton = new Button(Container, "StatsPanelSelector")
            {
                Text = "STATS"
            };
            StatsButton.Clicked += StatsClicked;

            PanelSelectors = new List<Button>
            {
                StatsButton
            };
            
            PanelContainer = new ImagePanel(Container, "PanelContainer");
            PanelHideButton = new Button(PanelContainer, "HidePanelButton");
            PanelHideButton.Clicked += HidePanelClicked;

            Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            StatsPanel = new CharacterStatsWindow(PanelContainer);

            PositionToParent();
            Hide();
            CurrentPanel = StatsPanel; // Default to equipment
        }

        private void PositionToParent()
        {
            Container.X = Parent.X + Parent.Width - 4;
            Container.Y = Parent.Y - 4;
            PositionSelectors();
        }

        private void PositionSelectors()
        {
            var initY = 48;
            var yPadding = 40;
            var idx = 0;
            foreach (var selector in PanelSelectors)
            {
                selector.Y = initY + (yPadding * idx);
                selector.X = PanelContainer.IsHidden ? 
                    0 : 
                    PanelContainer.Width;

                idx++;
            }
        }

        public void Update()
        {
            if (!Parent.IsVisible())
            {
                Hide();
            }

            if (Container.IsHidden)
            {
                return;
            }

            CurrentPanel?.Update();

            PositionToParent();
        }

        public void ChangePanel(CharacterPanelType type)
        {
            PanelContainer.Show();
            CurrentPanel?.Hide();

            EnableNav();
            switch (type)
            {
                case CharacterPanelType.Stats:
                    StatsButton.Disable();
                    CurrentPanel = StatsPanel;
                    break;
            }

            CurrentPanel?.Show();
        }

        public void Hide()
        {
            HidePanel();
            Container.Hide();
        }

        public void Show()
        {
            Container.Show();
        }

        public void HidePanel()
        {
            EnableNav();
            PanelContainer.Hide();
        }

        public void ShowPanel()
        {
            PanelContainer.Show();
        }

        private void EnableNav()
        {
            foreach(var selector in PanelSelectors)
            {
                selector.Enable();
            }
        }

        private void HidePanelClicked(Base control, EventArgs args)
        {
            HidePanel();
        }

        private void StatsClicked(Base control, EventArgs args)
        {
            ChangePanel(CharacterPanelType.Stats);
        }
    }
}
