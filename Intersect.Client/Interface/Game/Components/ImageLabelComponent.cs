using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;

namespace Intersect.Client.Interface.Game.Components
{
    class ImageLabelComponent
    {
        private Base Parent { get; set; }

        private ImagePanel Container { get; set; }

        private Label Label { get; set; }

        private ImagePanel Image { get; set; }

        public ImageLabelComponent(Base parent)
        {
            Parent = parent;
            Container = new ImagePanel(parent, "ImageLabel");

            Label = new Label(Container, "Label");
            Image = new ImagePanel(Container, "Image");
        }

        public void Initialize(Color labelColor, Color hoverColor, string texture, string label, string toolTipText)
        {
            Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            SetImage(texture);

            Parent?.SetSize(Container.Width, Container.Height);
            Parent?.ProcessAlignments();

            Label.SetTextColor(hoverColor, Label.ControlState.Hovered);
            Label.SetTextColor(labelColor, Label.ControlState.Normal);
            Label.SetText(label);
            Label.SetToolTipText(toolTipText);
        }

        public void SetLabel(string text)
        {
            Label.SetText(text);
        }

        public void SetImage(string imageTexture)
        {
            var image = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, imageTexture);
            if (image == null)
            {
                return;
            }

            Image.Texture = image;
        }
    }
}
