using Intersect.Client.Core;
using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Entities.CombatNumbers
{
    public class DamageNumber : AnimatedCombatNumber
    {
        private Dictionary<int, int> YOffsetsPerFrame { get; set; }

        public DamageNumber(Guid target,
            int value,
            CombatNumberType type,
            int fallbackX,
            int fallbackY,
            Guid fallbackMapId,
            Entity visibleTo = null) : base(target, value, type, fallbackX, fallbackY, fallbackMapId, visibleTo)
        {
            Looping = false;
            CombatNumberManager.PopulateTextures(this);

            FrameRate = 50; // 50ms frame rate
            FramesInAnimaton = 16;
            FlashingFrames = new int[] { 0, 1 };

            InitializeAnimation();
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
            if (CurrentFrame < YOffsetsPerFrame.Count)
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
                new Color(Alpha, 255, 255, 255));
            DrawText();
        }
    }
}
