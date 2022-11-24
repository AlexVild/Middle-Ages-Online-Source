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
        NameTag
    }

    public partial class CharacterWindowMAO
    {
        private WindowControl mCharacterWindow;
        private CharacterEquipmentWindow mEquipmentPanel;
        private ImagePanel PlayerContainer;
        private CharacterWindowPanelController PanelContainer;

        public WindowControl CharacterWindow => mCharacterWindow;

        public int X => mCharacterWindow.X;
        public int Y => mCharacterWindow.Y;
        public int Width => mCharacterWindow.Width;
        public int Height => mCharacterWindow.Height;

        public CharacterWindowMAO(Canvas gameCanvas)
        {
            mCharacterWindow = new WindowControl(gameCanvas, Strings.Character.title, false, "CharacterWindowMAO", onClose: Hide);
            mCharacterWindow.DisableResizing();
            PlayerContainer = new ImagePanel(mCharacterWindow, "Container");
            mCharacterWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            PanelContainer = new CharacterWindowPanelController(gameCanvas, this);

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
            mCharacterWindow.Hide();
        }

        public void Show()
        {
            mCharacterWindow.Show();
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
        }
    }
}
