using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;

namespace Intersect.Client.Interface.Game.Components
{
    class ImageLabelComponent : IGwenComponent
    {
        public ImagePanel ParentContainer { get; set; }

        private ImagePanel Container { get; set; }

        private Label Label { get; set; }

        private ImagePanel Image { get; set; }

        private Color LabelColor { get; set; }

        private Color HoverColor { get; set; }

        private string Texture { get; set; }

        private string LabelText { get; set; }

        private string TooltipText { get; set; }

        public ImageLabelComponent(Base parent, string parentContainerName, Color labelColor, Color hoverColor, string texture, string label, string toolTipText, ComponentList<ImageLabelComponent> referenceList = null)
        {
            ParentContainer = new ImagePanel(parent, parentContainerName);

            LabelColor = labelColor;
            HoverColor = hoverColor;
            Texture = texture;
            LabelText = label;
            TooltipText = toolTipText;

            if (referenceList != null)
            {
                referenceList.Add(this);
            }
        }

        public void Initialize()
        {
            Container = new ImagePanel(ParentContainer, "ImageLabel");

            Label = new Label(Container, "Label");
            Image = new ImagePanel(Container, "Image");

            Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            SetImage(Texture);

            ParentContainer?.SetSize(Container.Width, Container.Height);
            ParentContainer?.ProcessAlignments();

            Label.SetTextColor(HoverColor, Label.ControlState.Hovered);
            Label.SetTextColor(LabelColor, Label.ControlState.Normal);
            Label.SetText(LabelText);
            Label.SetToolTipText(TooltipText);
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
