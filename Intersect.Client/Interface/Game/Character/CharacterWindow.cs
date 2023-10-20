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
    public enum CharacterPanelType
    {
        None = 0,
        Equipment,
        Stats,
        Bonuses,
        Harvesting,
        Recipes,
        NameTag,
        Cosmetics,
        Skills,
        Challenges,
        Loadouts,
        Enhancements
    }

    public partial class CharacterWindow
    {
        private WindowControl mCharacterWindow;
        private CharacterEquipmentWindow mEquipmentPanel;
        private ImagePanel PlayerContainer;
        private CharacterWindowPanelController PanelContainer;
        private CharacterWindowNotifications NotificationContainer;
        public CharacterLevelUpWindow LevelUpWindow;

        public WindowControl Window => mCharacterWindow;

        public int CenterX => mCharacterWindow.X + (mCharacterWindow.Width / 2);
        public int X => mCharacterWindow.X;
        public int Y => mCharacterWindow.Y;
        public int Width => mCharacterWindow.Width;
        public int Height => mCharacterWindow.Height;

        public CharacterWindow(Canvas gameCanvas)
        {
            mCharacterWindow = new WindowControl(gameCanvas, Strings.Character.title, false, "CharacterWindowMAO", onClose: Hide);
            mCharacterWindow.DisableResizing();
            PlayerContainer = new ImagePanel(mCharacterWindow, "Container");
            mCharacterWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            
            PanelContainer = new CharacterWindowPanelController(gameCanvas, this);
            NotificationContainer = new CharacterWindowNotifications(gameCanvas, this, PanelContainer);
            LevelUpWindow = new CharacterLevelUpWindow(this, gameCanvas);

            mEquipmentPanel = new CharacterEquipmentWindow(this, PlayerContainer);
            mEquipmentPanel.Show();
        }

        public bool IsVisible()
        {
            return mCharacterWindow.IsVisible;
        }

        public void Hide()
        {
            PanelContainer.Hide();
            NotificationContainer.Hide();
            mCharacterWindow.Hide();
            LevelUpWindow.Hide();
        }

        public void Show()
        {
            mCharacterWindow.Show();

            // Request information from the server if needed
            if (Globals.Me?.Skillbook.Count == 0)
            {
                PacketSender.SendRequestSkillbook();
            }

            PanelContainer.Show();
        }

        public void Update()
        {
            if (mCharacterWindow.IsHidden)
            {
                return;
            }

            if (Globals.Me.InCutscene())
            {
                Hide();
                return;
            }

            mEquipmentPanel.Update();
            PanelContainer.Update();

            NotificationContainer.IsHidden = !Globals.CharacterUpdateAvailable;
            if (NotificationContainer.IsVisible)
            {
                NotificationContainer.Update();
            }

            if (LevelUpWindow.IsVisible)
            {
                LevelUpWindow.Update();
            }
        }
    }
}
