using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Interface.Game.Toasts
{
    public static class ToastService
    {
        static Queue<Toast> ToastQueue = new Queue<Toast>();

        public static ToastWindow CurrentWindow => ToastQueue.FirstOrDefault()?.Window;

        public static bool HasMouse { get; set; }

        public static void SetToast(Toast toast)
        {
            if (toast == default)
            {
                return;
            }

            ToastQueue.Enqueue(toast);
        }

        public static void Draw()
        {
            CurrentWindow?.Draw();
        }

        public static bool TryDequeueToast()
        {
            if (ToastQueue.Count == 0)
            {
                return false;
            }

            ToastQueue.FirstOrDefault()?.Dispose();
            ToastQueue.Dequeue();
            HasMouse = false;
            return true;
        }

        public static void EmptyToastQueue()
        {
            ToastQueue.Clear();
            HasMouse = false;
        }
    }
}
