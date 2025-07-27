using static ExpenseTracker.Core.CategorySummaryData;

namespace ExpenseTracker.Core
{
    internal class CalculationResults
    {
        public BudgetData BudgetData { get; set; }
        public PeriodData WeeklyData { get; set; }
        public PeriodData MonthlyData { get; set; }
        public PeriodData YearlyData { get; set; }
        public AveragesData AveragesData { get; set; }
        public CategorySummaryData CategorySummaryData { get; set; }
        public BiggestExpensesData BiggestExpensesData { get; set; }
        public SpendingInsight[] SpendingAnomaliesData { get; set; }
    }

    internal class BudgetData
    {
        public decimal MonthlyBudget { get; set; }
        public decimal BudgetLeft { get; set; }
        public decimal TotalSpend { get; set; }
    }

    internal class PeriodData
    {
        public decimal ThisPeriodTotal { get; set; }
        public decimal LastPeriodTotal { get; set; }
        public decimal Prediction { get; set; }
        public decimal PercentChange { get; set; }
    }

    internal class AveragesData
    {
        public decimal AvgDaily { get; set; }
        public decimal AvgWeekly { get; set; }
        public decimal AvgMonthly { get; set; }
        public decimal AvgYearly { get; set; }
    }

    internal class CategorySummaryData
    {
        public CategorySummaryItem[] TopCategories { get; set; }

        internal class CategorySummaryItem
        {
            public string Category { get; set; }
            public decimal Total { get; set; }
        }
    }

    internal class BiggestExpensesData
    {
        public BigExpenseItem[] TopExpenses { get; set; }

        internal class BigExpenseItem
        {
            public string Shop { get; set; }
            public decimal Total { get; set; }
        }
    }
}
