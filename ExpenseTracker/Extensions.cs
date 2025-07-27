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

        public static (DateTime start, DateTime end) GetWeekRange(this DateTime referenceDate)
        {
            int day = (int)referenceDate.DayOfWeek;
            int offset = (day == 0) ? 6 : day - 1;
            var start = referenceDate.AddDays(-offset).Date;
            var end = start.AddDays(6);
            return (start, end);
        }

        public static (DateTime start, DateTime end) GetMonthRange(this DateTime referenceDate)
        {
            var start = new DateTime(referenceDate.Year, referenceDate.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            return (start, end);
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
