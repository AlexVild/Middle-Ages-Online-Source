using Intersect.Attributes;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.ScreenAnimations
{
    class DeconstructorAnim : ScreenAnimation
    {
        protected override int HFrames => 5;

        protected override int VFrames => 4;

        protected override int FPS => 12;

        protected override bool LoopAnimation => false;

        protected override GameTexture Texture => Globals.ContentManager?.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "deconstruction.png");

        protected override string Sound => "al_deconstruction.wav";

        public DeconstructorAnim(Action callback) : base(callback) { }
    }
}
