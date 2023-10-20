using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public static class CharacterEnhancementsPanelController
    {
        public static bool Refresh { get; set; } = false;
    }

    public class CharacterEnhancementsPanel : CharacterWindowPanel
    {
        public CharacterPanelType Type = CharacterPanelType.Enhancements;

        public CharacterEnhancementsPanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Enhancements");

            mBackground.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        public override void Update()
        {
            return;
        }
    }
}
