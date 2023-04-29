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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public static class CharacterNameTagPanelController
    {
        /// <summary>
        /// Dictionary of <LabelDescriptorId, LabelIsNew>
        /// </summary>
        public static Dictionary<Guid, bool> UnlockedLabels { get; set; } = new Dictionary<Guid, bool>();

        public static bool RefreshLabels { get; set; } = false;

        public static int SelectedLabelIndex { get; set; } = -1;
    }

    public class CharacterNameTagPanel : CharacterWindowPanel
    {
        private ImagePanel LabelSearchContainer { get; set; }

        private ImagePanel LabelsBackground { get; set; }

        private ScrollControl LabelContainer { get; set; }

        private bool RefreshLabels
        {
            get => CharacterNameTagPanelController.RefreshLabels;
            set => CharacterNameTagPanelController.RefreshLabels = value;
        }
        private Label LabelSearchLabel { get; set; }
        private ImagePanel LabelSearchBg { get; set; }
        private TextBox LabelSearchBar { get; set; }
        private string SearchTerm 
        {
            get => LabelSearchBar.Text;
            set => LabelSearchBar.SetText(value);
        }

        private Button LabelClearButton { get; set; }

        private ComponentList<GwenComponent> LabelRows { get; set; } = new ComponentList<GwenComponent>();

        public CharacterNameTagPanel(ImagePanel characterWindow)
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
            LabelSearchBar.TextChanged += LabelSearchBar_textChanged;

            LabelClearButton = new Button(LabelSearchContainer, "LabelSearchButton")
            {
                Text = "CLEAR"
            };
            LabelClearButton.Clicked += LabelClearButton_Clicked;

            LabelsBackground = new ImagePanel(mBackground, "LabelBackground");
            LabelContainer = new ScrollControl(LabelsBackground, "LabelContainer");

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        private void LabelClearButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            SearchTerm = string.Empty;
        }

        private void LabelSearchBar_textChanged(Base control, EventArgs args)
        {
            RefreshLabels = true;
        }

        public override void Show()
        {
            PacketSender.SendRequestLabelInfo();
            Interface.InputBlockingElements.Add(LabelSearchBar);

            base.Show();
        }

        public override void Hide()
        {
            Interface.InputBlockingElements.Remove(LabelSearchBar);
            ClearLabels();
            SearchTerm = string.Empty;
            
            base.Hide();
        }

        private void LoadLabels()
        {
            ClearLabels();
            
            var labels = LabelDescriptor.Lookup
                .OrderBy(p => ((LabelDescriptor)p.Value)?.DisplayName)
                .Select(p => (LabelDescriptor)p.Value)
                .Where(p => string.IsNullOrEmpty(SearchTerm) || SearchHelper.IsSearchable(p.DisplayName, SearchTerm))
                .ToArray();

            var idx = 0;
            var yPadding = 0;
            foreach (var label in labels)
            {
                if (label.ShowOnlyUnlocked && (!CharacterNameTagPanelController.UnlockedLabels.TryGetValue(label.Id, out var unlockedLabel)))
                {
                    continue;
                }

                var row = new LabelRowComponent(LabelContainer, this, "LabelRow", label.Id, idx, LabelRows);
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

        public void UncheckPrevious()
        {
            if (LabelRows == null || LabelRows.Count <= CharacterNameTagPanelController.SelectedLabelIndex)
            {
                return;
            }

            if (CharacterNameTagPanelController.SelectedLabelIndex == -1)
            {
                return;
            }
            
            var row = (LabelRowComponent)LabelRows[CharacterNameTagPanelController.SelectedLabelIndex];
            row.Unselect();
        }

        public override void Update()
        {
            if (RefreshLabels)
            {
                LoadLabels();
                RefreshLabels = false;
            }
        }
    }
}
