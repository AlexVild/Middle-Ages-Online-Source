using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Toasts
{
    public class Toast
    {
        public string Message { get; set; }

        public readonly Color TextColor = Color.White;

        private long CreatedAt { get; set; }

        public Toast(string message)
        {
            Message = message;

            CreatedAt = Timing.Global.Milliseconds;
        }
    }
}
