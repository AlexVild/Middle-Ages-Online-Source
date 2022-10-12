using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Utilities;
using System;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.HUD
{
    public class Gravestone
    {
        private const int HFrames = 4;
        private const int VFrames = 4;
        private int CurrentHFrame;
        private int CurrentVFrame;
        private const int FPS = 3;

        private float CenterX => Graphics.CurrentView.CenterX;
        private float CenterY => Graphics.CurrentView.CenterY;
        
        private GameTexture Texture;

        private long LastUpdateTime;
        private bool ThrownLog;

        public Gravestone()
        {
            Texture = Globals.ContentManager.GetTexture(TextureType.Gui, "gravestone.png");
            LastUpdateTime = Timing.Global.Milliseconds;
        }

        public void ResetDrawing()
        {
            CurrentHFrame = 0;
            CurrentVFrame = 0;
            LastUpdateTime = Timing.Global.Milliseconds;
        }

        public void Draw()
        {
            try
            {
                var srcWidth = Texture.GetWidth() / HFrames;
                var srcHeight = Texture.GetHeight() / VFrames;
                var srcX = srcWidth * CurrentHFrame;
                var srcY = srcHeight * CurrentVFrame;

                var centerX = CenterX - (srcWidth / 2);
                var centerY = CenterY - (srcHeight / 2);

                Graphics.DrawGameTexture(Texture,
                    new FloatRect(srcX, srcY, srcWidth, srcHeight),
                    new FloatRect(centerX, centerY, srcWidth, srcHeight),
                    Color.White);

                Animate();
                ThrownLog = false;
            } catch (Exception e)
            {
                if (!ThrownLog)
                {
                    Logging.Log.Error(e.Message);
                    Logging.Log.Error(e.StackTrace);
                    ThrownLog = true;
                }
            }
        }

        public void Animate()
        {
            var now = Timing.Global.Milliseconds;
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
                        CurrentVFrame = 0;
                    }
                }
                LastUpdateTime = Timing.Global.Milliseconds;
            }
        }
    }
}
