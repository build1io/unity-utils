using System;

namespace Build1.UnityUtils.Extensions
{
    public static class DateTimeExtensions
    {
        public enum SecondsFormat
        {
            Timer                  = 1,
            TimerWithHours         = 2,
            TimerLettered          = 3,
            OneValue               = 4,
            TimerWithMinifiedHours = 5
        }

        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            return (long)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static DateTime FromUnixTimestamp(this int timestamp)
        {
            return new DateTime(1970, 1, 1).AddSeconds(timestamp);
        }

        public static DateTime FromUnixTimestamp(this long timestamp)
        {
            return new DateTime(1970, 1, 1).AddSeconds(timestamp);
        }

        public static DateTime FromUnixTimestampMilliseconds(this ulong timestamp)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(timestamp);
        }

        public static string FormatSeconds(this int seconds, SecondsFormat format)  { return TimeSpan.FromSeconds(seconds).FormatSeconds(format); }
        public static string FormatSeconds(this long seconds, SecondsFormat format) { return TimeSpan.FromSeconds(seconds).FormatSeconds(format); }

        public static string FormatSeconds(this TimeSpan span, SecondsFormat format)
        {
            switch (format)
            {
                case SecondsFormat.Timer:
                    return span.Days > 0
                               ? $"{(int)span.TotalHours}:{span:mm\\:ss}"
                               : span.ToString(span.Hours > 0 ? "hh\\:mm\\:ss" : "mm\\:ss");

                case SecondsFormat.TimerWithHours:
                    return span.Days > 0
                               ? $"{(int)span.TotalHours}:{span:mm\\:ss}"
                               : span.ToString("hh\\:mm\\:ss");

                case SecondsFormat.TimerWithMinifiedHours:
                    return span.Days > 0
                               ? $"{(int)span.TotalHours}:{span:mm\\:ss}"
                               : span.ToString(span.Hours > 0 ? "h\\:mm\\:ss" : "mm\\:ss");

                case SecondsFormat.TimerLettered:
                    return $"{(int)span.TotalHours}h {span.Minutes}m {span:ss}s";

                case SecondsFormat.OneValue:
                    if (span.Days > 0)
                        return $"{span.Days}d";
                    if (span.Hours > 0)
                        return $"{span.Hours}h";
                    if (span.Minutes > 0)
                        return $"{span.Minutes}m";
                    return span.Seconds > 0
                               ? $"{span.Seconds}s"
                               : $"{span.Milliseconds}ms";

                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
        }
    }
}