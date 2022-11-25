using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Toasts
{
    public static class ToastService
    {
        public static Toast CurrentToast { get; set; }

        public static void SetToast(Toast toast)
        {
            CurrentToast = toast;
            ToastWindow.RecalculateContainer();
        }

        public static void Draw()
        {
            if (CurrentToast != null)
            {
                if (!ToastWindow.IsVisible)
                {
                    Show();
                }
                ToastWindow.Draw();
            }
            else if (ToastWindow.IsVisible)
            {
                Hide();
            }
        }

        public static void Show()
        {
            ToastWindow.IsVisible = true;
        }

        public static void Hide()
        {
            ToastWindow.IsVisible = false;
        }
    }
}
