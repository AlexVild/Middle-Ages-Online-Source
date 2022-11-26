using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.General;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Toasts
{
    public static class ToastWindow
    {
        const float SmallWidth = 1280;
        const float BorderWidth = 4;
        const int ContainerPadding = 116;
        const int SlideInTime = 150;
        const long DisplayTime = 8000;

        private static long FlashEndTime;
        const long FlashTime = 35;
        private static bool HasFlashed;
        private static bool Flashing;

        const string Sound = "al_new_toast.wav";

        static readonly Color BorderColor = Color.White;
        static readonly Color FlashingBackgroundColor = Color.White;
        static readonly Color StaticBackgroundColor = Color.Black;

        static Color BackgroundColor => Flashing ? FlashingBackgroundColor : StaticBackgroundColor;

        public static bool IsVisible;

        private static float _x;
        public static float X
        {
            get => ViewX + _x;
            set => _x = value;
        }
        private static float _y;
        public static float Y
        {
            get => _y + (int)ViewTop;
            set => _y = value;
        }
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static float Right => X + Width;
        public static float Bottom => Y + Height;

        private static bool IsSmall => ViewWidth < SmallWidth;

        private static float ViewX => Graphics.CurrentView.X;
        private static float ViewCenterY => Graphics.CurrentView.CenterY;
        private static float ViewTop => Graphics.CurrentView.Top;
        private static float ViewBottom => Graphics.CurrentView.Bottom;
        private static float ViewWidth => Graphics.CurrentView.Width;

        private static float ResX => Graphics.Renderer.ActiveResolution.X;
        private static float ResY => Graphics.Renderer.ActiveResolution.Y;

        private static int MaxWidth => IsSmall ? 480 : 640;
        private static GameTexture WhiteTexture = Graphics.Renderer.GetWhiteTexture();
        private static FloatRect SrcRect = new FloatRect(0, 0, 1, 1);

        private static GameFont Font => IsSmall ? Graphics.ToastFontSmall : Graphics.ToastFont;

        private static Padding TextPadding = new Padding(20, 8, 20, 8);
        private static Padding TextInnerPadding = new Padding(0, 4, 0, 0);
        private static int TextMaxWidth => MaxWidth - TextPadding.Left - TextPadding.Right;

        private static List<string> Lines { get; set; } = new List<string>();

        private static int EndingY => ContainerPadding;

        public static void Draw()
        {
            if (ToastService.CurrentToast.CreatedAt + DisplayTime < Timing.Global.Milliseconds)
            {
                IsVisible = false;
            }

            if (!IsVisible)
            {
                return;
            }

            Animate();

            DrawTextContainer();
            DrawText();
        }

        private static void Animate()
        {
            var toast = ToastService.CurrentToast;
            var now = Timing.Global.Milliseconds;

            if (now > toast.CreatedAt + SlideInTime)
            {
                Y = EndingY;

                if (Y == EndingY + ViewTop && !HasFlashed)
                {
                    FlashEndTime = Timing.Global.Milliseconds + FlashTime;
                    Flashing = true;
                    HasFlashed = true;
                    Audio.AddGameSound(Sound, false);
                }
                if (HasFlashed && FlashEndTime < now)
                {
                    Flashing = false;
                }

                return;
            }
            
            SlideIn(toast, now);
        }

        private static void SlideIn(Toast toast, long now)
        {
            var elapsed = now - toast.CreatedAt;

            var distanceRatio = (float)elapsed / SlideInTime;

            Y = EndingY * distanceRatio;
        }

        public static void ResetAnimation()
        {
            Y = 0;
            HasFlashed = false;
            FlashEndTime = 0L;
            Flashing = false;
        }

        public static void RecalculateContainer()
        {
            GetLines();

            var maxLine = 0;
            var idx = 0;
            Height = TextPadding.Top + TextPadding.Bottom;
            foreach (var line in Lines)
            {
                var textLen = Graphics.Renderer.MeasureText(line, Font, 1.0f);
                maxLine = Math.Max(maxLine, (int)textLen.X);

                // Don't top-pad the first line
                var padding = idx == 0 ? TextInnerPadding.Bottom : TextInnerPadding.Top + TextInnerPadding.Bottom;

                Height += (int)textLen.Y + padding;
                idx++;
            }

            Width = Math.Min(MaxWidth, maxLine + TextPadding.Left + TextPadding.Right);

            X = ResX / 2 - (Width / 2);
        }

        private static void DrawTextContainer()
        {
            DrawBackground();
            DrawForeground();
        }

        private static void DrawBackground()
        {
            var x = X - BorderWidth;
            var y = Y;
            var width = Width + (BorderWidth * 2);

            var horizontalRect = new FloatRect(x, y, width, Height);
            Graphics.DrawGameTexture(WhiteTexture, SrcRect, horizontalRect, BorderColor);

            x = X;
            y = Y - BorderWidth;
            var height = Height + (BorderWidth * 2);
            
            var verticalRect = new FloatRect(x, y, Width, height);
            Graphics.DrawGameTexture(WhiteTexture, SrcRect, verticalRect, BorderColor);
        }

        private static void DrawForeground()
        {
            var x = X;
            var y = Y;

            var destRect = new FloatRect(x, y, Width, Height);
            Graphics.DrawGameTexture(WhiteTexture, SrcRect, destRect, BackgroundColor);
        }

        private static void GetLines()
        {
            Lines.Clear();

            var toast = ToastService.CurrentToast;
            var message = toast.Message;

            // Word wrap
            var messageSplit = message.Split(' ');
            var sb = new StringBuilder();
            var lineNum = 0;
            var prevLine = 0;
            for (var idx = 0; idx < messageSplit.ToArray().Length; idx++)
            {
                var word = messageSplit.ToArray()[idx];
                // if we're either doing the first word, or the first word of a new line, don't append a space
                if (idx == 0 || lineNum != prevLine)
                {
                    sb.Append(word);
                }
                else
                {
                    sb.Append($" {word}");
                }

                prevLine = lineNum;

                if (idx == messageSplit.Length - 1)
                {
                    continue;
                }

                var nextWord = $"{sb} {messageSplit[idx + 1]}";
                var textLen = Graphics.Renderer.MeasureText(nextWord, Font, 1.0f);

                if (textLen.X > TextMaxWidth)
                {
                    Lines.Add(sb.ToString());
                    sb.Clear();

                    lineNum++;
                }
            }

            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                Lines.Add($"{sb}");
            }
        }

        private static void DrawText()
        {
            if (Flashing)
            {
                return;
            }

            var textX = X + TextPadding.Left;
            var textY = Y + TextPadding.Top;

            var toast = ToastService.CurrentToast;

            foreach(var line in Lines)
            {
                var textLen = Graphics.Renderer.MeasureText(line, Font, 1.0f);
                Graphics.Renderer.DrawString(
                    line, Font, textX, textY, 1.0f, toast.TextColor
                );
                textY += textLen.Y + TextInnerPadding.Top + TextInnerPadding.Bottom;
            }
        }
    }
}
