using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.DescriptionWindows;
using System;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.Components
{
    public class ChallengeImageFrameComponent : ImageFrameComponent
    {
        public ChallengeDescriptionWindow HoverPanel;

        public int DescX { get; set; }
        public int DescY { get; set; }

        public Guid ChallengeId { get; set; }

        public ChallengeImageFrameComponent(ImagePanel parent,
            string containerName,
            string frameTexture,
            string imageTexture,
            TextureType imageTextureType,
            int scale,
            int borderWidth,
            Guid challengeId,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, frameTexture, imageTexture, imageTextureType, scale, borderWidth, referenceList)
        {
            ChallengeId = challengeId;
        }

        public override void Initialize()
        {
            base.Initialize();
            Image.HoverEnter += Image_HoverEnter;
            Image.HoverLeave += Image_HoverLeave;
        }

        private void Image_HoverLeave(Base sender, EventArgs arguments)
        {
            HoverPanel?.Dispose();
        }

        private void Image_HoverEnter(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }

            var mousePos = Globals.InputManager.GetMousePosition();
            HoverPanel = new ChallengeDescriptionWindow(ChallengeId, (int)mousePos.X, (int)mousePos.Y);
        }

        public override void Dispose()
        {
            HoverPanel?.Dispose();
            base.Dispose();
        }
    }
}
