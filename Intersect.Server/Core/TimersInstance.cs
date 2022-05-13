using Intersect.GameObjects.Timers;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Entities;
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
            // Stop watch timers don't expire - don't process them.
            foreach (var timer in Timers.Where((t => t.Descriptor.Type != TimerType.Stopwatch)).ToArray())
            {
                // Short-circuit out if the newest timer is not yet expired
                if (timer.TimeRemaining > now)
                {
                    return;
                }

                timer.ExpireTimer();

                // If the timer has completed its required amount of repetitions, remove the timer from processing
                if (timer.Descriptor.Repetitions != int.MinValue && timer.CompletionCount >= timer.Descriptor.Repetitions + 1)
                {
                    RemoveTimer(timer);
                }
                else
                {
                    timer.TimeRemaining = now + (timer.Descriptor.TimeLimit * 1000); // Extend timer for next repetition
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

        public static bool TimerIsActive(Guid descriptorId, Guid ownerId)
        {
            return Timers.ToList().Find(t => t.DescriptorId == descriptorId && t.OwnerId == ownerId) != default;
        }

        public static bool TryGetOwnerId(TimerOwnerType ownerType, Guid descriptorId, Player player, out Guid ownerId)
        {
            ownerId = default;

            switch (ownerType)
            {
                case TimerOwnerType.Global:
                    ownerId = default;
                    
                    break;
                case TimerOwnerType.Player:
                    ownerId = player.Id;

                    break;
                case TimerOwnerType.Instance:
                    ownerId = player.MapInstanceId;

                    break;
                case TimerOwnerType.Party:
                    if (player.Party == null || player.Party.Count < 1)
                    {
                        return false; // This timer requires the player to be in a party
                    }
                    ownerId = player.Party[0].Id; // party leader

                    break;
                case TimerOwnerType.Guild:
                    if (player.Guild == null)
                    {
                        return false; // This timer requires the player to be in a guild
                    }

                    ownerId = player.Guild.Id;
                    break;
                default:
                    throw new NotImplementedException("This timer owner type can not be processed!");
            }

            return true;
        }

        public static bool TryGetActiveTimer(Guid descriptorId, Guid ownerId, out TimerInstance activeTimer)
        {
            activeTimer = Timers.ToList().Find(t => t.DescriptorId == descriptorId && t.OwnerId == ownerId);

            return activeTimer != default;
        }
    }
}
