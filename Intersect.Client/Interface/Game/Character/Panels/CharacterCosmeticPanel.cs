using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public class CharacterCosmeticPanel : CharacterWindowPanel
    {
        private ImagePanel LabelSearchContainer { get; set; }

        private ImagePanel LabelsBackground { get; set; }

        private ScrollControl LabelContainer { get; set; }

        private Label LabelSearchLabel { get; set; }
        private ImagePanel LabelSearchBg { get; set; }
        private TextBox LabelSearchBar { get; set; }

        private Button LabelClearButton { get; set; }

        private ComponentList<GwenComponent> LabelRows { get; set; }

        public CharacterCosmeticPanel(ImagePanel characterWindow)
        {
            mParentContainer = characterWindow;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Cosmetics");

            LabelSearchContainer = new ImagePanel(mBackground, "LabelSearchContainer");
            LabelSearchLabel = new Label(LabelSearchContainer, "LabelSearchLabel")
            {
                Text = "Search:"
            };
            LabelSearchBg = new ImagePanel(LabelSearchContainer, "LabelSearchBg");
            LabelSearchBar = new TextBox(LabelSearchBg, "LabelSearchBar");
            LabelClearButton = new Button(LabelSearchContainer, "LabelSearchButton")
            {
                Text = "CLEAR"
            };

            LabelsBackground = new ImagePanel(mBackground, "LabelBackground");
            LabelContainer = new ScrollControl(LabelsBackground, "LabelContainer");

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public override void Show()
        {
            LoadLabels();
            Interface.InputBlockingElements.Add(LabelSearchBar);

            base.Show();
        }

        public override void Hide()
        {
            Interface.InputBlockingElements.Remove(LabelSearchBar);
            ClearLabels();
            
            base.Hide();
        }

        private void LoadLabels()
        {
            ClearLabels();
            
            var labels = LabelDescriptor.Lookup.OrderBy(p => p.Value?.Name).Select(p => (LabelDescriptor)p.Value).ToArray();

            var idx = 0;
            var yPadding = 0;
            foreach (var label in labels)
            {
                var row = new LabelRowComponent(LabelContainer, "LabelRow", label.Id, true, false, LabelRows);
                row.Initialize();

                row.SetPosition(row.X, row.Y + (idx * (row.Height + yPadding)));

                idx++;
            }
        }

        private void ClearLabels()
        {
            LabelContainer?.ClearCreatedChildren();
            LabelRows?.DisposeAll();
        }

        public override void Update()
        {
            // Intentionally blank
        }
    }
}
