using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Maps;
using Intersect.GameObjects.Events;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Entities.CombatNumbers
{
    public abstract class CombatNumber
    {
        private const long RefreshWindow = 250;
        private const long DisplayLengthMs = 650;
        private const long FadeFrames = 50;
        private const int FadeRate = 55; // How much alpha to lose per update
        private long NextFadeUpdate;

        /// <summary>
        /// Used to identify a unique combat number by its <see cref="Target"/> and <see cref="Type"/>
        /// </summary>
        public string Id { get; set; }

        protected const float BackgroundTextureScale = 4.0f;

        protected Entity Target { get; set; }
        protected Entity VisibleTo { get; set; }

        internal CombatNumberType Type { get; set; }

        protected long CreatedAt { get; set; }
        protected long DestroyAt { get; set; }
        protected long FrameRate { get; set; }
        protected long CleanupTime { get; set; }
        private bool NeedsCleaned { get; set; }
        private bool FadeOut { get; set; }
        protected int CurrentFrame { get; set; }
        protected int FramesInAnimaton { get; set; }
        protected int[] FlashingFrames { get; set; }

        protected int FallbackX { get; set; }
        protected int FallbackY { get; set; }
        protected Guid FallbackMapId { get; set; }

        private float _xOffset;
        private float _yOffset;
        protected float XOffset { get => _xOffset + SpawnXOffset; set => _xOffset = value + SpawnXOffset; }
        protected float YOffset { get => _yOffset + SpawnYOffset; set => _yOffset = value + SpawnYOffset; }
        protected float SpawnXOffset { get; set; }
        protected float SpawnYOffset { get; set; }

        protected float X
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
                    return map.GetX() + FallbackX * Options.TileWidth + XOffset;
                }

                return (Target?.WorldPos.X ?? 0) + XOffset;
            }
        }

        protected float Y
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
                    return map.GetY() + FallbackY * Options.TileWidth + YOffset;
                }

                return (Target?.WorldPos.Y ?? 0) + YOffset;
            }
        }

        protected FloatRect DestinationRect => new FloatRect(
            X, 
            Y, 
            CurrentBackground.Width * BackgroundTextureScale, 
            CurrentBackground.Height * BackgroundTextureScale
        );

        private int BackgroundVPadding = 8;

        protected bool Animated { get; set; }
        
        protected bool IsFlashing { get; set; }
        protected int Alpha { get; set; }

        internal GameTexture BackgroundTextureFlash { get; set; }
        internal GameTexture BackgroundTexture { get; set; }
        protected GameTexture CurrentBackground => IsFlashing ? BackgroundTextureFlash : BackgroundTexture;
        
        public int Value { get; set; }
        internal Color FontColor { get; set; }
        internal Color FontFlashColor { get; set; }
        protected Color CurrentFontColor => IsFlashing
            ? new Color(Alpha, FontFlashColor.R, FontFlashColor.G, FontFlashColor.B)
            : new Color(Alpha, FontColor.R, FontColor.G, FontColor.B);
        protected float FontX { get; set; }
        protected float FontY { get; set; }

        protected bool TargetInvalid => Target == null || !Globals.Entities.ContainsKey(Target.Id);

        private string GenerateKey(Guid targetId, CombatNumberType type)
        {
            return $"{targetId}_{type}";
        }

        public CombatNumber(Guid targetId, 
            int value, 
            CombatNumberType type, 
            int fallbackX,
            int fallbackY,
            Guid fallbackMapId, 
            Entity visibleTo = null)
        {
            Id = GenerateKey(targetId, type);

            Globals.Entities.TryGetValue(targetId, out var target);
            Target = target;
            Value = value;
            Type = type;
            VisibleTo = visibleTo;
            Alpha = 255;
            CurrentFrame = 0;
            CreatedAt = Timing.Global.Milliseconds;
            CleanupTime = CreatedAt + DisplayLengthMs;

            FallbackX = fallbackX;
            FallbackY = fallbackY;
            FallbackMapId = fallbackMapId;

            var maxWidth = (Target?.WorldPos.Width ?? 0) / 8;
            SpawnXOffset = Randomization.Next((int) -maxWidth, (int)maxWidth + 1);
        }

        protected void CenterText()
        {
            var textRect = Graphics.Renderer.MeasureText(Value.ToString(), Graphics.DamageFont, 1.0f);
            var textLength = textRect.X;
            var textHeight = textRect.Y;

            FontX = DestinationRect.CenterX - textLength / 2;
            // I have no idea why this +2 is necessary, but it centers incorrectly otherwise
            FontY = DestinationRect.CenterY - textHeight / 2 + 2; 
        }

        protected abstract void Animate();

        protected abstract void Draw();

        private void ResetPosition()
        {
            if (!TargetInvalid)
            {
                XOffset = (Target.WorldPos.Width / 2) - ((CurrentBackground.Width * BackgroundTextureScale) / 2);
            }
            else
            {
                XOffset = (Options.TileWidth / 2) - ((CurrentBackground.Width * BackgroundTextureScale) / 2);
            }
            YOffset = (CurrentBackground.Height * BackgroundTextureScale * -1) - BackgroundVPadding;
            CenterText();
        }

        public void UpdateAndDraw()
        {
            if (NeedsCleaned)
            {
                return;
            }

            if (Timing.Global.Milliseconds > CleanupTime && !FadeOut)
            {
                NextFadeUpdate = Timing.Global.Milliseconds + FadeFrames;
                FadeOut = true;
            }

            if (FadeOut)
            {
                UpdateFade();
            }

            ResetPosition();
            AdvanceAnimation();
            UpdateFlash();
            if (Animated)
            {
                Animate();
            }
            Draw();
        }

        public void Refresh(int newValue)
        {
            Value += newValue;
            Alpha = 255;
            CreatedAt = Timing.Global.Milliseconds;
            CleanupTime = CreatedAt + DisplayLengthMs;
            FadeOut = false;
        }

        private void UpdateFade()
        {
            if (Timing.Global.Milliseconds > NextFadeUpdate)
            {
                Alpha -= FadeRate;
                NextFadeUpdate = Timing.Global.Milliseconds + FadeFrames;
            }

            Alpha = MathHelper.Clamp(Alpha, 0, 255);
            if (Alpha <= 0)
            {
                NeedsCleaned = true;
            }
        }

        public bool ShouldRefresh()
        {
            return Timing.Global.Milliseconds - RefreshWindow < CreatedAt;
        }

        public bool Cleanup()
        {
            return NeedsCleaned;
        }

        private void UpdateFlash()
        {
            if (!Animated)
            {
                return;
            }
            IsFlashing = FlashingFrames.Contains(CurrentFrame);
        }

        protected virtual void AdvanceAnimation()
        {
            // Intentionally blank
        }

        protected void DrawText()
        {
            Graphics.Renderer.DrawString(Value.ToString(), Graphics.DamageFont, FontX, FontY, 1.0f, CurrentFontColor);
        }
    }
}
