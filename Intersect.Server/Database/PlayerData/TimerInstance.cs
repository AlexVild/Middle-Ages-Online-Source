using Intersect.GameObjects.Events;
using Intersect.GameObjects.Timers;
using Intersect.Server.Core;
using Intersect.Server.Entities;
using Intersect.Server.General;
using System;
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

            if (Descriptor.Type == TimerType.Stopwatch)
            {
                TimeRemaining = now;
            }
            else
            {
                TimeRemaining = now + (Descriptor.TimeLimit * 1000); // TimeLimit is in seconds, multiply accordingly
            }
            
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

        public long TimeRemaining { get; set; }

        public int CompletionCount { get; set; }

        public void ExpireTimer()
        {
            CompletionCount++;

            var onlinePlayers = Globals.OnlineList;
            switch (Descriptor.OwnerType)
            {
                case TimerOwnerType.Global:
                    foreach (var onlinePlayer in onlinePlayers)
                    {
                        FireExpireEvent(onlinePlayer);
                    }
                    break;
                case TimerOwnerType.Player:
                    var player = onlinePlayers.Find(ply => ply.Id == OwnerId);
                    if (player == default)
                    {
                        return;
                    }

                    FireExpireEvent(player);
                    break;
                case TimerOwnerType.Instance:
                    foreach (var instanceMember in onlinePlayers.FindAll(ply => ply.MapInstanceId == OwnerId))
                    {
                        FireExpireEvent(instanceMember);
                    }
                    break;
                case TimerOwnerType.Party:
                    foreach (var partyMember in onlinePlayers.FindAll(ply => ply.Party != null && ply.Party.Count >= 1 && ply.Party[0].Id == OwnerId))
                    {
                        FireExpireEvent(partyMember);
                    }
                    break;
                case TimerOwnerType.Guild:
                    foreach (var guildMember in onlinePlayers.FindAll(ply => ply.Guild != null && ply.Guild.Id == OwnerId))
                    {
                        FireExpireEvent(guildMember);
                    }
                    break;
                default:
                    throw new NotImplementedException($"{Enum.GetName(typeof(TimerOwnerType), Descriptor.OwnerType)} expiry not implemented!");
            }
        }

        private void FireExpireEvent(Player player)
        {
            _ = player ?? throw new ArgumentNullException(nameof(player));

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
    }
}
