using Intersect.Client.Core;
using Intersect.Client.Framework.Graphics;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game
{
    public static class ExpToastService
    {
        public const int ToastDuration = 3000;

        public static Queue<ExpToast> Toasts { get; set; } = new Queue<ExpToast>();

        public static void CreateExpToast(long exp, string extra = "")
        {
            Toasts.Enqueue(new ExpToast(exp, extra));
        }

        public static void Draw()
        {
            var now = Timing.Global.Milliseconds;
            while (Toasts.Count > 0 && (Toasts.Peek().EndTime < now || Toasts.Peek().Alpha <= 0))
            {
                Toasts.Dequeue();
            }
            
            if (Toasts.Count == 0)
            {
                return;
            }

            foreach(var toast in Toasts)
            {
                toast.Update();
                toast.Draw();
            }
        }
    }

    enum ExpToastStates
    {
        Idle = 0,
        Disappearing,
    }

    public class ExpToast
    {
        readonly Color TextColor = new Color(255, 242, 193, 223);
        const long IdleDuration = 2000;
        const int Speed = -4;
        const long DisappearingFramerate = 16;

        private Color Color => new Color(Alpha, TextColor.R, TextColor.G, TextColor.B);
        private Color OutlineColor => new Color(Alpha, 0, 0, 0);

        private long LastUpdate { get; set; }

        public int Alpha { get; set; } = 255;
        
        public long StartTime { get; set; }
        public long EndTime { get; set; }

        private float ViewX => Graphics.CurrentView.X;
        private float ViewCenterY => Graphics.CurrentView.CenterY;
        private float ViewTop => Graphics.CurrentView.Top;
        private float ViewBottom => Graphics.CurrentView.Bottom;
        private float ViewWidth => Graphics.CurrentView.Width;

        private static GameFont Font => Graphics.ToastFont;

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

        public string ExpText { get; set; }

        ExpToastStates State { get; set; }

        public ExpToast(long exp, string extra)
        {
            if (!string.IsNullOrEmpty(extra))
            {
                ExpText = $"{extra} {exp} EXP";
            }
            else
            {
                ExpText = $"{exp} EXP";
            }
            
            StartTime = Timing.Global.Milliseconds;
            EndTime = StartTime + ExpToastService.ToastDuration;
            State = ExpToastStates.Idle;

            // Spawn anywhere underneath the EXP bar
            var hud = Interface.GameUi.GetHud();
            var spawnX = Randomization.Next((int)hud.ExpX + 12, (int)hud.ExpX + (int)hud.ExpWidth - 12);

            X = spawnX;
            _y = hud.ExpY + 72;
        }

        public void Update()
        {
            var now = Timing.Global.Milliseconds;
            switch (State)
            {
                case ExpToastStates.Idle:
                    LastUpdate = now;
                    if (now - StartTime >= IdleDuration)
                    {
                        State = ExpToastStates.Disappearing;
                    }
                    break;
                case ExpToastStates.Disappearing:
                    if (now - LastUpdate >= DisappearingFramerate)
                    {
                        LastUpdate = now;

                        _y -= 1;
                        Alpha = MathHelper.Clamp(Alpha - 20, 0, 255);
                    }
                    break;
            }
        }

        public void Draw()
        {
            Graphics.Renderer.DrawString(ExpText, Font, X, Y, 1.0f, Color, borderColor: OutlineColor);
        }
    }
}
