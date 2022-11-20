using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Components;
using System.Collections.Generic;

namespace Intersect.Client.Interface.Game.Components
{
    public class NumberContainerComponent : IGwenComponent
    {
        // GWEN Elements
        public ImagePanel ParentContainer { get; set; }
        private ImagePanel Container { get; set; }
        private Label Label { get; set; }
        private Label Value { get; set; }

        // Properties
        public Color LabelColor { get; set; }
        public Color ValueColor { get; set; }
        public string LabelText { get; set; }
        public string TooltipText { get; set; }

        public NumberContainerComponent(Base parent, string parentContainerName, Color labelColor, Color valueColor, string label, string tooltipText, ComponentList<NumberContainerComponent> referenceList = null)
        {
            ParentContainer = new ImagePanel(parent, parentContainerName);

            LabelColor = labelColor;
            ValueColor = valueColor;
            LabelText = label;
            TooltipText = tooltipText;

            if (referenceList != null)
            {
                referenceList.Add(this);
            }
        }

        public void Initialize()
        {
            Container = new ImagePanel(ParentContainer, "NumberContainer");

            Label = new Label(Container, "Label");
            Value = new Label(Container, "Value");

            Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            Container.SetToolTipText(TooltipText);

            ParentContainer?.SetSize(Container.Width, Container.Height);
            ParentContainer?.ProcessAlignments();

            Label.SetTextColor(LabelColor, Label.ControlState.Normal);
            Label.SetText(LabelText);

            Value.SetTextColor(ValueColor, Label.ControlState.Normal);
        }

        public void SetLabel(string text)
        {
            Label.SetText(text);
        }

        public void SetValue(string text)
        {
            Value.SetText(text);
        }
    }
}
