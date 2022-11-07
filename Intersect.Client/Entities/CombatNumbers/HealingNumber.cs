using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Entities.CombatNumbers
{
    public class HealingNumber : AnimatedCombatNumber
    {
        FloatRect HealingSourceRect { get; set; }
        FloatRect HealingDestRect { get; set; }
        private bool DrawText { get; set; }
        private int LastAnimatedFrame { get; set; } = 4;
        private Dictionary<int, int> FrameHeights { get; set; }

        public HealingNumber(Guid target,
            int value,
            CombatNumberType type,
            int fallbackX,
            int fallbackY,
            Guid fallbackMapId,
            Entity visibleTo = null) : base(target, value, type, fallbackX, fallbackY, fallbackMapId, visibleTo)
        {
            Looping = false;
            CombatNumberManager.PopulateTextures(this);

            FrameRate = 50;
            FramesInAnimaton = 18;
            FlashingFrames = new int[] { 5, 6 };

            InitializeAnimation();
        }

        protected override void InitializeAnimation()
        {
            FrameHeights = new Dictionary<int, int>();
            var idx = 0;
            foreach (var val in Enumerable.Repeat(0, FramesInAnimaton))
            {
                FrameHeights[idx] = val;
                if (idx < LastAnimatedFrame)
                {
                    FrameHeights[idx] = 2 * (idx + 1);
                }
                idx++;
            }
        }

        /// <summary>
        /// This animation draws a wipe effect. It is configured via:
        /// </summary>
        protected override void Animate()
        {
            // If we're done with the wipe
            if (CurrentFrame >= LastAnimatedFrame)
            {
                HealingSourceRect = Graphics.GetSourceRect(CurrentBackground);
                HealingDestRect = DestinationRect;
                DrawText = true;
                return;
            }
            // Otherwise, draw das wipe
            UpdateWipe();
        }

        private void UpdateWipe()
        {
            DrawText = false;
            var backgroundHeight = CurrentBackground.Height;
            HealingSourceRect = new FloatRect(
                0,
                backgroundHeight - FrameHeights[CurrentFrame],
                CurrentBackground.Width,
                FrameHeights[CurrentFrame]
            );
            var height = CurrentBackground.Height * BackgroundTextureScale;
            var currHeight = FrameHeights[CurrentFrame] * BackgroundTextureScale;
            HealingDestRect = new FloatRect(
                X,
                Y + height - currHeight,
                CurrentBackground.Width * BackgroundTextureScale,
                currHeight
            );
        }

        protected override void Draw()
        {
            Graphics.DrawGameTexture(
                CurrentBackground,
                HealingSourceRect,
                HealingDestRect,
                new Color(Alpha, 255, 255, 255));

            // Only draw text after we've finished our animation
            if (DrawText)
            {
                DrawText();
            }
        }
    }
}
