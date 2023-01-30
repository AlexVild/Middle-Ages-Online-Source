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
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Character
{
    public class CharacterWindowNotifications
    {
        private ImagePanel mNotificationContainer;

        private CharacterWindowMAO Parent { get; set; }

        public CharacterWindowNotifications(Canvas gameCanvas, CharacterWindowMAO parent)
        {
            Parent = parent;

            mNotificationContainer = new ImagePanel(gameCanvas, "CharacterWindowNotificationWindow");

            mNotificationContainer.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            Hide();
            PositionToParent();
        }

        public void Show()
        {
            mNotificationContainer.Show();
        }

        public void Hide()
        {
            mNotificationContainer.Hide();
        }

        private void PositionToParent()
        {
            mNotificationContainer.X = Parent.CenterX - (mNotificationContainer.Width / 2);
            mNotificationContainer.Y = Parent.Y + Parent.Height - 4;
        }

        public void Update()
        {
            if (!Parent.IsVisible())
            {
                Hide();
            }

            if (mNotificationContainer.IsHidden)
            {
                return;
            }

            PositionToParent();
        }
    }
}
