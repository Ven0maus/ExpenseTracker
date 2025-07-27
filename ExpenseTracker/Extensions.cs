using System.Globalization;

namespace ExpenseTracker
{
    internal static class Extensions
    {
        public static long ToUnixTimestamp(this DateTime dt)
        {
            return new DateTimeOffset(dt).ToUnixTimeSeconds();
        }

        public static DateTime FromUnixTimestamp(this long ts)
        {
            return DateTimeOffset.FromUnixTimeSeconds(ts).UtcDateTime;
        }

        // Euro
        public static string ToEuroFormat(this int value)
        {
            return $"€ {value}";
        }

        public static string ToEuroFormat(this long value)
        {
            return $"€ {value}";
        }

        public static string ToEuroFormat(this double value)
        {
            return $"€ {Math.Round(value, 2).ToString("0.##", CultureInfo.InvariantCulture)}";
        }

        public static string ToEuroFormat(this decimal value)
        {
            return $"€ {Math.Round(value, 2).ToString("0.##", CultureInfo.InvariantCulture)}";
        }

        // Percentage
        public static string ToPercentageFormat(this int value)
        {
            return $"{value} %";
        }

        public static string ToPercentageFormat(this long value)
        {
            return $"{value} %";
        }

        public static string ToPercentageFormat(this double value)
        {
            return $"{Math.Round(value, 2).ToString("0.##", CultureInfo.InvariantCulture)} %";
        }

        public static string ToPercentageFormat(this decimal value)
        {
            return $"{Math.Round(value, 2).ToString("0.##", CultureInfo.InvariantCulture)} %";
        }
    }
}
