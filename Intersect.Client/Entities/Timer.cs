using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.GameObjects.Timers;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;
using System;

namespace Intersect.Client.Entities
{
    public enum TimerDisplayType
    {
        Ascending,
        Descending,
    }

    public enum TimerState
    {
        Active,
        Finishing
    }

    public class Timer
    {
        public Guid DescriptorId => Descriptor?.Id ?? Guid.Empty;

        public TimerDescriptor Descriptor { get; set; }

        public long TimeRemaining { get; set; }

        public long ServerTimeElapsed { get; set; }

        public long StartTime { get; set; }

        public string DisplayName { get; set; }

        public bool ContinueAfterExpiration { get; set; }

        public string Time { get; set; }

        public long ElapsedTime { get; set; }

        public bool IsHidden { get; set; }

        private TimerState State { get; set; } = TimerState.Active;

        private TimerDisplayType DisplayType { get; set; }

        public Timer(TimerDescriptor descriptor, TimerPacket packet, TimerDisplayType displayType)
        {
            Descriptor = descriptor;
            TimeRemaining = packet.TimeRemaining;
            ServerTimeElapsed = packet.ElapsedTime;
            StartTime = Timing.Global.MillisecondsUtcUnsynced;
            DisplayType = displayType;
            DisplayName = packet.DisplayName;
            ContinueAfterExpiration = packet.ContinueAfterExpiration;
        }

        public void Refresh(TimerPacket packet)
        {
            TimeRemaining = packet.TimeRemaining;
            ServerTimeElapsed = packet.ElapsedTime;
            StartTime = Timing.Global.MillisecondsUtcUnsynced;
        }

        public void Update()
        {
            switch(State)
            {
                case TimerState.Active:
                    StateActive();
                    break;
                case TimerState.Finishing:
                    StateFinishing();
                    break;
                default:
                    throw new NotImplementedException($"Client timer {DisplayName} set to invalid state: {State}");
            }
        }

        private void StateFinishing()
        {
            Timers.ActiveTimers.Remove(this);
        }

        private void StateActive()
        {
            var timePassed = (Timing.Global.MillisecondsUtcUnsynced - StartTime) + ServerTimeElapsed;
            if (DisplayType == TimerDisplayType.Ascending)
            {
                ElapsedTime = timePassed;
            }
            else
            {
                ElapsedTime = Descriptor.TimeLimitSeconds - timePassed;
            }

            if (!ContinueAfterExpiration)
            {
                // If the timer is ascending, never display a time longer than the configured time for the timer
                // If the timer is descending, never display a negative time
                var maxTime = DisplayType == TimerDisplayType.Ascending ? Descriptor.TimeLimitSeconds : long.MaxValue;
                ElapsedTime = MathHelper.Clamp(ElapsedTime, 0, maxTime);
            }

            Time = GenerateTimeDisplay(ElapsedTime);
        }

        public void EndTimer()
        {
            State = TimerState.Finishing;
        }

        private static string GenerateTimeDisplay(long elapsedTime)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(elapsedTime);

            switch(elapsedTime)
            {
                // We divide millis by 100 here because the more precisely we display a timer, the less accurate the display due to ping
                case long i when i < TimerConstants.HourMillis:
                    return string.Format(Strings.TimerWindow.ElapsedMinutes,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds / 100);
                case long i when i >= TimerConstants.HourMillis && i < TimerConstants.DayMillis:
                    return string.Format(Strings.TimerWindow.ElapsedHours,
                        t.Hours,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds / 100);
                case long i when i >= TimerConstants.DayMillis:
                    return string.Format(Strings.TimerWindow.ElapsedDays,
                        t.Days,
                        t.Hours,
                        t.Minutes);
                default:
                    return string.Empty;
            }
        }
    }
}
