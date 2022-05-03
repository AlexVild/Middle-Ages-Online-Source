namespace Intersect.GameObjects.Timers
{

    /// <summary>
    /// Used to determine what kind of timer we are creating
    /// </summary>
    public enum TimerType
    {
        Scheduler,
        Stopwatch,
        Countdown
    }

    public enum TimerOwnerType : byte
    {
        Global,
        Player,
        Instance,
        Party,
        Guild
    }

    public enum TimerCompletionBehavior : byte
    {
        ExpirationThenCompletion,
        OnlyCompletion,
    }
}
