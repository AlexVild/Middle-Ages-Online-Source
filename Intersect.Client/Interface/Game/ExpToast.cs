using Intersect.Client.Core;
using Intersect.Client.Framework.Graphics;
using Intersect.Utilities;
using System.Collections.Generic;

namespace Intersect.Client.Interface.Game
{
    public static class ExpToastService
    {
        public const int ToastDuration = 3000;

        public static Queue<ExpToast> Toasts { get; set; } = new Queue<ExpToast>();

        public static bool WeaponExpLastFlash { get; set; }

        public static bool ExpFlash { get; set; }

        public static void CreateExpToast(string exp, string extra = "", bool weaponExp = false)
        {
            Toasts.Enqueue(new ExpToast(exp, extra, weaponExp));
            WeaponExpLastFlash = weaponExp;
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
        readonly Color ExpTextColor = new Color(255, 242, 193, 223);
        readonly Color WeaponExpTextColor = new Color(255, 200, 145, 62);
        const long IdleDuration = 1000;
        const float Speed = 0.5f;
        const long DisappearingFramerate = 16;

        private Color Color => WeaponExpOnly ? 
            new Color(Alpha, WeaponExpTextColor.R, WeaponExpTextColor.G, WeaponExpTextColor.B) :
            new Color(Alpha, ExpTextColor.R, ExpTextColor.G, ExpTextColor.B);

        private Color OutlineColor => new Color(Alpha, 0, 0, 0);

        private long LastUpdate { get; set; }

        public int Alpha { get; set; } = 255;
        
        public long StartTime { get; set; }
        public long EndTime { get; set; }

        private float ViewX => Graphics.CurrentView.X;
        private float ViewTop => Graphics.CurrentView.Top;

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

        private bool WeaponExpOnly { get; set; }

        ExpToastStates State { get; set; }

        public ExpToast(string expStr, string extra, bool weaponExp)
        {
            ExpText = expStr;
            
            StartTime = Timing.Global.Milliseconds;
            EndTime = StartTime + ExpToastService.ToastDuration;
            State = ExpToastStates.Idle;

            // Spawn anywhere underneath the EXP bar
            var hud = Interface.GameUi.GetHud();
            var spawnX = Randomization.Next((int)hud.ExpX + 12, (int)hud.ExpX + (int)hud.ExpWidth - 12);

            X = spawnX;
            _y = hud.ExpY + 82 + Randomization.Next(-8, 9);

            WeaponExpOnly = weaponExp;
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

                        _y -= Speed;
                        Alpha = MathHelper.Clamp(Alpha - 10, 0, 255);
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
