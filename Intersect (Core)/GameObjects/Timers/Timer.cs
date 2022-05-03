using Intersect.GameObjects.Events;
using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.GameObjects.Timers
{
    public abstract class Timer<TObject> : DatabaseObject<TObject> where TObject : Timer<TObject>
    {
        // EF
        public Timer()
        {
            Name = "New Timer";
        }

        [JsonConstructor]
        public Timer(Guid id) : base(id)
        {
            Name = "New Timer";
        }

        /// <summary>
        /// Whether or not the timer is visible to a player
        /// </summary>
        public bool Visible = false;

        /// <summary>
        /// The name displayed in the client, if the timer is visible
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// Whether the timer is ascending or descending by default
        /// </summary>
        public TimerTimingType TimingType = TimerTimingType.Ascending;

        /// <summary>
        /// The start time for a descending timer
        /// </summary>
        public long InitialTime = 0L;

        /// <summary>
        /// A <see cref="EventBase"/> to run if a descending timer reaches 0
        /// </summary>
        [Column("FailureEvent")]
        public Guid FailureEventId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public EventBase FailureEvent
        {
            get => EventBase.Get(FailureEventId);
            set => FailureEventId = value?.Id ?? Guid.Empty;
        }

        /// <summary>
        /// A <see cref="EventBase"/> to run if the timer is stopped via event.
        /// </summary>
        [Column("StopEvent")]
        public Guid StopEventId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public EventBase StopEvent
        {
            get => EventBase.Get(StopEventId);
            set => StopEventId = value?.Id ?? Guid.Empty;
        }
    }
}
