using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Timers;
using Intersect.Server.Entities;
using Intersect.Server.General;
using Intersect.Server.Maps;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData
{
    public class TimerInstance
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public TimerInstance() { } // EF

        public TimerInstance(Guid descriptorId, Guid ownerId, long now, int completionCount)
        {
            Id = Guid.NewGuid();

            DescriptorId = descriptorId;
            OwnerId = ownerId;
            TimeRemaining = now + (Descriptor.TimeLimit * 1000); // TimeLimit is in seconds, multiply accordingly
            CompletionCount = completionCount;
        }

        [ForeignKey(nameof(Descriptor))]
        public Guid DescriptorId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public TimerDescriptor Descriptor
        {
            get => TimerDescriptor.Get(DescriptorId);
            set => DescriptorId = value?.Id ?? Guid.Empty;
        }

        // Default if owner is server (global timer)
        public Guid OwnerId { get; set; }

        /// <summary>
        /// A UTC Timestamp of when the timer will next expire
        /// </summary>
        public long TimeRemaining { get; set; }

        /// <summary>
        /// How many times this timer has completed an interval
        /// </summary>
        public int CompletionCount { get; set; }

        public bool IsExpired => Timing.Global.MillisecondsUtc > TimeRemaining;

        /// <summary>
        /// Helper for determining if this timer has expired
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public bool IsCompleted => Descriptor.Repetitions != TimerConstants.TimerIndefiniteRepeat && CompletionCount >= Descriptor.Repetitions + 1;

        /// <summary>
        /// Helper to calculate the timer's start time
        /// </summary>
        [NotMapped]
        [JsonIgnore] 
        public long StartTime => TimeRemaining - (Descriptor.TimeLimit * 1000 * (CompletionCount > 1 ? CompletionCount : 1));

        /// <summary>
        /// Helper to calculate how long this timer has been running
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public long ElapsedTime => Timing.Global.MillisecondsUtc - StartTime;

        /// <summary>
        /// Increments a timers <see cref="CompletionCount"/> and fires events for the necessary players based on this timers <see cref="OwnerId"/>
        /// </summary>
        public void ExpireTimer(long now)
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                CompletionCount++;

                foreach (var player in GetAffectedPlayers())
                {
                    FireExpireEvent(player);
                }

                var descriptor = Descriptor;
                if (!IsCompleted)
                {
                    TimeRemaining = now + (descriptor.TimeLimit * 1000); // Extend timer for next repetition
                }

                context.Timers.Update(this);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Runs the cancellation event for all relevant players
        /// </summary>
        public void CancelTimer()
        {
            foreach (var player in GetAffectedPlayers())
            {
                player.StartCommonEvent(Descriptor.CancellationEvent);
            }
        }

        /// <summary>
        /// Gets a list of players that should be affected by this timers completion event
        /// </summary>
        /// <returns>A list of <see cref="Player"/>s to be affected by timer expiration, based on the <see cref="TimerDescriptor.OwnerType"/></returns>
        public List<Player> GetAffectedPlayers()
        {
            var affectedPlayers = new List<Player>();
            var onlinePlayers = Globals.OnlineList;
            switch (Descriptor.OwnerType)
            {
                case TimerOwnerType.Global:
                    affectedPlayers.AddRange(onlinePlayers);
                    break;

                case TimerOwnerType.Player:
                    affectedPlayers.Add(onlinePlayers.Find(ply => ply.Id == OwnerId));
                    break;

                case TimerOwnerType.Instance:
                    affectedPlayers.AddRange(onlinePlayers.FindAll(ply => ply.MapInstanceId == OwnerId));
                    break;

                case TimerOwnerType.Party:
                    affectedPlayers.AddRange(onlinePlayers.FindAll(ply => ply.Party != null && ply.Party.Count >= 1 && ply.Party[0].Id == OwnerId));
                    break;

                case TimerOwnerType.Guild:
                    affectedPlayers.AddRange(onlinePlayers.FindAll(ply => ply.Guild != null && ply.Guild.Id == OwnerId));
                    break;

                default:
                    throw new NotImplementedException($"{Enum.GetName(typeof(TimerOwnerType), Descriptor.OwnerType)} not implemented!");
            }

            return affectedPlayers;
        }

        /// <summary>
        /// Fires the appropriate timer event based on the timer's state
        /// </summary>
        /// <param name="player">The player that will experience the event.</param>
        private void FireExpireEvent(Player player)
        {
            if (player == default)
            {
                return;
            }

            int reps = Descriptor.Repetitions;

            // If this timer is set to repeat and we're not done repeating yet, run expire event
            if (reps >= 0 && CompletionCount != reps + 1)
            {
                player.StartCommonEvent(EventBase.Get(Descriptor.ExpirationEventId));
                return;
            }
            
            // Otherwise, the timer is "complete". Fire the necessary events
            if (Descriptor.CompletionBehavior == TimerCompletionBehavior.ExpirationThenCompletion)
            {
                player.StartCommonEvent(Descriptor.ExpirationEvent);
            }
            player.StartCommonEvent(Descriptor.CompletionEvent);
        }

        /// <summary>
        /// Stores the timer's total elapsed time in a variable
        /// </summary>
        public void StoreElapsedTime()
        {
            // Log the elapsed time in a variable, if necessary
            var descriptor = Descriptor;
            if (descriptor.ElapsedTimeVariableId != default)
            {
                switch (descriptor.OwnerType)
                {
                    case TimerOwnerType.Global:
                        var globalVar = ServerVariableBase.Get(descriptor.ElapsedTimeVariableId);
                        if (globalVar != default)
                        {
                            globalVar.Value.Integer = ElapsedTime;
                        }
                        break;
                    case TimerOwnerType.Player:
                    case TimerOwnerType.Party:
                        foreach (var player in GetAffectedPlayers())
                        {
                            player.SetVariableValue(descriptor.ElapsedTimeVariableId, ElapsedTime);
                        }
                        break;
                    case TimerOwnerType.Instance:
                        MapInstance.SetInstanceVariable(descriptor.ElapsedTimeVariableId, ElapsedTime, OwnerId);
                        break;
                }
            }
        }
    }
}
