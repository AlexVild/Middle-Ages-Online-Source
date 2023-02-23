using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Character.Panels;
using System;

namespace Intersect.Client.Interface.Game.Components
{
    public class EnhancementRowComponent : GwenComponent
    {
        private Label Bonus { get; set; }
        private string BonusText { get; set; }

        private Label Separator { get; set; }
        const string SeparatorText = "...";

        private Label Percentage { get; set; }
        private string Value { get; set; }

        private ImagePanel Background { get; set; }

        private string TooltipText { get; set; }

        private Color TextColor => new Color(255, 255, 255, 255);
        private Color TextHoveredColor => new Color(255, 169, 169, 169);

        public int X => ParentContainer?.X ?? default;

        public int Y => ParentContainer?.Y ?? default;
        public int Width => ParentContainer?.Width ?? default;
        public int Height => ParentContainer?.Height ?? default;

        public EnhancementRowComponent(
            Base parent,
            string containerName,
            string bonus,
            string value,
            string tooltip,
            ComponentList<GwenComponent> referenceList = null,
            string fileName = "EnhancementRowComponent") : base(parent, containerName, fileName, referenceList)
        {
            BonusText = bonus;
            Value = value;
            TooltipText = tooltip;
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            SelfContainer.HoverEnter += SelfContainer_HoverEnter;
            SelfContainer.HoverLeave += SelfContainer_HoverLeave;
            SelfContainer.SetToolTipText(TooltipText);

            var percent = $"{Value}";
            Percentage = new Label(SelfContainer, "Percentage")
            {
                Text = percent
            };

            Separator = new Label(SelfContainer, "Separator")
            {
                Text = SeparatorText
            };

            Bonus = new Label(SelfContainer, "BonusName")
            {
                Text = BonusText
            };

            base.Initialize();
            FitParentToComponent();

            Bonus.SetTextColor(TextColor, Label.ControlState.Normal);
            Bonus.SetTextColor(TextHoveredColor, Label.ControlState.Hovered);
        }

        private void SelfContainer_HoverEnter(Base sender, EventArgs arguments)
        {
            Bonus.SetTextColor(TextHoveredColor, Label.ControlState.Normal);
            Separator.SetTextColor(TextHoveredColor, Label.ControlState.Normal);
            Percentage.SetTextColor(TextHoveredColor, Label.ControlState.Normal);
        }

        private void SelfContainer_HoverLeave(Base sender, EventArgs arguments)
        {
            Bonus.SetTextColor(TextColor, Label.ControlState.Normal);
            Separator.SetTextColor(TextColor, Label.ControlState.Normal);
            Percentage.SetTextColor(TextColor, Label.ControlState.Normal);
        }

        public void SetPosition(int x, int y)
        {
            ParentContainer?.SetPosition(x, y);
        }

        public void SetSize(int width, int height)
        {
            ParentContainer?.SetSize(width, height);
            ParentContainer?.ProcessAlignments();
        }
    }
}
