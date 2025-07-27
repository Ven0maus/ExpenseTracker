using ExpenseTracker.UserControls;

namespace ExpenseTracker
{
    public partial class DashboardControl : UserControl, IUserControl
    {
        public DashboardControl()
        {
            InitializeComponent();
        }

        public void OnLoad()
        {
            // Make sure we return new data, not cached data.
            PurchaseDatabase.ClearCaches();

            CalculateBudget();
            CalculatePeriod(PeriodType.Weekly);
            CalculatePeriod(PeriodType.Monthly);
            CalculatePeriod(PeriodType.Yearly);
            CalculateAverages();
            CalculateCategorySummary();
            CalculateBiggestExpenses();
            CalculateSpendingAnomalies();
        }

        private void CalculateBudget()
        {
            NrTotalSpend.Text = PurchaseDatabase.GetAllTimeExpenses().ToEuroFormat();

            // TODO: Budgeting
            NrMonthlyBudget.Text = 0.ToEuroFormat();
            NrBudgetLeft.Text = 0.ToEuroFormat();
        }

        private void CalculatePeriod(PeriodType period)
        {
            var today = DateTime.Today;

            DateTime thisPeriodStart;
            DateTime thisPeriodEnd;

            DateTime lastPeriodStart;
            DateTime lastPeriodEnd;

            DateTime periodBeforeLastStart;
            DateTime periodBeforeLastEnd;

            switch (period)
            {
                case PeriodType.Weekly:
                    // Calculate current week Monday to Sunday
                    int dayOfWeek = (int)today.DayOfWeek;
                    int daysSinceMonday = (dayOfWeek == 0) ? 6 : dayOfWeek - 1;
                    thisPeriodStart = today.AddDays(-daysSinceMonday);
                    thisPeriodEnd = thisPeriodStart.AddDays(6);

                    lastPeriodStart = thisPeriodStart.AddDays(-7);
                    lastPeriodEnd = thisPeriodStart.AddDays(-1);

                    periodBeforeLastStart = lastPeriodStart.AddDays(-7);
                    periodBeforeLastEnd = lastPeriodStart.AddDays(-1);
                    break;

                case PeriodType.Monthly:
                    // Start of current month
                    thisPeriodStart = new DateTime(today.Year, today.Month, 1);
                    // End of current month
                    thisPeriodEnd = thisPeriodStart.AddMonths(1).AddDays(-1);

                    lastPeriodStart = thisPeriodStart.AddMonths(-1);
                    lastPeriodEnd = thisPeriodStart.AddDays(-1);

                    periodBeforeLastStart = lastPeriodStart.AddMonths(-1);
                    periodBeforeLastEnd = lastPeriodStart.AddDays(-1);
                    break;

                case PeriodType.Yearly:
                    // Start of current year
                    thisPeriodStart = new DateTime(today.Year, 1, 1);
                    // End of current year
                    thisPeriodEnd = new DateTime(today.Year, 12, 31);

                    lastPeriodStart = thisPeriodStart.AddYears(-1);
                    lastPeriodEnd = thisPeriodStart.AddDays(-1);

                    periodBeforeLastStart = lastPeriodStart.AddYears(-1);
                    periodBeforeLastEnd = lastPeriodStart.AddDays(-1);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(period), period, null);
            }

            // Fetch purchases for current period
            var purchasesThisPeriod = PurchaseDatabase.GetByDates(thisPeriodStart, thisPeriodEnd);
            var purchasesLastPeriod = PurchaseDatabase.GetByDates(lastPeriodStart, lastPeriodEnd);
            var purchasesPeriodBeforeLast = PurchaseDatabase.GetByDates(periodBeforeLastStart, periodBeforeLastEnd);

            var thisPeriodTotal = purchasesThisPeriod.Sum(a => a.Price);
            var lastPeriodTotal = purchasesLastPeriod.Sum(a => a.Price);
            var periodBeforeLastTotal = purchasesPeriodBeforeLast.Sum(a => a.Price);

            // Calculate historical average (last two periods)
            decimal historicalAvg = (lastPeriodTotal + periodBeforeLastTotal) / 2.0m;

            // Calculate days so far and daily average so far for weekly and monthly (yearly daily avg might be too coarse)
            int daysSoFar = 0;
            decimal dailyAvgSoFar = 0;

