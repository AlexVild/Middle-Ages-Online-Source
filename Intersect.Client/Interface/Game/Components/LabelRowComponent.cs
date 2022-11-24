using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Components
{
    public class LabelRowComponent : GwenComponent
    {
        private Label LabelName { get; set; }

        private Guid LabelId { get; set; }
        private LabelDescriptor Label { get; set; }

        private Label LabelHint { get; set; }

        private Button ShowMore { get; set; }
        private const string ShowMoreText = "Show More...";

        private CheckBox UseCheckbox { get; set; }
        private const string UseToolTip = "Set as name tag";

        public int X => ParentContainer.X;
        public int Y => ParentContainer.Y;
        public int Height => ParentContainer.Height;

        private bool ForceSelection { get; set; }

        public int Index { get; set; }

        private readonly Color LockedColor = new Color(255, 169, 169, 169);
        private readonly Color UnlockedColor = new Color(255, 255, 255, 255);

        private CharacterCosmeticPanel Panel;

        public LabelRowComponent(
            Base parent,
            CharacterCosmeticPanel panel,
            string containerName,
            Guid labelId,
            int idx,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "LabelRowComponent", referenceList)
        {
            Panel = panel;
            LabelId = labelId;
            Label = LabelDescriptor.Get(LabelId);
            Index = idx;
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);

            LabelName = new Label(SelfContainer, "LabelName")
            {
                Text = Label.DisplayName
            };
            LabelHint = new Label(SelfContainer, "LabelHint")
            {
                Text = Label.Hint
            };
            
            ShowMore = new Button(SelfContainer, "ShowMoreButton")
            {
                Text = ShowMoreText
            };
            ShowMore.Clicked += ShowMore_Clicked;

            UseCheckbox = new CheckBox(SelfContainer, "UseCheckbox");
            UseCheckbox.CheckChanged += UseCheckbox_CheckChanged;

            base.Initialize();
            FitParentToComponent();

            var hintWidth = 212;
            var truncatedText = UiHelper.TruncateString(LabelHint.Text, LabelHint.Font, hintWidth);

            ShowMore.Hide();
            if (truncatedText != LabelHint.Text)
            {
                LabelHint.SetText(truncatedText);
                ShowMore.Show();
                ShowMore.SetPosition(ShowMore.X, LabelHint.Y + LabelHint.Height);
            }

            if (CharacterCosmeticPanelController.UnlockedLabels.TryGetValue(Label.Id, out var isNew))
            {
                SetUnlocked(isNew);
            }
            else
            {
                SetLocked();
            }

            if (Globals.Me?.LabelDescriptorId == Label.Id)
            {
                Select();
            }
        }

        private void UseCheckbox_CheckChanged(Base sender, EventArgs arguments)
        {
            if (ForceSelection)
            {
                ForceSelection = false;
                return;
            }

            // Selecting a new label
            if (CharacterCosmeticPanelController.SelectedLabelIndex != Index)
            {
                Panel.UncheckPrevious();
            }

            // Removing a label
            if (!UseCheckbox.IsChecked)
            {
                CharacterCosmeticPanelController.SelectedLabelIndex = -1;
                PacketSender.SendSetLabelPacket(Guid.Empty);
                return;
            }

            CharacterCosmeticPanelController.SelectedLabelIndex = Index;
            PacketSender.SendSetLabelPacket(Label.Id);
        }

        public void SetLocked()
        {
            UseCheckbox.Disable();
            LabelName.SetTextColor(LockedColor, Framework.Gwen.Control.Label.ControlState.Normal);
            UseCheckbox.SetToolTipText(string.Empty);
        }

        public void SetUnlocked(bool isNew)
        {
            UseCheckbox.Enable();
            LabelName.SetTextColor(UnlockedColor, Framework.Gwen.Control.Label.ControlState.Normal);
            if (isNew)
            {
                LabelName.SetText($"{LabelName.Text}*");
            }
            UseCheckbox.SetToolTipText(UseToolTip);
        }

        public void SetPosition(int x, int y)
        {
            ParentContainer.SetPosition(x, y);
        }

        private void ShowMore_Clicked(Base control, EventArgs args)
        {
            _ = new InputBox(
                Label.DisplayName,
                Label.Hint, true,
                InputBox.InputType.OkayOnly, null, null, null
            );
        }

        public void Unselect()
        {
            ForceSelection = true;
            UseCheckbox.IsChecked = false;
        }

        public void Select()
        {
            ForceSelection = true;
            UseCheckbox.IsChecked = true;
        }
    }
}
