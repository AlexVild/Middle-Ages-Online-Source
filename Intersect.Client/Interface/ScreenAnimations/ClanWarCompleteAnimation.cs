using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using System;

namespace Intersect.Client.Interface.ScreenAnimations
{
    public class ClanWarCompleteAnimation : ScreenAnimation
    {
        protected override int HFrames => 9;

        protected override int VFrames => 3;

        protected override int FPS => 15;

        protected override bool LoopAnimation => false;

        protected override GameTexture Texture => Globals.ContentManager?.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "clan_wars_end.png");

        protected override string Sound => "al_enhancement.wav";

        protected override float Scale => 4.0f;

        public ClanWarCompleteAnimation(Action callback) : base(callback) { }
    }
}
