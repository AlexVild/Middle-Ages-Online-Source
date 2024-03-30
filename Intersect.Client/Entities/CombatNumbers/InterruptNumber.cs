using Intersect.Client.Core;
using Intersect.Client.General;
using Intersect.Client.Maps;
using Intersect.GameObjects.Events;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Entities.CombatNumbers
{
    public class InterruptNumber : AnimatedCombatNumber
    {
        private Dictionary<int, int> YOffsetsPerFrame { get; set; }

        private int mThreshold { get; set; }

        public InterruptNumber(Guid target,
            int value,
            CombatNumberType type,
            int fallbackX,
            int fallbackY,
            Guid fallbackMapId,
            int threshold,
            Entity visibleTo = null) : base(target, value, type, fallbackX, fallbackY, fallbackMapId, visibleTo)
        {
            FallbackY = fallbackY - 34;
            Looping = false;
            CombatNumberManager.PopulateTextures(this);

            FrameRate = 50; // 50ms frame rate
            FramesInAnimaton = 16;
            FlashingFrames = new int[] { 0, 1 };

            mThreshold = threshold;

            InitializeAnimation();
            CreatedAt = Timing.Global.MillisecondsUtcUnsynced - 1000;
        }

        protected override void SetXOffset()
        {
            XOffset = 0;
        }

        protected override void InitializeAnimation()
        {
            YOffsetsPerFrame = new Dictionary<int, int>();
            var idx = 0;
            foreach (var val in Enumerable.Repeat(0, FramesInAnimaton))
            {
                YOffsetsPerFrame[idx] = val;
                idx++;
            }
            YOffsetsPerFrame[1] = -5;
            YOffsetsPerFrame[3] = 3;
            YOffsetsPerFrame[5] = -2;
            YOffsetsPerFrame[7] = 1;
        }

        protected override void Animate()
        {
            if (YOffsetsPerFrame.ContainsKey(CurrentFrame))
            {
                YOffset += YOffsetsPerFrame[CurrentFrame];
            }
            CenterText();
        }

        protected override void Draw()
        {
            Graphics.DrawGameTexture(
                CurrentBackground,
                Graphics.GetSourceRect(CurrentBackground),
                DestinationRect,
                new Color(255, 255, 255, 255));
            DrawText();
        }

        public override bool Cleanup()
        {
            return Value >= mThreshold || (!Target?.IsCasting ?? true);
        }

        public override bool ShouldRefresh()
        {
            return true;
        }

        protected override float Y
        {
            get
            {
                // If there's no entity there anymore, draw the number where the entity _was_
                if (TargetInvalid)
                {
                    var map = MapInstance.Get(FallbackMapId);
                    if (map == null)
                    {
                        return 0;
                    }
                    return map.GetY() + FallbackY * Options.TileWidth + YOffset - 34;
                }

                return (Target?.WorldPos.Y ?? 0) + YOffset - 34;
            }
        }

        protected override string Str => $"{Value}/{mThreshold}";

        protected override void DrawText()
        {
            Graphics.Renderer.DrawString(Str, Graphics.DamageFont, FontX, FontY, 1.0f, new Color(255, CurrentFontColor.R, CurrentFontColor.G, CurrentFontColor.B));
        }

        public override void ResizeBackground()
        {
            // No need
            return;
        }
    }
}
