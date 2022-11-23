using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Components;
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

        private bool IsUnlocked { get; set; }
        private bool IsUsed { get; set; }

        public int X => ParentContainer.X;
        public int Y => ParentContainer.Y;
        public int Height => ParentContainer.Height;

        public LabelRowComponent(
            Base parent,
            string containerName,
            Guid labelId,
            bool isUnlocked,
            bool isUsed,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "LabelRowComponent", referenceList)
        {
            LabelId = labelId;
            Label = LabelDescriptor.Get(LabelId);

            IsUnlocked = isUnlocked;
            IsUsed = isUsed;
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
            UseCheckbox.SetToolTipText(UseToolTip);

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
    }
}
