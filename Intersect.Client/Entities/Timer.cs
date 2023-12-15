using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.GameObjects.Timers;
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
        public Guid DescriptorId;

        public TimerDescriptor Descriptor;

        public long Timestamp;

        public long StartTime;

        public string DisplayName;

        public bool ContinueAfterExpiration;

        public string Time;

        public long ElapsedTime = 0L;

        public bool IsHidden = false;

        private TimerState State = TimerState.Active;

        private TimerDisplayType DisplayType;

        public Timer(Guid descriptorId, long timestamp, long startTime, TimerDisplayType displayType, string displayName, bool continueAfterExpiration)
        {
            DescriptorId = descriptorId;
            Descriptor = TimerDescriptor.Get(DescriptorId);
            Timestamp = timestamp;
            StartTime = startTime;
            DisplayType = displayType;
            DisplayName = displayName;
            ContinueAfterExpiration = continueAfterExpiration;
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
            switch (DisplayType)
            {
                case TimerDisplayType.Ascending:
                    ElapsedTime = Timing.Global.MillisecondsUtc - StartTime;
                    if (!ContinueAfterExpiration)
                    {
                        // If the timer is not set to continue after expiration, never display a time longer than the configured time for the timer
                        ElapsedTime = MathHelper.Clamp(ElapsedTime, 0, Timestamp - StartTime);
                    }
                    break;
                case TimerDisplayType.Descending:
                    ElapsedTime = Timestamp - Timing.Global.MillisecondsUtc;
                    if (!ContinueAfterExpiration)
                    {
                        // If the timer is not set to continue after expiration, never display a negative time
                        ElapsedTime = MathHelper.Clamp(ElapsedTime, 0, long.MaxValue);
                    }
                    break;
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
