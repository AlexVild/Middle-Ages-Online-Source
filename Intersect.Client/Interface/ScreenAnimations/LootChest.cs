using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using System;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.ScreenAnimations
{
    public class LootChest : ScreenAnimation
    {
        protected override int HFrames => 8;

        protected override int VFrames => 2;

        protected override int FPS => 17;

        protected override bool LoopAnimation => false;

        protected override GameTexture Texture => Globals.ContentManager?.GetTexture(TextureType.Gui, "loot_chest.png");

        protected override string Sound => "al_loot_chest.wav";

        public LootChest(Action callback) : base(callback) { }
    }
}
