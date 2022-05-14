using Intersect.GameObjects.Events;
using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        /// Whether or not this timer continues after expiration - relying on a player to run its "Complete" or "Cancel" event via the "Stop Timer" command
        /// </summary>
        public bool ContinueAfterExpiration { get; set; }

        /// <summary>
        /// What kind of <see cref="TimerLogoutBehavior"/> to perform. Player timers only
        /// </summary>
        public TimerLogoutBehavior LogoutBehavior { get; set; }

        /// <summary>
        /// Whether or not to fire an epiry event at completion time
        /// </summary>
        public TimerCompletionBehavior CompletionBehavior { get; set; }

        /// <summary>
        /// Stores the timers elapsed time at timer destruction in some variable with this ID.
        /// The variable type is determined by <see cref="OwnerType"/>:
        /// - <see cref="TimerOwnerType.Global"/> - <see cref="ServerVariableBase"/>
        /// - <see cref="TimerOwnerType.Player"/> - <see cref="PlayerVariableBase"/>
        /// - <see cref="TimerOwnerType.Party"/> - <see cref="PlayerVariableBase"/>
        /// - <see cref="TimerOwnerType.Instance"/> - <see cref="InstanceVariableBase"/>
        /// </summary>
        public Guid ElapsedTimeVariableId { get; set; }

        /// <summary>
        /// How long a timer should run before execution
        /// </summary>
        public long TimeLimit { get; set; } = 1L;

        /// <summary>
        /// A <see cref="EventBase"/> to run if a timer is stopped via some event
        /// </summary>
        [ForeignKey(nameof(CancellationEvent))]
        public Guid CancellationEventId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public EventBase CancellationEvent
        {
            get => EventBase.Get(CancellationEventId);
            set => CancellationEventId = value?.Id ?? Guid.Empty;
        }

        /// <summary>
        /// A <see cref="EventBase"/> to run each time a timer hits its expiry time
        /// </summary>
        [ForeignKey(nameof(ExpirationEvent))]
        public Guid ExpirationEventId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public EventBase ExpirationEvent
        {
            get => EventBase.Get(ExpirationEventId);
            set => ExpirationEventId = value?.Id ?? Guid.Empty;
        }

        /// <summary>
        /// A <see cref="EventBase"/> to run each time a timer hits the end of its lifecycle
        /// </summary>
        [ForeignKey(nameof(CompletionEvent))]
        public Guid CompletionEventId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public EventBase CompletionEvent
        {
            get => EventBase.Get(CompletionEventId);
            set => CompletionEventId = value?.Id ?? Guid.Empty;
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";

        #region Access Helpers
        public static Guid IdFromList(int listIndex, TimerOwnerType ownerType)
        {
            if (listIndex < 0 || listIndex > Lookup.KeyList.Count)
            {
                return Guid.Empty;
            }

            var ids = Lookup.KeyList
                .OrderBy(pairs => Lookup[pairs]?.Name)
                .Where(pairs => ((TimerDescriptor)Lookup[pairs])?.OwnerType == ownerType)
                .ToArray();

            if (listIndex >= ids.Length)
            {
                return Guid.Empty;
            }

            return ids[listIndex];
        }

        public static int ListIndex(Guid id, TimerOwnerType ownerType)
        {
            return Lookup.KeyList
                .OrderBy(pairs => Lookup[pairs]?.Name)
                .Where(pairs => ((TimerDescriptor)Lookup[pairs])?.OwnerType == ownerType)
                .ToList().IndexOf(id);
        }
        #endregion
    }
}
