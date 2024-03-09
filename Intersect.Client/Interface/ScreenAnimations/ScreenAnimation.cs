using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Utilities;
using System;

namespace Intersect.Client.Interface.ScreenAnimations
{
    public abstract class ScreenAnimation
    {
        /// <summary>
        /// If we exceed this time since last update, reset the animation
        /// </summary>
        private const long ResetTime = 3000;

        /// <summary>
        /// How many horizontal frames the animation is
        /// </summary>
        protected abstract int HFrames { get; }
        
        /// <summary>
        /// How many vertical frames the animation is
        /// </summary>
        protected abstract int VFrames { get; }

        /// <summary>
        /// How many frames a second to animate the image
        /// </summary>
        protected abstract int FPS { get; }

        /// <summary>
        /// The scale to draw at
        /// </summary>
        protected virtual float Scale { get; } = 1.0f;

        /// <summary>
        /// The current horizontal frame of animation
        /// </summary>
        protected int CurrentHFrame { get; set;  }

        /// <summary>
        /// The current vertical frame of animation
        /// </summary>
        protected int CurrentVFrame { get; set; }

        protected abstract bool LoopAnimation { get; }

        protected virtual Action AnimationEnd { get; set; } = null;

        /// <summary>
        /// Helper for finding centerX of current view
        /// </summary>
        protected float CenterX => Graphics.CurrentView.CenterX;

        /// <summary>
        /// Helper for finding centerY of current view
        /// </summary>
        protected float CenterY => Graphics.CurrentView.CenterY;

        /// <summary>
        /// The <see cref="GameTexture"/> of which to draw
        /// </summary>
        protected abstract GameTexture Texture { get; }
        protected abstract string Sound { get; }

        private bool ThrownLog { get; set; }
        private long LastUpdateTime { get; set; }
        public bool Done { get; protected set; }
        private bool SoundPlayed { get; set; }

        public ScreenAnimation(Action animationEndCallback = null)
        {
            AnimationEnd = animationEndCallback;
            LastUpdateTime = Timing.Global.MillisecondsUtcUnsynced;
        }

        /// <summary>
        /// Resets the animation state to the beginning
        /// </summary>
        public virtual void ResetAnimation()
        {
            CurrentHFrame = 0;
            CurrentVFrame = 0;
            LastUpdateTime = Timing.Global.MillisecondsUtcUnsynced;
            Done = false;
            SoundPlayed = false;
        }

        /// <summary>
        /// Draws the animation. If you want to hide the animation, simply don't draw it in your calling logic.
        /// </summary>
        public void Draw()
        {
            try
            {
                if (Done)
                {
                    // Don't draw if we're finished - image needs reset
                    return;
                }
                if (LoopAnimation && Timing.Global.MillisecondsUtcUnsynced - LastUpdateTime >= ResetTime)
                {
                    ResetAnimation();
                }

                if (!SoundPlayed && CurrentHFrame == 0 && CurrentVFrame == 0 && !string.IsNullOrEmpty(Sound))
                {
                    Audio.AddGameSound(Sound, false);
                    SoundPlayed = true;
                }

                var srcWidth = Texture.GetWidth() / HFrames;
                var srcHeight = Texture.GetHeight() / VFrames;
                var srcX = srcWidth * CurrentHFrame;
                var srcY = srcHeight * CurrentVFrame;

                var centerX = CenterX - (srcWidth * Scale / 2);
                var centerY = CenterY - (srcHeight * Scale / 2);

                Graphics.DrawGameTexture(Texture,
                    new FloatRect(srcX, srcY, srcWidth, srcHeight),
                    new FloatRect(centerX, centerY, srcWidth * Scale, srcHeight * Scale),
                    Color.White);

                Animate();
                ThrownLog = false;
            }
            catch (Exception e)
            {
                // Safely throw errors - no sense crashing the client over a failed animation load
                if (!ThrownLog)
                {
                    Logging.Log.Error(e.Message);
                    Logging.Log.Error(e.StackTrace);
                    ThrownLog = true;
                }
            }
        }

        /// <summary>
        /// Animates the image
        /// </summary>
        private void Animate()
        {
            var now = Timing.Global.MillisecondsUtcUnsynced;
            var frameTime = 1000 / FPS;

            if (now - LastUpdateTime > frameTime)
            {
                CurrentHFrame++;
                if (CurrentHFrame >= HFrames)
                {
                    CurrentHFrame = 0;
                    CurrentVFrame++;
                    if (CurrentVFrame >= VFrames)
                    {
                        if (LoopAnimation)
                        {
                            ResetAnimation();
                        }
                        else
                        {
                            if (AnimationEnd != null)
                            {
                                AnimationEnd();
                            }
                            Done = true;
                        }
                    }
                }
                LastUpdateTime = Timing.Global.MillisecondsUtcUnsynced;
            }
        }
    }
}
