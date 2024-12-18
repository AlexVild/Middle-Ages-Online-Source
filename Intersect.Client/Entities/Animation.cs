using System;
using Atk;
using GLib;
using Gtk;
using Intersect.Client.Core;
using Intersect.Client.Core.Sounds;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Entities
{

    public partial class Animation
    {
        public readonly int DRAW_SCALE = 4;

        public bool AutoRotate;

        private bool disposed = false;

        public bool Hidden;

        public bool InfiniteLoop;

        private bool mDisposeNextDraw;

        private int mLowerFrame;

        private int mLowerLoop;

        private long mLowerTimer;

        private Entity mParent;

        private int mRenderDir;

        private float mRenderX;

        private float mRenderY;

        private bool mShowLower = true;

        private bool mShowUpper = true;

        private MapSound mSound;

        private long mStartTime = Timing.Global.Milliseconds;

        private int mUpperFrame;

        private int mUpperLoop;

        private long mUpperTimer;

        public AnimationBase MyBase;

        private int mZDimension = -1;

        public int Opacity = 255;

        public GameTexture LowerTexture { get; set; }
        public GameTexture UpperTexture { get; set; }

        public Animation(
            AnimationBase animBase,
            bool loopForever,
            bool autoRotate = false,
            int zDimension = -1,
            Entity parent = null
        )
        {
            MyBase = animBase;
            mParent = parent;
            if (MyBase != null)
            {
                mLowerLoop = animBase.Lower.LoopCount;
                mUpperLoop = animBase.Upper.LoopCount;
                mLowerTimer = Timing.Global.Milliseconds + animBase.Lower.FrameSpeed;
                mUpperTimer = Timing.Global.Milliseconds + animBase.Upper.FrameSpeed;
                InfiniteLoop = loopForever;
                AutoRotate = autoRotate;
                mZDimension = zDimension;
                if (displayBasedOnBrightness(MyBase))
                {
                    mSound = Audio.AddMapSound(MyBase.Sound, 0, 0, Guid.Empty, loopForever, 0, 12, parent);
                }
                lock (Graphics.AnimationLock)
                {
                    Graphics.LiveAnimations.Add(this);
                }
                LowerTexture = Globals.ContentManager.GetTexture(
                    GameContentManager.TextureType.Animation, MyBase.Lower.Sprite
                );
                UpperTexture = Globals.ContentManager.GetTexture(
                    GameContentManager.TextureType.Animation, MyBase.Upper.Sprite
                );
            }
            else
            {
                Dispose();
            }
        }

        public void Draw(bool upper = false, bool alternate = false)
        {
            if (Hidden || !displayBasedOnBrightness(MyBase))
            {
                return;
            }

            if (!upper && alternate != MyBase.Lower.AlternateRenderLayer)
            {
                return;
            }

            if (upper && alternate != MyBase.Upper.AlternateRenderLayer)
            {
                return;
            }

            var rotationDegrees = 0f;
            var dontRotate = upper && MyBase.Upper.DisableRotations || !upper && MyBase.Lower.DisableRotations;
            var drawColor = new Color(Opacity, 255, 255, 255);
            if ((AutoRotate || mRenderDir != -1) && !dontRotate)
            {
                rotationDegrees = MathHelper.GetRotationAngleFromDir((ProjectileDirections)mRenderDir);
            }

            if (!upper && mShowLower && mZDimension < 1 || !upper && mShowLower && mZDimension > 0)
            {
                DrawAnimationLayer(MyBase.Lower, LowerTexture, mLowerFrame, rotationDegrees, drawColor);
            }

            if (upper && mShowUpper && mZDimension != 0 || upper && mShowUpper && mZDimension == 0)
            {
                DrawAnimationLayer(MyBase.Upper, UpperTexture, mUpperFrame, rotationDegrees, drawColor);
            }
        }

        public float GetRotationDegrees()
        {
            return MathHelper.GetRotationAngleFromDir((ProjectileDirections)mRenderDir);
        }

        private void DrawAnimationLayer(AnimationLayer layer, GameTexture texture, int frame, float rotationDegrees, Color drawColor)
        {
            if (texture != null && layer.XFrames > 0 && layer.YFrames > 0)
            {
                var frameWidth = texture.GetWidth() / layer.XFrames;
                var frameHeight = texture.GetHeight() / layer.YFrames;
                var scaledWidth = frameWidth * DRAW_SCALE;
                var scaledHeight = frameHeight * DRAW_SCALE;
                Graphics.DrawGameTexture(
                    texture,
                    new FloatRect(
                        frame % layer.XFrames * frameWidth,
                        (float)Math.Floor((double)frame / layer.XFrames) * frameHeight,
                        frameWidth, frameHeight
                    ),
                    new FloatRect(
                        mRenderX - scaledWidth / 2, mRenderY - scaledHeight / 2, scaledWidth, scaledHeight
                    ), drawColor, null, GameBlendModes.None, null, rotationDegrees
                );
            }

            DrawLayerLights(layer, frame, rotationDegrees);
        }

        private void DrawLayerLights(AnimationLayer layer, int frame, float rotationDegrees)
        {
            var currentLight = layer.Lights[frame];
            if (currentLight.Intensity == 0 || currentLight.Size == 0)
            {
                return;
            }
            var offsetX = currentLight.OffsetX;
            var offsetY = currentLight.OffsetY;
            var offset = RotatePoint(
                new Point((int)offsetX, (int)offsetY), new Point(0, 0), rotationDegrees + 180
            );

            Graphics.AddLight(
                (int)mRenderX - offset.X, (int)mRenderY - offset.Y, currentLight.Size,
                currentLight.Intensity, currentLight.Expand,
                currentLight.Color
            );
        }

        public int Width => Math.Max(
            (LowerTexture?.Width ?? 0) / (MyBase?.Lower?.XFrames ?? 1),
            (UpperTexture?.Width ?? 0) / (MyBase?.Upper?.XFrames ?? 1)) * DRAW_SCALE;
        
        public int Height => Math.Max(
            (LowerTexture?.Height ?? 0) / (MyBase?.Lower?.YFrames ?? 1),
            (UpperTexture?.Height ?? 0) / (MyBase?.Upper?.YFrames ?? 1)) * DRAW_SCALE;

        public void EndDraw()
        {
            if (mDisposeNextDraw)
            {
                Dispose();
            }
        }

        static Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
        {
            var angleInRadians = angleInDegrees * (Math.PI / 180);
            var cosTheta = Math.Cos(angleInRadians);
            var sinTheta = Math.Sin(angleInRadians);

            return new Point
            {
                X = (int) (cosTheta * (pointToRotate.X - centerPoint.X) -
                           sinTheta * (pointToRotate.Y - centerPoint.Y) +
                           centerPoint.X),
                Y = (int) (sinTheta * (pointToRotate.X - centerPoint.X) +
                           cosTheta * (pointToRotate.Y - centerPoint.Y) +
                           centerPoint.Y)
            };
        }

        public void Hide()
        {
            Hidden = true;
        }

        public void Show()
        {
            Hidden = false;
        }

        public bool ParentGone()
        {
            if (mParent != null && mParent.IsDisposed())
            {
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            lock (Graphics.AnimationLock)
            {
                if (mSound != null)
                {
                    mSound.Loop = false;
                    if (!MyBase.CompleteSound)
                    {
                        mSound.Stop();
                    }

                    mSound = null;
                }

                Graphics.LiveAnimations.Remove(this);
                disposed = true;
            }
        }

        public void DisposeNextDraw()
        {
            mDisposeNextDraw = true;
        }

        public bool Disposed()
        {
            return disposed;
        }

        public void SetPosition(float worldX, float worldY, int mapx, int mapy, Guid mapId, int dir, int z = 0)
        {
            mRenderX = worldX;
            mRenderY = worldY;
            if (mSound != null)
            {
                mSound.UpdatePosition(mapx, mapy, mapId);
            }

            if (dir > -1)
            {
                mRenderDir = dir;
            }

            mZDimension = z;
        }

        public void Update()
        {
            if (disposed)
            {
                return;
            }

            if (MyBase != null)
            {
                if (mSound != null && displayBasedOnBrightness(MyBase))
                {
                    mSound.Update();
                }

                //Calculate Frames
                var elapsedTime = Timing.Global.Milliseconds - mStartTime;

                //Lower
                if (MyBase.Lower.FrameCount > 0 && MyBase.Lower.FrameSpeed > 0)
                {
                    var realFrameCount = Math.Min(MyBase.Lower.FrameCount, MyBase.Lower.XFrames * MyBase.Lower.YFrames);
                    var lowerFrame = (int) Math.Floor(elapsedTime / (float) MyBase.Lower.FrameSpeed);
                    var lowerLoops = (int) Math.Floor(lowerFrame / (float) realFrameCount);
                    if (lowerLoops > mLowerLoop && !InfiniteLoop)
                    {
                        mShowLower = false;
                    }
                    else
                    {
                        mLowerFrame = lowerFrame - lowerLoops * realFrameCount;
                    }
                }

                //Upper
                if (MyBase.Upper.FrameCount > 0 && MyBase.Upper.FrameSpeed > 0)
                {
                    var realFrameCount = Math.Min(MyBase.Upper.FrameCount, MyBase.Upper.XFrames * MyBase.Upper.YFrames);
                    var upperFrame = (int) Math.Floor(elapsedTime / (float) MyBase.Upper.FrameSpeed);
                    var upperLoops = (int) Math.Floor(upperFrame / (float) realFrameCount);
                    if (upperLoops > mUpperLoop && !InfiniteLoop)
                    {
                        mShowUpper = false;
                    }
                    else
                    {
                        mUpperFrame = upperFrame - upperLoops * realFrameCount;
                    }
                }

                if ((!mShowLower && !mShowUpper) || !displayBasedOnBrightness(MyBase))
                {
                    Dispose();
                }
            }
        }

        public Point AnimationSize()
        {
            var size = new Point(0, 0);

            var tex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Animation, MyBase.Lower.Sprite);
            if (tex != null)
            {
                if (MyBase.Lower.XFrames > 0 && MyBase.Lower.YFrames > 0)
                {
                    var frameWidth = tex.GetWidth() / MyBase.Lower.XFrames;
                    var frameHeight = tex.GetHeight() / MyBase.Lower.YFrames;
                    if (frameWidth > size.X)
                    {
                        size.X = frameWidth;
                    }

                    if (frameHeight > size.Y)
                    {
                        size.Y = frameHeight;
                    }
                }
            }

            tex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Animation, MyBase.Upper.Sprite);
            if (tex != null)
            {
                if (MyBase.Upper.XFrames > 0 && MyBase.Upper.YFrames > 0)
                {
                    var frameWidth = tex.GetWidth() / MyBase.Upper.XFrames;
                    var frameHeight = tex.GetHeight() / MyBase.Upper.YFrames;
                    if (frameWidth > size.X)
                    {
                        size.X = frameWidth;
                    }

                    if (frameHeight > size.Y)
                    {
                        size.Y = frameHeight;
                    }
                }
            }

            foreach (var light in MyBase.Lower.Lights)
            {
                if (light != null)
                {
                    if (light.Size + Math.Abs(light.OffsetX) > size.X)
                    {
                        size.X = light.Size + light.OffsetX;
                    }

                    if (light.Size + Math.Abs(light.OffsetY) > size.Y)
                    {
                        size.Y = light.Size + light.OffsetY;
                    }
                }
            }

            foreach (var light in MyBase.Upper.Lights)
            {
                if (light != null)
                {
                    if (light.Size + Math.Abs(light.OffsetX) > size.X)
                    {
                        size.X = light.Size + light.OffsetX;
                    }

                    if (light.Size + Math.Abs(light.OffsetY) > size.Y)
                    {
                        size.Y = light.Size + light.OffsetY;
                    }
                }
            }

            return size;
        }

        public void SetDir(int dir)
        {
            mRenderDir = dir;
        }

        public static bool displayBasedOnBrightness(AnimationBase animBase)
        {
            if (animBase != null)
            {
                int thresh = animBase.BrightnessThreshold;

                return thresh >= (Graphics.BrightnessLevel / 255) * 100;
            } else
            {
                return false;
            }
        }
    }
}
