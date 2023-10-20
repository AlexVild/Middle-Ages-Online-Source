using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.Components
{
    public class EnhancementItemComponent : ImageFrameComponent
    {
        public EnhancementStudyDescriptionWindow HoverWindow;

        private Guid ItemId { get; set; }
        
        private Guid EnhancementId { get; set; }

        private int X { get; set; }
        private int Y { get; set; }

        public EnhancementItemComponent(Base parent,
            string containerName,
            string frameTexture,
            string imageTexture,
            TextureType imageTextureType,
            int scale,
            int borderWidth,
            Guid itemId,
            Guid enhancementId,
            int x,
            int y,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, frameTexture, imageTexture, imageTextureType, scale, borderWidth, referenceList)
        {
            ItemId = itemId;
            EnhancementId = enhancementId;
            X = x;
            Y = y;
        }

        public override void Initialize()
        {
            base.Initialize();
            Image.HoverEnter += Image_HoverEnter;
            Image.HoverLeave += Image_HoverLeave;
        }

        private void Image_HoverLeave(Base sender, EventArgs arguments)
        {
            HoverWindow?.Dispose();
        }

        private void Image_HoverEnter(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }

            HoverWindow = new EnhancementStudyDescriptionWindow(ItemId, EnhancementId, X, Y);
        }

        public override void Dispose()
        {
            HoverWindow?.Dispose();
            base.Dispose();
        }
    }
}
