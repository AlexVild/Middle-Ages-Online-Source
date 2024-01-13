using Intersect.Client.Core;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using System;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.Components
{
    public class ImageFrameComponent : GwenComponent
    {
        private int Scale { get; set; }
        private int BorderWidth { get; set; }

        private string FrameTexture { get; set; }
        protected ImagePanel Frame { get; set; }

        private string ImageTexture { get; set; }
        private TextureType ImageTextureType { get; set; }
        public ImagePanel Image { get; set; }

        public int X => ParentContainer.X;
        public int Y => ParentContainer.Y;
        public int Width => ParentContainer.Width;
        public int Height => ParentContainer.Height;
        
        public int Bottom => ParentContainer.Bottom;

        public ImageFrameComponent(Base parent, 
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

        public void SetPosition(int x, int y)
        {
            ParentContainer.SetPosition(x, y);
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);

            Frame = new ImagePanel(SelfContainer, "Frame");
            Image = new ImagePanel(Frame, "Image");

            SelfContainer.LoadJsonUi(UI.InGame, Graphics.Renderer.GetResolutionString());

            var frameTexture = Globals.ContentManager.GetTexture(TextureType.Gui, FrameTexture);
            Frame.Texture = frameTexture;
            var imageTexture = Globals.ContentManager.GetTexture(ImageTextureType, ImageTexture);
            Image.Texture = imageTexture;

            Frame.SetSize(frameTexture.GetWidth() * Scale, frameTexture.GetHeight() * Scale);

            // fit to texture height
            FitImageToFrame();

            SelfContainer.SetSize(Frame.Width, Frame.Height);

            FitParentToComponent();
        }

        private void FitImageToFrame()
        {
            if (Image.Texture != null)
            {
                var frameWidth = Frame.Width - BorderWidth;
                var frameHeight = Frame.Height - BorderWidth;

                Image.SetSize(frameWidth, frameHeight);
                Image.AddAlignment(Framework.Gwen.Alignments.Center);
                Image.ProcessAlignments();
            }
        }

        public void SetImageRenderColor(Color color)
        {
            Image.RenderColor = color;
        }

        public void SetTooltipText(string text)
        {
            Frame.SetToolTipText(text);
            Image.SetToolTipText(text);
        }

        public void SetImageTexture(string texture)
        {
            Image.Texture = Globals.ContentManager.GetTexture(ImageTextureType, texture);
            FitImageToFrame();
        }
    }
}