            switch (period)
            {
                case PeriodType.Weekly:
                    int dayOfWeekNow = (int)today.DayOfWeek;
                    daysSoFar = (dayOfWeekNow == 0) ? 7 : dayOfWeekNow; // days since Monday (counting today)
                    dailyAvgSoFar = daysSoFar > 0 ? thisPeriodTotal / daysSoFar : 0;
                    break;

                case PeriodType.Monthly:
                    daysSoFar = today.Day; // day of month
                    int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
                    dailyAvgSoFar = daysSoFar > 0 ? thisPeriodTotal / daysSoFar : 0;
                    break;

                case PeriodType.Yearly:
                    daysSoFar = today.DayOfYear;
                    int daysInYear = DateTime.IsLeapYear(today.Year) ? 366 : 365;
                    dailyAvgSoFar = daysSoFar > 0 ? thisPeriodTotal / daysSoFar : 0;
                    break;
            }

            decimal periodLengthInDays = period switch
            {
                PeriodType.Weekly => 7,
                PeriodType.Monthly => DateTime.DaysInMonth(today.Year, today.Month),
                PeriodType.Yearly => DateTime.IsLeapYear(today.Year) ? 366 : 365,
                _ => 7
            };

            decimal liveForecast = dailyAvgSoFar * periodLengthInDays;

            // Weighted prediction
            // Use percentage of period passed to adjust weighting
            decimal progressRatio = daysSoFar / periodLengthInDays;
            progressRatio = Math.Clamp(progressRatio, 0m, 1m); // Ensure between 0 and 1

            // Let live data get more weight as time passes
            decimal prediction = (historicalAvg * (1 - progressRatio)) + (liveForecast * progressRatio);
            prediction = Math.Max(prediction, thisPeriodTotal); // Cannot go below the current already spend period

            // Percent change from last period
            decimal percentChange = 0;
            if (lastPeriodTotal != 0)
                percentChange = ((thisPeriodTotal - lastPeriodTotal) / lastPeriodTotal) * 100m;
            else
                percentChange = (thisPeriodTotal == 0) ? 0 : 100;

            // Assign to UI controls based on period
            switch (period)
            {
                case PeriodType.Weekly:
                    NrThisWeekTotal.Text = thisPeriodTotal.ToEuroFormat();
                    NrLastWeeKTotal.Text = lastPeriodTotal.ToEuroFormat();
                    NrThisWeekPrediction.Text = Math.Round(prediction, 0).ToEuroFormat();
                    NrComparedToLastWeek.Text = Math.Round(percentChange, 1).ToPercentageFormat();
                    break;

                case PeriodType.Monthly:
                    NrThisMonthTotal.Text = thisPeriodTotal.ToEuroFormat();
                    NrLastMonthTotal.Text = lastPeriodTotal.ToEuroFormat();
                    NrThisMonthPrediction.Text = Math.Round(prediction, 0).ToEuroFormat();
                    NrComparedToLastMonth.Text = Math.Round(percentChange, 1).ToPercentageFormat();
                    break;

                case PeriodType.Yearly:
                    NrThisYearTotal.Text = thisPeriodTotal.ToEuroFormat();
                    NrLastYearTotal.Text = lastPeriodTotal.ToEuroFormat();
                    NrThisYearPrediction.Text = Math.Round(prediction, 0).ToEuroFormat();
                    NrComparedToLastYear.Text = Math.Round(percentChange, 1).ToPercentageFormat();
                    break;
            }
        }

