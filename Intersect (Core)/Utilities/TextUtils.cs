using Intersect.GameObjects.Timers;
using System;

namespace Intersect.Utilities
{

    public static partial class TextUtils
    {

        static TextUtils()
        {
            None = "None";
        }

        public static string None { get; set; }

        public static string StripToLower(string source)
        {
            return source?.ToLowerInvariant().Replace(" ", "");
        }

        public static bool IsNone(string str)
        {
            if (string.IsNullOrEmpty(str?.Trim()))
            {
                return true;
            }

            return string.Equals("None", StripToLower(str), StringComparison.InvariantCultureIgnoreCase) ||
                   string.Equals(None, StripToLower(str), StringComparison.InvariantCultureIgnoreCase);
        }

        public static string NullToNone(string nullableString)
        {
            return IsNone(nullableString) ? None : nullableString;
        }

        public static string SanitizeNone(string nullableString)
        {
            return IsNone(nullableString) ? null : nullableString;
        }

    }

    public static partial class TextUtils
    {
        public static string GetTimeElapsedString(long timeMs, string minutesString, string hoursString, string daysString)
        {
            string elapsedString = string.Empty;
            if (timeMs < 0)
            {
                return elapsedString;
            }

            TimeSpan t = TimeSpan.FromMilliseconds(timeMs);
            switch ((int)timeMs)
            {
                case int n when n < TimerConstants.HourMillis:
                    elapsedString = string.Format(minutesString,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);
                    break;
                case int n when n >= TimerConstants.HourMillis && n < TimerConstants.DayMillis:
                    elapsedString = string.Format(hoursString,
                        t.Hours,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);
                    break;
                case int n when n >= TimerConstants.DayMillis:
                    elapsedString = string.Format(daysString,
                        t.Days,
                        t.Hours,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);
                    break;
            }

            return elapsedString;
        }
    }

}
