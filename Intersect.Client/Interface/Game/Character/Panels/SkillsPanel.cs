using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public class SkillsPanel : CharacterWindowPanel
    {
        private Label SearchLabel { get; set; }
        private ImagePanel SearchBg { get; set; }
        private TextBox SearchBar { get; set; }
        private string SearchTerm
        {
            get => SearchBar.Text;
            set => SearchBar.SetText(value);
        }
        private Button SearchClearButton { get; set; }

        private ImagePanel SkillTypeBackground { get; set; }
        private Label SkillTypeLabel { get; set; }
        private ComboBox SkillTypeSelection { get; set; }

        private ScrollControl SkillsContainer { get; set; }
        private ComponentList<GwenComponent> SkillRows { get; set; } = new ComponentList<GwenComponent>();

        private List<string> AvailableSkillTypes = new List<string>();

        public SkillsPanel(ImagePanel characterWindow)
        {
            mParentContainer = characterWindow;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Skills");

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public override void Show()
        {
            // Load in available skills here
            Interface.InputBlockingElements.Add(SearchBar);
            SearchTerm = string.Empty;

            base.Show();
        }

        public override void Update()
        {
        }
    }
}
