using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using System;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.ScreenAnimations
{
    public class Gravestone : ScreenAnimation
    {
        protected override int HFrames => 4;
        protected override int VFrames => 4;
        protected override int FPS => 3;
        protected override bool LoopAnimation => true;
        protected override Action AnimationEnd => null;
        protected override GameTexture Texture => Globals.ContentManager.GetTexture(TextureType.Gui, "gravestone.png");
    }
}
