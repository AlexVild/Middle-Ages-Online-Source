using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;

namespace Intersect.Client.Interface.Game.Components
{
    public class NumberContainerComponent
    {
        private Base Parent { get; set; }

        private ImagePanel Container { get; set; }

        private Label Label { get; set; }

        private Label Value { get; set; }

        public NumberContainerComponent(Base parent)
        {
            Parent = parent;
            Container = new ImagePanel(parent, "NumberContainer");

            Label = new Label(Container, "Label");
            Value = new Label(Container, "Value");
        }

        public void Initialize(Color labelColor, Color valueColor, string label, string toolTipText)
        {
            Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            Container.SetToolTipText(toolTipText);

            Parent?.SetSize(Container.Width, Container.Height);
            Parent?.ProcessAlignments();

            Label.SetTextColor(labelColor, Label.ControlState.Normal);
            Label.SetText(label);

            Value.SetTextColor(valueColor, Label.ControlState.Normal);
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
