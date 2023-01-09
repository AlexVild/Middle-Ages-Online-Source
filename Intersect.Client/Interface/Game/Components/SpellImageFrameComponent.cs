using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.DescriptionWindows;
using System;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.Components
{
    public class SpellImageFrameComponent : ImageFrameComponent
    {
        public SpellDescriptionWindow HoverPanel;

        public Guid SpellId { get; set; }

        public int DescX { get; set; }
        public int DescY { get; set; }

        Action HoverLeaveAction { get; set; }
        Action HoverEnterAction { get; set; }

        private readonly GameTexture RowHoverTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "character_hover_select.png");

        public SpellImageFrameComponent(ImagePanel parent,
            string containerName,
            string frameTexture,
            string imageTexture,
            TextureType imageTextureType,
            int scale,
            int borderWidth,
            Guid spellId,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, frameTexture, imageTexture, imageTextureType, scale, borderWidth, referenceList)
        {
            SpellId = spellId;
        }

        public override void Initialize()
        {
            base.Initialize();
            Image.HoverEnter += Image_HoverEnter;
            Image.HoverLeave += Image_HoverLeave;
            Frame.HoverEnter += Frame_HoverEnter;
            Frame.HoverLeave += Frame_HoverLeave;
        }

        private void Frame_HoverLeave(Base sender, EventArgs arguments)
        {
            HoverLeaveAction?.Invoke();
        }

        private void Frame_HoverEnter(Base sender, EventArgs arguments)
        {
            HoverEnterAction?.Invoke();
        }

        private void Image_HoverLeave(Base sender, EventArgs arguments)
        {
            HoverPanel.Dispose();
            HoverLeaveAction?.Invoke();
        }

        private void Image_HoverEnter(Base sender, EventArgs arguments)
        {
            var mousePos = Globals.InputManager.GetMousePosition();
            HoverPanel = new SpellDescriptionWindow(SpellId, (int)mousePos.X, (int)mousePos.Y);
            HoverEnterAction?.Invoke();
        }

        public void SetDescWindowPosition(float x, float y)
        {
            DescX = (int)x;
            DescY = (int)y;
        }

        public override void Dispose()
        {
            HoverPanel?.Dispose();
            base.Dispose();
        }

        public void SetOnClick(Base.GwenEventHandler<Framework.Gwen.Control.EventArguments.ClickedEventArgs> action)
        {
            Image.Clicked += action;
            Frame.Clicked += action;
        }

        public void SetHoverLeaveAction(Action action)
        {
            HoverLeaveAction = action;
        }

        public void SetHoverEnterAction(Action action)
        {
            HoverEnterAction = action;
        }
    }
}
