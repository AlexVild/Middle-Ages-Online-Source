using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
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

        private readonly GameTexture RowHoverTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "character_hover_select.png");

        private CharacterNameTagPanel Panel;

        private bool Unlocked;

        public LabelRowComponent(
            Base parent,
            CharacterNameTagPanel panel,
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
            SelfContainer.Clicked += SelfContainer_Clicked;
            SelfContainer.HoverEnter += SelfContainer_HoverEnter;
            SelfContainer.HoverLeave += SelfContainer_HoverLeave;

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

            if (CharacterNameTagPanelController.UnlockedLabels.TryGetValue(LabelId, out var isNew))
            {
                SetUnlocked(isNew);
            }
            else
            {
                SetLocked();
            }

            if (Globals.Me?.LabelDescriptorId == LabelId)
            {
                Select();
            }
        }

        private void SelfContainer_HoverLeave(Base sender, EventArgs arguments)
        {
            if (Unlocked)
            {
                SelfContainer.Texture = null;
            }
        }

        private void SelfContainer_HoverEnter(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }

            if (Unlocked)
            {
                SelfContainer.Texture = RowHoverTexture;
            }
        }

        private void SelfContainer_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            UseCheckbox.IsChecked = !UseCheckbox.IsChecked;
        }

        private void UseCheckbox_CheckChanged(Base sender, EventArgs arguments)
        {
            if (ForceSelection)
            {
                ForceSelection = false;
                return;
            }

            // Selecting a new label
            if (CharacterNameTagPanelController.SelectedLabelIndex != Index)
            {
                Panel.UncheckPrevious();
            }

            // Removing a label
            if (!UseCheckbox.IsChecked)
            {
                CharacterNameTagPanelController.SelectedLabelIndex = -1;
                PacketSender.SendSetLabelPacket(Guid.Empty);
                return;
            }

            CharacterNameTagPanelController.SelectedLabelIndex = Index;
            PacketSender.SendSetLabelPacket(Label.Id);
        }

        public void SetLocked()
        {
            Unlocked = false;
            UseCheckbox.Disable();
            LabelName.SetTextColor(LockedColor, Framework.Gwen.Control.Label.ControlState.Normal);
            UseCheckbox.SetToolTipText(string.Empty);
        }

        public void SetUnlocked(bool isNew)
        {
            Unlocked = true;
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
