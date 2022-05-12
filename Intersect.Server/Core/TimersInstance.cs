using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Core
{
    public class TimerComparer : IComparer<TimerInstance>
    {
        public int Compare(TimerInstance timerA, TimerInstance timerB)
        {
            return timerA.TimeRemaining.CompareTo(timerB.TimeRemaining);
        }
    }

    public static class TimersInstance
    {
        /// <summary>
        /// A list of all active timers of all types
        /// </summary>
        public static SortedSet<TimerInstance> Timers;

        public static void ProcessTimers(long now)
        {
            foreach (var timer in Timers.Where((t => t.Descriptor.Type != GameObjects.Timers.TimerType.Stopwatch)).ToArray())
            {
                // Short-circuit out if the newest timer is not yet expired
                if (timer.TimeRemaining > now)
                {
                    return;
                }

                timer.ExpireTimer();

                // If the timer has completed its required amount of repetitions, remove the timer from processing
                if (timer.CompletionCount >= timer.Descriptor.Repetitions + 1)
                {
                    Timers.Remove(timer);
                    
                }
            }
        }
    }
}
