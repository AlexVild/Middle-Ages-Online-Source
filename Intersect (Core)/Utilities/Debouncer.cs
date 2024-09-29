using System;
using System.Threading.Tasks;
using System.Threading;
using MathNet.Numerics.Optimization;

namespace Intersect.Utilities
{
    public class Debouncer
    {
        private CancellationTokenSource _cancellationTokenSource;
        private long _debounceExpiredAt;

        public void Debounce(Action action, int millisecondsDelay, bool pessimistic = false, bool alwaysDelay = false)
        {
            if (action == null)
            {
                return;
            }

            // Need to debounce
            if (Timing.Global.MillisecondsUtc < _debounceExpiredAt || alwaysDelay)
            {
                if (pessimistic)
                {
                    return;
                }

                _debounceExpiredAt = Timing.Global.MillisecondsUtc + millisecondsDelay;
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                var token = _cancellationTokenSource.Token;
                
                // Delay the execution of the action for the debounce period
                Task.Delay(millisecondsDelay, token).ContinueWith(t =>
                {
                    if (!t.IsCanceled)
                    {
                        action();
                    }
                }, TaskScheduler.Default);

                return;
            }
            
            _debounceExpiredAt = Timing.Global.MillisecondsUtc + millisecondsDelay;
            action();
        }
    }
}