        /* // ORIGINAL IMPLEMENTATION (Before generalize into CalculatePeriod)
        private void CalculateWeek()
        {
            // Today
            var today = DateTime.Today;

            // Get the current day of the week (0 = Sunday, 1 = Monday, ...)
            var dayOfWeek = (int)today.DayOfWeek;

            // Calculate how many days to subtract to get back to Monday
            int daysSinceMonday = (dayOfWeek == 0) ? 6 : dayOfWeek - 1;

            // Start of the week (Monday)
            var thisWeekStart = today.AddDays(-daysSinceMonday);

            // End of the week (Sunday)
            var thisWeekEnd = thisWeekStart.AddDays(6);

            // Now fetch purchases between Monday and Sunday
            var purchasesThisWeek = PurchaseDatabase.GetByDates(thisWeekStart, thisWeekEnd);

            var lastWeekStart = thisWeekStart.AddDays(-7);
            var lastWeekEnd = thisWeekStart.AddDays(-1);

            // Fetch purchases from last week
            var purchasesLastWeek = PurchaseDatabase.GetByDates(lastWeekStart, lastWeekEnd);

            var thisWeekTotal = purchasesThisWeek.Sum(a => a.Price);
            var lastWeekTotal = purchasesLastWeek.Sum(a => a.Price);

            NrThisWeekTotal.Text = thisWeekTotal.ToEuroFormat();
            NrLastWeeKTotal.Text = lastWeekTotal.ToEuroFormat();

            // Prediction based on daily spending and last two weeks average
            var weekBeforeLastWeekStart = lastWeekStart.AddDays(-7);
            var weekBeforeLastWeekEnd = lastWeekStart.AddDays(-1);
            var purchasesWeekBeforeLastWeek = PurchaseDatabase.GetByDates(lastWeekStart, lastWeekEnd);
            var weekBeforeLastWeekTotal = purchasesWeekBeforeLastWeek.Sum(a => a.Price);

            // --- 1. Historical average over last 2 weeks
            decimal historicalAvg = (lastWeekTotal + weekBeforeLastWeekTotal) / 2.0m;

            // --- 2. Current week's daily average (based on days so far)
            decimal currentTotal = thisWeekTotal;
            int daysSoFar = daysSinceMonday + 1;
            decimal dailyAvgSoFar = (daysSoFar > 0) ? currentTotal / daysSoFar : 0;
            decimal liveForecast = dailyAvgSoFar * 7; // Full week estimate

            // --- 3. Combine both (weighted)
            decimal prediction = (historicalAvg * 0.6m) + (liveForecast * 0.4m); // adjust weights if needed
            NrThisWeekPrediction.Text = Math.Round(prediction, 0).ToEuroFormat();

            // Percentage based on last week
            decimal percentChange = 0;
            if (lastWeekTotal != 0)
            {
                percentChange = ((thisWeekTotal - lastWeekTotal) / lastWeekTotal) * 100.0m;
            }
            else
            {
                // Optional: if lastWeekTotal was 0, avoid divide-by-zero
                percentChange = (thisWeekTotal == 0) ? 0 : 100;
            }

            NrComparedToLastWeek.Text = Math.Round(percentChange, 1).ToEuroFormat();
        }
        */

        private void CalculateAverages()
        {
            var today = DateTime.Today;

            // Loop through all period types you want averages for
            foreach (PeriodType period in Enum.GetValues<PeriodType>())
            {
                // Get date range for current period (e.g. this week, this month, etc.)
                var (start, end) = GetPeriodDateRange(today, period);

                // Get purchases for the current period
                var purchasesThisPeriod = PurchaseDatabase.GetByDates(start, end);

                // Sum and count
                var totalAmount = purchasesThisPeriod.Sum(p => p.Price);
                var count = purchasesThisPeriod.Length;

                // Calculate average purchase amount for the period
                decimal average = Math.Round(count > 0 ? totalAmount / count : 0, 2);

                // Now assign to UI or process further depending on period
                switch (period)
                {
                    case PeriodType.Daily:
                        NrAvgDailySpend.Text = average.ToEuroFormat();
                        break;

                    case PeriodType.Weekly:
                        NrAvgWeeklySpend.Text = average.ToEuroFormat();
                        break;

                    case PeriodType.Monthly:
                        NrAvgMonthlySpend.Text = average.ToEuroFormat();
                        break;

                    case PeriodType.Yearly:
                        NrAvgYearlySpend.Text = average.ToEuroFormat();
                        break;
                }
            }
        }

