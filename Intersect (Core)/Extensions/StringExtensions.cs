using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Intersect.Extensions
{
    /// <summary>
    /// Extensions for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Dictionary<char, char> LeetMap = new Dictionary<char, char>()
        {
            ['4'] = 'a',
            ['@'] = 'a',
            ['8'] = 'b',
            ['('] = 'c',
            ['3'] = 'e',
            ['6'] = 'g',
            ['#'] = 'h',
            ['1'] = 'i',
            ['!'] = 'i',
            ['|'] = 'l',
            ['0'] = 'o',
            ['9'] = 'p',
            ['5'] = 's',
            ['$'] = 's',
            ['7'] = 't',
            ['+'] = 't',
            ['2'] = 'z'
        };

        private static readonly Regex SpacedOutPattern = new Regex(
            @"(?<!\S)(\S\s+){2,}\S(?!\S)",
            RegexOptions.Compiled
        );

        /// <summary>
        /// Formats the string in the current culture with the provided arguments.
        /// </summary>
        /// <param name="format">the format string</param>
        /// <param name="args">the format arguments</param>
        /// <returns>a formatted string</returns>
        /// <seealso cref="string.Format(IFormatProvider, string, object[])"/>
        public static string Format(this string format, params object[] args) =>
            Format(format, CultureInfo.CurrentCulture);

        /// <summary>
        /// Formats the string with the provided <see cref="IFormatProvider"/> and arguments.
        /// </summary>
        /// <param name="format">the format string</param>
        /// <param name="formatProvider">the format provider to use to format the provided string and arguments</param>
        /// <param name="args">the format arguments</param>
        /// <returns>a formatted string</returns>
        /// <seealso cref="string.Format(IFormatProvider, string, object[])"/>
        public static string Format(this string format, IFormatProvider formatProvider, params object[] args) =>
            string.Format(formatProvider, format, args);

        public static string TerminateWith(this string self, char terminateWith, bool condition = true) =>
            TerminateWith(self, new[] {terminateWith}, condition);

        public static string TerminateWith(this string self, char[] terminateWith, bool condition = true) =>
            TerminateWith(self, new string(terminateWith), condition);

        public static string TerminateWith(this string self, string terminateWith, bool condition = true)
        {
            if (!condition)
            {
                return self;
            }

            if (string.IsNullOrWhiteSpace(self))
            {
                return terminateWith;
            }

            if (self.EndsWith(terminateWith))
            {
                return self;
            }

            return self + terminateWith;
        }

        public static string NormalizeLeet(this string text)
        {
            var sb = new StringBuilder(text.Length);
            foreach (var c in text)
            {
                sb.Append(LeetMap.TryGetValue(c, out var normal) ? normal : c);
            }

            return sb.ToString();
        }

        public static string NormalizeSpacing(this string text)
        {
            return SpacedOutPattern.Replace(text, match =>
                Regex.Replace(match.Value, @"\s+", ""));
        }
    }
}
