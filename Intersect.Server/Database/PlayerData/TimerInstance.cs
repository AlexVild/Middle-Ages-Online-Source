using Intersect.GameObjects.Events;
using Intersect.GameObjects.Timers;
using Intersect.Server.Entities;
using Intersect.Server.General;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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

        public TimerInstance(Guid descriptorId, Guid ownerId, long now, int completionCount = 0)
        {
            Id = Guid.NewGuid();

            DescriptorId = descriptorId;
            Descriptor = TimerDescriptor.Get(DescriptorId);
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

        [NotMapped]
        public TimerDescriptor Descriptor { get; set; }
        
        // Default if owner is server (global timer)
        public Guid OwnerId { get; set; }

        public long TimeRemaining { get; set; }

        public int CompletionCount { get; set; }

        public void ExpireTimer()
        {
            CompletionCount++;
            switch (Descriptor.OwnerType)
            {
                case TimerOwnerType.Player:
                    var player = Globals.OnlineList.Find(ply => ply.Id == OwnerId);
                    if (player == default)
                    {
                        return;
                    }

                    FireExpireEvent(player);
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
                player.StartCommonEvent(EventBase.Get(Descriptor.ExpirationEventId));
            }
            player.StartCommonEvent(EventBase.Get(Descriptor.CompletionEventId));
        }
    }
}
