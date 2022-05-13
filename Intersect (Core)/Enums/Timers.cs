namespace Intersect.GameObjects.Timers
{

    /// <summary>
    /// Used to determine what kind of timer we are creating
    /// </summary>
    public enum TimerType
    {
        /// <summary>
        /// A timer that executes an event every X seconds, counting up, and continues to run after completion
        /// </summary>
        Scheduler,

        /// <summary>
        /// A timer that executes an event every X seconds, counting up, but can only be completed via event or timer cleanup
        /// </summary>
        Stopwatch,

        /// <summary>
        /// A timer that executes an event every X seconds, counting down, and completes when it has finished its repetitions
        /// </summary>
        Countdown
    }

    /// <summary>
    /// Timers have owner types - which defines the scope in which to execute their associated events.
    /// </summary>
    public enum TimerOwnerType : byte
    {
        /// <summary>
        /// This timer will execute events to all online players
        /// </summary>
        Global,

        /// <summary>
        /// This timer will execute events to the player that started the timer
        /// </summary>
        Player,

        /// <summary>
        /// This timer will execute events to all online players with the same MapInstanceId
        /// </summary>
        Instance,

        /// <summary>
        /// This timer will execute events to all online players in the party that started the timer
        /// </summary>
        Party,

        /// <summary>
        /// This timer will execute events to all online players in a guild
        /// </summary>
        Guild
    }

    /// <summary>
    /// Timers can have different completion behaviors - the events that should fire when a timer is "complete", that is, has reached its time limit and has no more repetitions remaining.
    /// </summary>
    public enum TimerCompletionBehavior : byte
    {
        /// <summary>
        /// Fire the expiry event, then the completion event.
        /// </summary>
        ExpirationThenCompletion,

        /// <summary>
        /// Only fire the completion event - leave the expiry event for when non-final repetitions are reached only
        /// </summary>
        OnlyCompletion,
    }

    /// <summary>
    /// A timer can stop in a variety of fashions.
    /// </summary>
    public enum TimerStopType
    {
        /// <summary>
        /// Stop the timer, but do not execute any of its events. Only accessible via the "Stop Timer" event command.
        /// </summary>
        None,
        /// <summary>
        /// Stop the timer, and execute its "Canceled" event. Only accessible via the "Stop Timer" event command.
        /// </summary>
        Cancel,

        /// <summary>
        /// Stop the timer, and execute its "Expired" event. Can be accessed via "Stop Timer" event, when a timer finishes a repetition, or when a timer completes
        /// with the behavior <see cref="TimerCompletionBehavior.ExpirationThenCompletion"/>
        /// </summary>
        Expire,

        /// <summary>
        /// Stop the timer, and execute its "Canceled" event. Only accessible via the "Stop Timer" event command.
        /// </summary>
        Complete,
    }

    /// <summary>
    /// Operators for the "Modify Timer" event
    /// </summary>
    public enum TimerOperator
    {
        /// <summary>
        /// Used to set a timer's time limit/interval to a specific number of seconds
        /// </summary>
        Set,

        /// <summary>
        /// Used to add to a timer's time limit/interval a specific number of seconds
        /// </summary>
        Add,

        /// <summary>
        /// Used to subtract from a timer's time limit/interval a specific number of seconds
        /// </summary>
        Subtract
    }

    public static class TimerConstants
    {
        /// <summary>
        /// Used when a timer is set to not repeat
        /// </summary>
        public const int DoNotRepeat = -1;

        /// <summary>
        /// Used to be consistent about timer repetition values
        /// </summary>
        public const int TimerIndefiniteRepeat = int.MinValue;
    }
}
