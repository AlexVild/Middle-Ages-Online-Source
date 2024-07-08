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

        public static string CooldownText(float remainingMs)
        {
            var secondsRemaining = (int)Math.Floor(remainingMs / 1000f);
            var minutesRemaining = (int)Math.Floor(secondsRemaining / 60f);
            var hoursRemaining = (int)Math.Floor(minutesRemaining / 60f);
            var daysRemaining = (int)Math.Floor(hoursRemaining / 24f);

            if (daysRemaining >= 1)
            {
                var hourRemainder = hoursRemaining - (daysRemaining * 24);
                return $"{hoursRemaining.ToString("N0")}d{hourRemainder.ToString("N0")}h";
            }

            if (hoursRemaining >= 1)
            {
                var minuteRemainder = minutesRemaining - (hoursRemaining * 60);
                return $"{hoursRemaining.ToString("N0")}h{minuteRemainder.ToString("N0")}m";
            }

            if (minutesRemaining >= 1)
            {
                var secondsRemainder = secondsRemaining - (minutesRemaining * 60);
                return $"{minutesRemaining.ToString("N0")}m";
            }

            if (secondsRemaining > 10f)
            {
                return $"{secondsRemaining.ToString("N0")}s";
            }

            return $"{(remainingMs / 1000f).ToString("N1")}s";
        }

        public static string GetArticle(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentException("Input word cannot be null or whitespace.", nameof(word));
            }

            char firstChar = word[0];
            string[] vowels = { "a", "e", "i", "o", "u" };

            // Special cases for words starting with a vowel sound
            if (firstChar == 'u' && word.Length > 2 && word[1] == 'n') // e.g., "unicorn"
            {
                return "a";
            }
            if (firstChar == 'h' && word.Length > 1 && (word[1] == 'o' || word[1] == 'i')) // e.g., "honor", "hour"
            {
                return "an";
            }

            // Default case
            return Array.Exists(vowels, v => v.Equals(firstChar.ToString(), StringComparison.OrdinalIgnoreCase)) ? "an" : "a";
        }
    }

}
