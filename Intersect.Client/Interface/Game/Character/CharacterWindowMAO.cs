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
        Equipment = 0,
        Stats,
    }

    public partial class CharacterWindowMAO
    {
        private WindowControl mCharacterWindow;
        private CharacterEquipmentWindow mEquipmentPanel;
        private CharacterStatsWindow mStatPanel;
        private CharacterWindowPanel CurrentPanel;
        private ImagePanel Container;

        public WindowControl CharacterWindow => mCharacterWindow;

        //Location
        public int X;

        public int Y;

        public CharacterWindowMAO(Canvas gameCanvas)
        {
            mCharacterWindow = new WindowControl(gameCanvas, Strings.Character.title, false, "CharacterWindowMAO");
            mCharacterWindow.DisableResizing();
            Container = new ImagePanel(mCharacterWindow, "Container");

            mCharacterWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            mEquipmentPanel = new CharacterEquipmentWindow(this, Container);
            mEquipmentPanel.Hide();
            
            mStatPanel = new CharacterStatsWindow(Container);
            mStatPanel.Hide();

            CurrentPanel = mEquipmentPanel; // Default to equipment
        }

        public bool IsVisible()
        {
            return mCharacterWindow.IsVisible;
        }

        public void Hide()
        {
            mCharacterWindow.Hide();
        }

        public void Show()
        {
            mCharacterWindow.Show();
            CurrentPanel?.Show();
        }

        public void Update()
        {
            if (mCharacterWindow.IsHidden)
            {
                return;
            }

            CurrentPanel?.Update();
        }

        public void ChangePanel(CharacterPanelType type)
        {
            CurrentPanel?.Hide();

            switch(type)
            {
                case CharacterPanelType.Equipment:
                    CurrentPanel = mEquipmentPanel;
                    break;
            }

            CurrentPanel?.Show();
        }
    }
}
