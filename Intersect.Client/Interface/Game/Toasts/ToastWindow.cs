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
        static readonly Color BorderColor = Color.White;
        static readonly Color BackgroundColor = Color.Black;

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
        private static float ViewWidth => Graphics.CurrentView.Width;

        private static float ResX => Graphics.Renderer.ActiveResolution.X;
        private static float ResY => Graphics.Renderer.ActiveResolution.Y;

        private static int MaxWidth => IsSmall ? 220 : 480;
        private static GameTexture WhiteTexture = Graphics.Renderer.GetWhiteTexture();
        private static FloatRect SrcRect = new FloatRect(0, 0, 1, 1);

        private static GameFont Font => IsSmall ? Graphics.ToastFontSmall : Graphics.ToastFont;

        private static Padding TextPadding = new Padding(20, 8, 20, 8);
        private static Padding TextInnerPadding = new Padding(0, 4, 0, 0);
        private static int TextMaxWidth => MaxWidth - TextPadding.Left - TextPadding.Right;

        public static void Draw()
        {
            if (!IsVisible)
            {
                return;
            }

            X = ResX / 2;
            Y = ResY / 2;

            DrawTextContainer();
            DrawText();
        }

        public static void RecalculateContainer()
        {
            var toast = ToastService.CurrentToast;

            var textLen = Graphics.Renderer.MeasureText(toast.Message, Font, 1.0f);
            Width = Math.Min(MaxWidth, (int)textLen.X + TextPadding.Left + TextPadding.Right);
            Height += (int)textLen.Y + TextPadding.Top + TextPadding.Bottom;

            while (textLen.X > TextMaxWidth)
            {
                Height += (int)textLen.Y + TextInnerPadding.Top + TextInnerPadding.Bottom;
                textLen.X -= TextMaxWidth;
            }
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

        private static void DrawText()
        {
            var textX = X + TextPadding.Left;
            var textY = Y + TextPadding.Top;

            var toast = ToastService.CurrentToast;
            var message = toast.Message;

            // Word wrap
            var messageSplit = message.Split(' ');
            var sb = new StringBuilder();
            var lineNum = 0;
            var prevLine = 0;
            for(var idx = 0; idx < messageSplit.ToArray().Length - 1; idx++)
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

                var nextWord = $"{sb} {messageSplit[idx + 1]}";
                var textLen = Graphics.Renderer.MeasureText(nextWord, Font, 1.0f);

                if (textLen.X > TextMaxWidth)
                {
                    Graphics.Renderer.DrawString(
                        sb.ToString(), Font, textX, textY, 1.0f, toast.TextColor
                    );
                    sb.Clear();

                    textY += textLen.Y + TextInnerPadding.Top + TextInnerPadding.Bottom;
                    lineNum++;
                }
            }

            // Add on the last word
            if (messageSplit.Length > 1)
            {
                sb.Append($" {messageSplit.LastOrDefault()}");
            }
            else // Otherwise this is a one word toast
            {
                sb.Append($"{messageSplit.LastOrDefault()}");
            }

            // Draw either the last line, or the text did not need wrapped, so draw without it
            Graphics.Renderer.DrawString(
                sb.ToString(), Font, textX, textY, 1.0f, toast.TextColor
            );
        }
    }
}
