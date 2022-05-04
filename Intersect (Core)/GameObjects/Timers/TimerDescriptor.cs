using Intersect.GameObjects.Events;
using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.GameObjects.Timers
{
    public class TimerDescriptor : DatabaseObject<TimerDescriptor>, IFolderable
    {
        // EF
        public TimerDescriptor() : this(default)
        {
        }

        [JsonConstructor]
        public TimerDescriptor(Guid id) : base(id)
        {
            Name = "New Timer";
        }

        public TimerDescriptor(Guid id, TimerOwnerType ownerType) : base(id)
        {
            Name = "New Timer";
            OwnerType = ownerType;
        }

        /// <summary>
        /// The type required by this timer's owner
        /// </summary>
        public TimerOwnerType OwnerType { get; set; }

        /// <summary>
        /// Whether the timer is ascending or descending by default
        /// </summary>
        public TimerType Type { get; set; }

        /// <summary>
        /// The name displayed in the client, if the timer is visible
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Whether or not the timer is visible to a player
        /// </summary>
        public bool Hidden { get; set; } = false;

        /// <summary>
        /// How many times a repeating timer can repeat
        /// </summary>
        public int Repetitions { get; set; }

        /// <summary>
        /// Whether this timer continues after a user gets timed-out
        /// </summary>
        public bool ContinueOnTimeout { get; set; }

        /// <summary>
        /// Whether or not to fire an epiry event at completion time
        /// </summary>
        public TimerCompletionBehavior CompletionBehavior { get; set; }

        /// <summary>
        /// How long a timer should run before execution
        /// </summary>
        public long TimeLimit { get; set; } = 0L;

        /// <summary>
        /// A <see cref="EventBase"/> to run if a timer is stopped via some event
        /// </summary>
        [ForeignKey(nameof(CancellationEvent))]
        public Guid CancellationEventId { get; set; }

        [NotMapped]
        public EventBase CancellationEvent { get; set; }

        /// <summary>
        /// A <see cref="EventBase"/> to run each time a timer hits its expiry time
        /// </summary>
        [ForeignKey(nameof(ExpirationEvent))]
        public Guid ExpirationEventId { get; set; }

        [NotMapped]
        public EventBase ExpirationEvent { get; set; }

        /// <summary>
        /// A <see cref="EventBase"/> to run each time a timer hits the end of its lifecycle
        /// </summary>
        [ForeignKey(nameof(CompletionEvent))]
        public Guid CompletionEventId { get; set; }

        [NotMapped]
        public EventBase CompletionEvent { get; set; }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }
}
