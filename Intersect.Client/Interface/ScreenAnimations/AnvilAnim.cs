using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using System;

namespace Intersect.Client.Interface.ScreenAnimations
{
    class AnvilAnim : ScreenAnimation
    {
        protected override int HFrames => 5;

        protected override int VFrames => 4;

        protected override int FPS => 15;

        protected override bool LoopAnimation => false;

        protected override GameTexture Texture => Globals.ContentManager?.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "anvil.png");

        protected override string Sound => "al_enhancement.wav";

        public AnvilAnim(Action callback) : base(callback) { }
    }
}
