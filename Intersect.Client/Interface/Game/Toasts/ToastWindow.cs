using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.General;
using Intersect.Client.Utilities;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intersect.Client.Interface.Game.Toasts
{
    public class ToastWindow
    {
        const float SmallWidth = 1280;
        const float BorderWidth = 4;
        const int ContainerPadding = 116;
        const int SlideInTime = 150;
        const long DisplayTime = 8000;

        private long FlashEndTime;
        const long FlashTime = 35;
        private bool HasFlashed;
        private bool Flashing;

        const string Sound = "al_new_toast.wav";

        readonly Color BorderColor = Color.White;
        readonly Color FlashingBackgroundColor = Color.White;
        readonly Color StaticBackgroundColor = Color.Black;
        readonly Color HoverBackgroundColor = new Color(82, 82, 82);

        private bool _mouseOver;
        private bool _mouseDown;

        Color BackgroundColor => Flashing ? FlashingBackgroundColor : 
            _mouseDown ? FlashingBackgroundColor :
            _mouseOver ? HoverBackgroundColor : 
            StaticBackgroundColor;

        private float _x;
        public float X
        {
            get => ViewX + _x;
            set => _x = value;
        }
        private float _y;
        public float Y
        {
            get => _y + (int)ViewTop;
            set => _y = value;
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Right => X + Width;
        public float Bottom => Y + Height;

        private bool IsSmall => ViewWidth < SmallWidth;

        private float ViewX => Graphics.CurrentView.X;
        private float ViewCenterY => Graphics.CurrentView.CenterY;
        private float ViewTop => Graphics.CurrentView.Top;
        private float ViewBottom => Graphics.CurrentView.Bottom;
        private float ViewWidth => Graphics.CurrentView.Width;

        private float ResX => Graphics.Renderer.ActiveResolution.X;
        private float ResY => Graphics.Renderer.ActiveResolution.Y;

        private int MaxWidth => IsSmall ? 480 : 640;
        private GameTexture WhiteTexture = Graphics.Renderer.GetWhiteTexture();
        private FloatRect SrcRect = new FloatRect(0, 0, 1, 1);

        private GameFont Font => IsSmall ? Graphics.ToastFontSmall : Graphics.ToastFont;

        private Padding TextPadding = new Padding(20, 8, 20, 8);
        private Padding TextInnerPadding = new Padding(0, 4, 0, 0);
        private int TextMaxWidth => MaxWidth - TextPadding.Left - TextPadding.Right;

        private List<string> Lines { get; set; } = new List<string>();

        private int EndingY => ContainerPadding;

        private long CreatedAt { get; set; }
        public bool Initialized = false;

        private Toast Toast { get; set; }

        public ToastWindow(Toast toast)
        {
            Toast = toast;
        }

        public void Initialize()
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

            CreatedAt = Timing.Global.MillisecondsUtcUnsynced;

            Initialized = true;
        }

        public void Draw()
        {
            if (!Initialized)
            {
                Initialize();
            }

            var mousePos = UiHelper.GetViewMousePos();
            _mouseOver = mousePos.X >= X && mousePos.X <= X + Width && mousePos.Y >= Y && mousePos.Y <= Y + Height;

            if (CreatedAt + DisplayTime < Timing.Global.MillisecondsUtcUnsynced)
            {
                _ = ToastService.TryDequeueToast();
                return;
            }

            Animate();

            DrawTextContainer();
            DrawText();

            // Click-to-close
            if (_mouseDown && !Globals.InputManager.MouseButtonDown(Framework.Input.GameInput.MouseButtons.Left))
            {
                ToastClicked();
                return;
            }
            if (_mouseOver && Globals.InputManager.MouseButtonDown(Framework.Input.GameInput.MouseButtons.Left))
            {
                _mouseDown = true;
            }
            if (_mouseDown && !_mouseOver)
            {
                _mouseDown = false;
            }

            ToastService.HasMouse = _mouseOver;
        }

        private void ToastClicked()
        {
            _mouseDown = false;
            Audio.AddGameSound("ui_close.wav", false);
            _ = ToastService.TryDequeueToast();
        }

        private void Animate()
        {
            var now = Timing.Global.MillisecondsUtcUnsynced;

            if (now > CreatedAt + SlideInTime)
            {
                Y = EndingY;

                if (Y == EndingY + ViewTop && !HasFlashed)
                {
                    FlashEndTime = Timing.Global.MillisecondsUtcUnsynced + FlashTime;
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
            
            SlideIn(Toast, now);
        }
        
        private void SlideIn(Toast toast, long now)
        {
            var elapsed = now - CreatedAt;

            var distanceRatio = (float)elapsed / SlideInTime;

            Y = EndingY * distanceRatio;
        }

        private void DrawTextContainer()
        {
            DrawBackground();
            DrawForeground();
        }

        private void DrawBackground()
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

        private void DrawForeground()
        {
            var x = X;
            var y = Y;

            var destRect = new FloatRect(x, y, Width, Height);
            Graphics.DrawGameTexture(WhiteTexture, SrcRect, destRect, BackgroundColor);
        }

        private void GetLines()
        {
            Lines.Clear();

            var message = Toast.Message;

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

        private void DrawText()
        {
            if (Flashing || _mouseDown)
            {
                return;
            }

            var textX = X + TextPadding.Left;
            var textY = Y + TextPadding.Top;

            foreach(var line in Lines)
            {
                var textLen = Graphics.Renderer.MeasureText(line, Font, 1.0f);
                Graphics.Renderer.DrawString(
                    line, Font, textX, textY, 1.0f, Toast.TextColor
                );
                textY += textLen.Y + TextInnerPadding.Top + TextInnerPadding.Bottom;
            }
        }
    }
}
