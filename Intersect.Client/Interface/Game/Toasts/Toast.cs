using Intersect.Utilities;
using System;

namespace Intersect.Client.Interface.Game.Toasts
{
    public class Toast : IDisposable
    {
        public string Message { get; set; }

        public readonly Color TextColor = Color.White;

        public ToastWindow Window { get; set; }

        public Toast(string message)
        {
            Message = message;

            Window = new ToastWindow(this);
        }

        public void Dispose()
        {
            Window = null;
        }
    }
}
