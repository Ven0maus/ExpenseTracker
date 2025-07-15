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
    }
}