        private void CalculateCategorySummary()
        {
            var (start, end) = GetPeriodDateRange(DateTime.Now, PeriodType.Monthly);
            var purchases = PurchaseDatabase.GetByDates(start, end);

            // Group by category and sum totals
            var categories = purchases
                .GroupBy(p => p.Category, StringComparer.OrdinalIgnoreCase)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(p => p.Price)
                })
                .OrderByDescending(g => g.Total)
                .Take(5) // Top 5 categories
                .ToList();

            var expenseLabels = new Dictionary<int, Label>
            {
                {0, SpendTrend1 },
                {1, SpendTrend2 },
                {2, SpendTrend3 },
                {3, SpendTrend4 },
                {4, SpendTrend5 },
            };

            var expenseValues = new Dictionary<int, Label>
            {
                {0, SpendTrendValue1 },
                {1, SpendTrendValue2 },
                {2, SpendTrendValue3 },
                {3, SpendTrendValue4 },
                {4, SpendTrendValue5 },
            };

            for (int i = 0; i < categories.Count; i++)
            {
                var purchase = categories[i];
                var label = expenseLabels[i];
                var value = expenseValues[i];

                label.Text = $"{i + 1}. {purchase.Category}";
                value.Text = purchase.Total.ToEuroFormat();
            }

            // Clear out others
            if (categories.Count != 5)
            {
                var leftOver = categories.Count;
                for (int i = 4; i >= leftOver; i--)
                {
                    var label = expenseLabels[i];
                    var value = expenseValues[i];
                    label.Text = string.Empty;
                    value.Text = string.Empty;
                }
            }
        }

        private void CalculateBiggestExpenses()
        {
            var (start, end) = GetPeriodDateRange(DateTime.Now, PeriodType.Monthly);
            var purchases = PurchaseDatabase.GetByDates(start, end);

            // Group by shop and sum totals
            var biggestExpenses = purchases
                .GroupBy(p => p.Shop, StringComparer.OrdinalIgnoreCase)
                .Select(g => new
                {
                    Shop = g.Key,
                    Total = g.Sum(p => p.Price)
                })
                .OrderByDescending(g => g.Total)
                .Take(5) // Top 5 biggest expenses
                .ToList();

            var expenseLabels = new Dictionary<int, Label>
            {
                {0, BigExpense1 },
                {1, BigExpense2 },
                {2, BigExpense3 },
                {3, BigExpense4 },
                {4, BigExpense5 },
            };

            var expenseValues = new Dictionary<int, Label>
            {
                {0, BigExpenseValue1 },
                {1, BigExpenseValue2 },
                {2, BigExpenseValue3 },
                {3, BigExpenseValue4 },
                {4, BigExpenseValue5 },
            };

            for (int i = 0; i < biggestExpenses.Count; i++)
            {
                var purchase = biggestExpenses[i];
                var label = expenseLabels[i];
                var value = expenseValues[i];

                label.Text = $"{i + 1}. {purchase.Shop}";
                value.Text = purchase.Total.ToEuroFormat();
            }

            // Clear out others
            if (biggestExpenses.Count != 5)
            {
                var leftOver = biggestExpenses.Count;
                for (int i = 4; i >= leftOver; i--)
                {
                    var label = expenseLabels[i];
                    var value = expenseValues[i];
                    label.Text = string.Empty;
                    value.Text = string.Empty;
                }
            }
        }

        private void CalculateSpendingAnomalies()
        {
            /* TODO:
             *  Comparative Anomalies

            "You spent X% more this week than last week."

            "Your daily average this month is Y% above your usual daily average."

            "You've already spent Z% of your monthly budget, and the month is only X% complete."

            "This month’s spending is your highest since [Month/Year]."

            "You spent less this week than any week in the last 6 months."

            📈 Trend-Based Insights

            "Your spending has increased for 3 consecutive weeks."

            "You've spent more every Monday than any other weekday for the past 4 weeks."

            "There’s a 50% increase in your weekend spending compared to weekdays."

            "Average daily spending this month is trending 15% higher than the previous 3-month average."

            "This is the 3rd time in a row your monthly spending increased."

            🛒 Category-Specific Anomalies (if you use categories)

            "You spent 3× more on Dining & Coffee this week than your usual weekly average."

            "Groceries made up 60% of your spending this month — highest ever."

            "No purchases were made in the Entertainment category this month — unusual!"

            "Your most expensive category this month is [X], which is different from your usual [Y]."

            💡 Behavior-Based Facts

            "Your most expensive purchase this week was at [Shop], costing [X €]."

            "You made 80% of your purchases at 3 shops: [A], [B], [C]."

            "You spent something every day for the last 10 days — streak!"

            "You didn’t spend anything for 3 days straight last week — rare!"

            "Most of your purchases happen on [Day of Week] — pattern spotted."

            "Your average purchase size increased by 20% this month."
            */
        }

        private static (DateTime start, DateTime end) GetPeriodDateRange(DateTime referenceDate, PeriodType period)
        {
            switch (period)
            {
                case PeriodType.Daily:
                    return (referenceDate.Date, referenceDate.Date);

                case PeriodType.Weekly:
                    // Week starts on Monday
                    int dayOfWeek = (int)referenceDate.DayOfWeek;
                    int daysSinceMonday = (dayOfWeek == 0) ? 6 : dayOfWeek - 1;
                    var weekStart = referenceDate.AddDays(-daysSinceMonday).Date;
                    var weekEnd = weekStart.AddDays(6);
                    return (weekStart, weekEnd);

                case PeriodType.Monthly:
                    var monthStart = new DateTime(referenceDate.Year, referenceDate.Month, 1);
                    var monthEnd = monthStart.AddMonths(1).AddDays(-1);
                    return (monthStart, monthEnd);

                case PeriodType.Yearly:
                    var yearStart = new DateTime(referenceDate.Year, 1, 1);
                    var yearEnd = new DateTime(referenceDate.Year, 12, 31);
                    return (yearStart, yearEnd);

                default:
                    throw new ArgumentOutOfRangeException(nameof(period), period, null);
            }
        }

        public enum PeriodType
        {
            Daily,
            Weekly,
            Monthly,
            Yearly
        }
    }
}