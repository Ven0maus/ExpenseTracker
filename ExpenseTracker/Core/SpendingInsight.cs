namespace ExpenseTracker.Core
{
    internal class SpendingInsight
    {
        public string Message { get; set; }
        public InsightCategory Category { get; set; }
    }

    public enum InsightCategory
    {
        Comparative,
        Trend,
        Category,
        Behavioral
    }
}
