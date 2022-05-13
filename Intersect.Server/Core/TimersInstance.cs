using Intersect.GameObjects.Timers;
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
            foreach (var timer in Timers.Where((t => TimerDescriptor.Get(t.DescriptorId).Type != TimerType.Stopwatch)).ToArray())
            {
                var descriptor = TimerDescriptor.Get(timer.DescriptorId);
                // Short-circuit out if the newest timer is not yet expired
                if (timer.TimeRemaining > now)
                {
                    return;
                }

                timer.ExpireTimer();

                // If the timer has completed its required amount of repetitions, remove the timer from processing
                if (timer.CompletionCount >= descriptor.Repetitions + 1)
                {
                    RemoveTimer(timer);
                }
            }
        }

        public static void AddTimer(Guid descriptorId, Guid ownerId, long now, int completionCount = 0)
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                var timer = new TimerInstance(descriptorId, ownerId, now, completionCount);
                Timers.Add(timer);

                context.Timers.Add(timer);
                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }

        public static void RemoveTimer(TimerInstance timer)
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                Timers.Remove(timer);
                context.Timers.Remove(timer);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }
    }
}
