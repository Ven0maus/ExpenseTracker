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

            var settingsControl = AppForm.Instance.GetInstance<SettingsControl>(Views.Settings);
            var monthlyBudget = settingsControl.Settings.MonthlyBudget;
            var (start, end) = DateTime.Now.GetMonthRange();
            var spendThisMonth = (decimal)PurchaseDatabase.GetExpensesByDates(start, end);
            var budgetLeft = (monthlyBudget - spendThisMonth);
            var twentyPercentOfMonthlyBudget = Math.Round(monthlyBudget * 0.2m, 2);

            NrMonthlyBudget.Text = monthlyBudget.ToEuroFormat();
            NrBudgetLeft.Text = budgetLeft.ToEuroFormat();

            // Define color of budget
            if (budgetLeft >= twentyPercentOfMonthlyBudget)
                NrBudgetLeft.ForeColor = Color.ForestGreen;
            else if (budgetLeft > 0)
                NrBudgetLeft.ForeColor = Color.Orange;
            else
                NrBudgetLeft.ForeColor = Color.Red;
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
                    if (percentChange <= 0)
                        NrComparedToLastWeek.ForeColor = Color.ForestGreen;
                    else
                        NrComparedToLastWeek.ForeColor = Color.Red;
                    break;

                case PeriodType.Monthly:
                    NrThisMonthTotal.Text = thisPeriodTotal.ToEuroFormat();
                    NrLastMonthTotal.Text = lastPeriodTotal.ToEuroFormat();
                    NrThisMonthPrediction.Text = Math.Round(prediction, 0).ToEuroFormat();
                    NrComparedToLastMonth.Text = Math.Round(percentChange, 1).ToPercentageFormat();
                    if (percentChange <= 0)
                        NrComparedToLastMonth.ForeColor = Color.ForestGreen;
                    else
                        NrComparedToLastMonth.ForeColor = Color.Red;
                    break;

                case PeriodType.Yearly:
                    NrThisYearTotal.Text = thisPeriodTotal.ToEuroFormat();
                    NrLastYearTotal.Text = lastPeriodTotal.ToEuroFormat();
                    NrThisYearPrediction.Text = Math.Round(prediction, 0).ToEuroFormat();
                    NrComparedToLastYear.Text = Math.Round(percentChange, 1).ToPercentageFormat();
                    if (percentChange <= 0)
                        NrComparedToLastYear.ForeColor = Color.ForestGreen;
                    else
                        NrComparedToLastYear.ForeColor = Color.Red;
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
            var earliestDate = PurchaseDatabase.GetEarliestPurchaseDate();

            // Define lookback limits
            var dailyLookback = today.AddYears(-1);
            var weeklyLookback = today.AddYears(-1);
            var monthlyLookback = today.AddYears(-1);
            var yearlyLookback = today.AddYears(-3);

            // Clamp actual start dates based on available data
            var dailyStart = earliestDate > dailyLookback ? earliestDate : dailyLookback;
            var weeklyStart = earliestDate > weeklyLookback ? earliestDate : weeklyLookback;
            var monthlyStart = earliestDate > monthlyLookback ? earliestDate : monthlyLookback;
            var yearlyStart = earliestDate > yearlyLookback ? earliestDate : yearlyLookback;

            // --- Daily average (include days with no purchases)
            var allDaily = PurchaseDatabase.GetByDates(dailyStart, today);
            var totalDays = (today - dailyStart).Days + 1;
            decimal avgDaily = totalDays > 0 ? allDaily.Sum(p => p.Price) / totalDays : 0;
            NrAvgDailySpend.Text = Math.Round(avgDaily, 2).ToEuroFormat();

            // --- Weekly average (weeks with 3+ distinct days)
            decimal totalWeekly = 0;
            int validWeeks = 0;
            var date = today;

            while (true)
            {
                var (start, end) = date.GetWeekRange();
                if (end < weeklyStart)
                    break;

                var weekPurchases = PurchaseDatabase.GetByDates(start, end);
                var daysWithPurchases = weekPurchases.Select(p => p.Date.Date).Distinct().Count();

                if (daysWithPurchases >= 3)
                {
                    totalWeekly += weekPurchases.Sum(p => p.Price);
                    validWeeks++;
                }

                date = date.AddDays(-7);
            }

            decimal avgWeekly = validWeeks > 0 ? totalWeekly / validWeeks : 0;
            NrAvgWeeklySpend.Text = Math.Round(avgWeekly, 2).ToEuroFormat();

            // --- Monthly average (months with 5+ purchases)
            decimal totalMonthly = 0;
            int validMonths = 0;
            date = today;

            while (true)
            {
                var (start, end) = date.GetMonthRange();
                if (end < monthlyStart)
                    break;

                var monthPurchases = PurchaseDatabase.GetByDates(start, end);

                if (monthPurchases.Length >= 5)
                {
                    totalMonthly += monthPurchases.Sum(p => p.Price);
                    validMonths++;
                }

                date = date.AddMonths(-1);
            }

            decimal avgMonthly = validMonths > 0 ? totalMonthly / validMonths : 0;
            NrAvgMonthlySpend.Text = Math.Round(avgMonthly, 2).ToEuroFormat();

            // --- Yearly average (years with 20+ purchases)
            decimal totalYearly = 0;
            int validYears = 0;
            date = today;

            while (true)
            {
                var (start, end) = date.GetYearRange();
                if (end < yearlyStart)
                    break;

                var yearPurchases = PurchaseDatabase.GetByDates(start, end);

                if (yearPurchases.Length >= 20)
                {
                    totalYearly += yearPurchases.Sum(p => p.Price);
                    validYears++;
                }

                date = date.AddYears(-1);
            }

            decimal avgYearly = validYears > 0 ? totalYearly / validYears : 0;
            NrAvgYearlySpend.Text = Math.Round(avgYearly, 2).ToEuroFormat();
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
            ListSpendingAnomalies.Items.Clear();
            var insights = InsightGenerators.GetInsights();
            foreach (var insight in insights)
                ListSpendingAnomalies.Items.Add(insight.Message);
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