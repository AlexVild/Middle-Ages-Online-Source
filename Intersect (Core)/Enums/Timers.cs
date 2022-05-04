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

    public enum TimerStopType
    {
        None,
        Cancel,
        Expire,
        Complete,
    }

    public enum TimerOperator
    {
        Set,
        Add,
        Subtract
    }
}
