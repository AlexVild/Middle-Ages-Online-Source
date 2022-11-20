using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using System.Collections.Generic;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.Components
{
    public class ImageFrameComponent : GwenComponent
    {
        private int Scale { get; set; }
        private int BorderWidth { get; set; }

        private string FrameTexture { get; set; }
        private ImagePanel Frame { get; set; }

        private string ImageTexture { get; set; }
        private TextureType ImageTextureType { get; set; }
        private ImagePanel Image { get; set; }

        public ImageFrameComponent(ImagePanel parent, 
            string containerName, 
            string frameTexture,
            string imageTexture,
            TextureType imageTextureType,
            int scale,
            int borderWidth,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "ImageFrameComponent", referenceList)
        {
            FrameTexture = frameTexture;
            ImageTextureType = imageTextureType;
            ImageTexture = imageTexture;
            Scale = scale;
            BorderWidth = borderWidth;
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);

            Frame = new ImagePanel(SelfContainer, "Frame");
            Image = new ImagePanel(Frame, "Image");
            
            var frameTexture = Globals.ContentManager.GetTexture(TextureType.Gui, FrameTexture);
            Frame.Texture = frameTexture;
            var imageTexture = Globals.ContentManager.GetTexture(ImageTextureType, ImageTexture);
            Image.Texture = imageTexture;

            Frame.SetSize(frameTexture.GetWidth() * Scale, frameTexture.GetHeight() * Scale);
            Image.SetSize(Frame.Width - (BorderWidth * Scale), Frame.Height - (BorderWidth * Scale));
            Image.AddAlignment(Framework.Gwen.Alignments.Center);
            SelfContainer.SetSize(Frame.Width, Frame.Height);

            FitParentToComponent();
        }
    }
}
