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

        private ImagePanel LevelUpContainer;
        private Label LevelUpLabel;
        private Button LevelUpButton;
        
        private ImagePanel SkillUpContainer;
        private Label SkillUpLabel;
        private Button SkillUpButton;

        private CharacterWindowPanelController PanelController;

        private CharacterWindowMAO Parent { get; set; }

        public bool IsVisible => mNotificationContainer.IsVisible;

        public bool IsHidden 
        {
            get => mNotificationContainer.IsHidden;
            set => mNotificationContainer.IsHidden = value;
        }

        public CharacterWindowNotifications(Canvas gameCanvas, CharacterWindowMAO parent, CharacterWindowPanelController controller)
        {
            Parent = parent;
            PanelController = controller;

            mNotificationContainer = new ImagePanel(gameCanvas, "CharacterWindowNotificationWindow");
            LevelUpContainer = new ImagePanel(mNotificationContainer, "LevelUpContainer");
            LevelUpLabel = new Label(LevelUpContainer, "LevelUpLabel")
            {
                Text = "Unspent stat points!"
            };
            LevelUpButton = new Button(LevelUpContainer, "LevelUpButton")
            {
                Text = "Spend"
            };

            SkillUpContainer = new ImagePanel(mNotificationContainer, "SkillUpContainer");
            SkillUpLabel = new Label(SkillUpContainer, "SkillUpLabel")
            {
                Text = "New skill points!"
            };
            SkillUpButton = new Button(SkillUpContainer, "SkillUpButton")
            {
                Text = "Assign"
            };
            SkillUpButton.Clicked += SkillUpButton_Clicked;

            mNotificationContainer.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            Hide();
            PositionToParent();
        }

        private void SkillUpButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            PanelController?.Show();
            PanelController?.ChangePanel(CharacterPanelType.Skills);
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

            if (IsHidden)
            {
                return;
            }


            LevelUpContainer.IsHidden = (Globals.Me?.StatPoints ?? 0) <= 0;
            SkillUpContainer.IsHidden = !Globals.SkillPointUpdate;

            if (SkillUpContainer.IsVisible)
            {
                SkillUpLabel.Text = string.IsNullOrEmpty(Globals.SkillUpdateString) ? "Skill update!" : Globals.SkillUpdateString;
            }

            PositionToParent();
        }
    }
}
