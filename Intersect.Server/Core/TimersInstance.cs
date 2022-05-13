using Intersect.GameObjects.Timers;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Server.Core
{
    /// <summary>
    /// Used to maintain a <see cref="SortedSet{T}"/> of <see cref="TimerInstance"/>s
    /// </summary>
    public class TimerComparer : IComparer<TimerInstance>
    {
        /// <summary>
        /// Compares two timer's <see cref="TimerInstance.TimeRemaining"/>
        /// </summary>
        /// <param name="timerA">The first timer to compare</param>
        /// <param name="timerB">The timer to compare timerA to</param>
        /// <returns></returns>
        public int Compare(TimerInstance timerA, TimerInstance timerB)
        {
            if (timerA == default && timerB == default)
            {
                return 0;
            } else if (timerA == default)
            {
                return -1;
            } else if (timerB == default)
            {
                return 1;
            }

            return timerA.TimeRemaining.CompareTo(timerB.TimeRemaining);
        }
    }

    /// <summary>
    /// A publicly available hook into processing <see cref="TimerInstance"/>s. Contains helper functions as well as <see cref="Timers"/>, which is the <see cref="SortedSet{T}"/> of
    /// actively processing timers.
    /// </summary>
    public static class TimersInstance
    {
        /// <summary>
        /// A list of all active timers of all types
        /// </summary>
        public static SortedSet<TimerInstance> Timers;

        /// <summary>
        /// Processes the current list of timers, in a set sorted by expiry time.
        /// </summary>
        /// <param name="now">The UTC timestamp of processing time</param>
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

                // If the timer has completed its required amount of repetitions and is NOT set to indefinite repetition, remove the timer from processing
                if (timer.Descriptor.Repetitions != TimerConstants.TimerIndefiniteRepeat && timer.CompletionCount >= timer.Descriptor.Repetitions + 1)
                {
                    RemoveTimer(timer);
                }
                else
                {
                    timer.TimeRemaining = now + (timer.Descriptor.TimeLimit * 1000); // Extend timer for next repetition
                }
            }
        }

        /// <summary>
        /// Adds a timer to both the list of managed timers and the database
        /// </summary>
        /// <param name="descriptorId">The timer's descriptor ID</param>
        /// <param name="ownerId">The timer's owner ID - who this belongs to depends on the <see cref="TimerOwnerType"/> of the descriptor.</param>
        /// <param name="now">The current UTC Timestamp</param>
        /// <param name="completionCount">How many times this timer has been completed. By default, 0.</param>
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

        /// <summary>
        /// Removes a timer from our processing list and from the database.
        /// </summary>
        /// <param name="timer">The <see cref="TimerInstance"/> to remove</param>
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

        /// <summary>
        /// Returns whether or not some timer is actively being processed
        /// </summary>
        /// <param name="descriptorId">The descriptor ID of the <see cref="TimerInstance"/></param>
        /// <param name="ownerId">The ownerID of the <see cref="TimerInstance"/></param>
        /// <returns>True if the requested timer is currently being processed</returns>
        public static bool TimerIsActive(Guid descriptorId, Guid ownerId)
        {
            return Timers.ToList().Find(t => t.DescriptorId == descriptorId && t.OwnerId == ownerId) != default;
        }

        /// <summary>
        /// Returns true and populates a <see cref="Guid"/> if we can successfully obtain a Owner ID for some <see cref="TimerDescriptor.OwnerType"/>;
        /// </summary>
        /// <param name="ownerType">The owner type of the descriptor</param>
        /// <param name="descriptorId">The descriptor's ID</param>
        /// <param name="player">The player requesting timer information</param>
        /// <param name="ownerId">out, the <see cref="Guid"/> to populate if we successfully obtain an owner ID</param>
        /// <returns>True if an owner ID can be created, false otherwise</returns>
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

        /// <summary>
        /// Returns true and populates a <see cref="TimerInstance"/> if we can find a processing timer with the given parameters
        /// </summary>
        /// <param name="descriptorId">The descriptor ID of the timer</param>
        /// <param name="ownerId">The owner ID of the timer, see <see cref="TryGetOwnerId(TimerOwnerType, Guid, Player, out Guid)"/> for more information</param>
        /// <param name="activeTimer">out, the timer instance to populate if found.</param>
        /// <returns>True if we successfully found a timer and populated activeTimer</returns>
        public static bool TryGetActiveTimer(Guid descriptorId, Guid ownerId, out TimerInstance activeTimer)
        {
            activeTimer = Timers.ToList().Find(t => t.DescriptorId == descriptorId && t.OwnerId == ownerId);

            return activeTimer != default;
        }
    }
}
