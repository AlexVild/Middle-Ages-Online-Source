using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.General.UpgradeStation;
using Intersect.Client.Interface.Game.Enhancement;
using Intersect.Network.Packets.Server;
using System;

namespace Intersect.Client.Interface.Game.UpgradeStation
{
    public class UpgradeCompleteWindow
    {
        private UpgradeStationInterface UpgradeStation => Globals.Me?.UpgradeStation;

        private UpgradeStationWindow ParentWindow { get; set; }

        private ImagePanel Background { get; set; }

        private ImagePanel ItemContainer { get; set; }
        private EnhancementItemIcon Item { get; set; }

        private Button OkayButton { get; set; }

        public UpgradeCompleteWindow(UpgradeStationWindow parentWindow, Base canvas)
        {
            ParentWindow = parentWindow;

            Background = new ImagePanel(canvas, "WeaponUpgradeCompleteWindow");

            ItemContainer = new ImagePanel(Background, "ItemContainer");

            Item = new EnhancementItemIcon(0, ItemContainer, Background.X, Background.Y);

            OkayButton = new Button(Background, "OkayButton")
            {
                Text = "Okay"
            };
            OkayButton.Clicked += OkayButton_Clicked;

            Background.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            Item.Setup();
        }

        public void Show(Guid itemId, ItemProperties properties)
        {
            Item.Update(itemId, properties);
            Item.SetHoverPanelLocation(Background.X + 382, Background.Y + 90);
            Background.Show();
        }

        public void Hide()
        {
            Background.Hide();
        }

        private void OkayButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            ParentWindow.ForceClose(); // closes this menu as well
        }
    }
}
