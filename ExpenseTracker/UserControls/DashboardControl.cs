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

            // Run the calculations on a background thread
            Task.Run(() =>
            {
                // Perform all calculations here, but **do NOT update UI controls here**.
                // Instead, collect all results into a container object.
                var results = new CalculationResults
                {
                    BudgetData = CalculateBudget(),
                    WeeklyData = CalculatePeriod(PeriodType.Weekly),
                    MonthlyData = CalculatePeriod(PeriodType.Monthly),
                    YearlyData = CalculatePeriod(PeriodType.Yearly),
                    AveragesData = CalculateAverages(),
                    CategorySummaryData = CalculateCategorySummary(),
                    BiggestExpensesData = CalculateBiggestExpenses(),
                    SpendingAnomaliesData = CalculateSpendingAnomalies()
                };

                // Clear Cache as to not retain old information in cache for no reason
                PurchaseDatabase.ClearCaches();

                // Marshall data to the UI thread
                Invoke(() =>
                {
                    SetUIData(results);
                });
            });
        }

        private void SetUIData(CalculationResults calcResults)
        {
            /* Budget Data */
            NrTotalSpend.Text = calcResults.BudgetData.TotalSpend.ToEuroFormat();
            NrMonthlyBudget.Text = calcResults.BudgetData.MonthlyBudget.ToEuroFormat();
            NrBudgetLeft.Text = calcResults.BudgetData.BudgetLeft.ToEuroFormat();

            var twentyPercentOfMonthlyBudget = Math.Round(calcResults.BudgetData.MonthlyBudget * 0.2m, 2);
            if (calcResults.BudgetData.BudgetLeft >= twentyPercentOfMonthlyBudget)
                NrBudgetLeft.ForeColor = Color.ForestGreen;
            else if (calcResults.BudgetData.BudgetLeft > 0)
                NrBudgetLeft.ForeColor = Color.Orange;
            else
                NrBudgetLeft.ForeColor = Color.Red;

            /* Weekly Data */
            NrThisWeekTotal.Text = calcResults.WeeklyData.ThisPeriodTotal.ToEuroFormat();
            NrLastWeeKTotal.Text = calcResults.WeeklyData.LastPeriodTotal.ToEuroFormat();
            NrThisWeekPrediction.Text = Math.Round(calcResults.WeeklyData.Prediction, 0).ToEuroFormat();
            NrComparedToLastWeek.Text = Math.Round(calcResults.WeeklyData.PercentChange, 1).ToPercentageFormat();
            if (calcResults.WeeklyData.PercentChange <= 0)
                NrComparedToLastWeek.ForeColor = Color.ForestGreen;
            else
                NrComparedToLastWeek.ForeColor = Color.Red;

            /* Monthly Data */
            NrThisMonthTotal.Text = calcResults.MonthlyData.ThisPeriodTotal.ToEuroFormat();
            NrLastMonthTotal.Text = calcResults.MonthlyData.LastPeriodTotal.ToEuroFormat();
            NrThisMonthPrediction.Text = Math.Round(calcResults.MonthlyData.Prediction, 0).ToEuroFormat();
            NrComparedToLastMonth.Text = Math.Round(calcResults.MonthlyData.PercentChange, 1).ToPercentageFormat();
            if (calcResults.MonthlyData.PercentChange <= 0)
                NrComparedToLastMonth.ForeColor = Color.ForestGreen;
            else
                NrComparedToLastMonth.ForeColor = Color.Red;

            /* Yearly Data */
            NrThisYearTotal.Text = calcResults.YearlyData.ThisPeriodTotal.ToEuroFormat();
            NrLastYearTotal.Text = calcResults.YearlyData.LastPeriodTotal.ToEuroFormat();
            NrThisYearPrediction.Text = Math.Round(calcResults.YearlyData.Prediction, 0).ToEuroFormat();
            NrComparedToLastYear.Text = Math.Round(calcResults.YearlyData.PercentChange, 1).ToPercentageFormat();
            if (calcResults.YearlyData.PercentChange <= 0)
                NrComparedToLastYear.ForeColor = Color.ForestGreen;
            else
                NrComparedToLastYear.ForeColor = Color.Red;

            /* Averages Data */
            NrAvgDailySpend.Text = calcResults.AveragesData.AvgDaily.ToEuroFormat();
            NrAvgWeeklySpend.Text = calcResults.AveragesData.AvgWeekly.ToEuroFormat();
            NrAvgMonthlySpend.Text = calcResults.AveragesData.AvgMonthly.ToEuroFormat();
            NrAvgYearlySpend.Text = calcResults.AveragesData.AvgYearly.ToEuroFormat();

            /* Category Summary Data */
            var labels = new Dictionary<int, Label>
            {
                {0, SpendTrend1},
                {1, SpendTrend2},
                {2, SpendTrend3},
                {3, SpendTrend4},
                {4, SpendTrend5},
            };
            var values = new Dictionary<int, Label>
            {
                {0, SpendTrendValue1},
                {1, SpendTrendValue2},
                {2, SpendTrendValue3},
                {3, SpendTrendValue4},
                {4, SpendTrendValue5},
            };

            for (int i = 0; i < calcResults.CategorySummaryData.TopCategories.Length; i++)
            {
                var item = calcResults.CategorySummaryData.TopCategories[i];
                labels[i].Text = $"{i + 1}. {item.Category}";
                values[i].Text = item.Total.ToEuroFormat();
            }

            // Clear unused rows
            for (int i = 4; i >= calcResults.CategorySummaryData.TopCategories.Length; i--)
            {
                labels[i].Text = string.Empty;
                values[i].Text = string.Empty;
            }

            /* Biggest Expenses Data */
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

            for (int i = 0; i < calcResults.BiggestExpensesData.TopExpenses.Length; i++)
            {
                var item = calcResults.BiggestExpensesData.TopExpenses[i];
                expenseLabels[i].Text = $"{i + 1}. {item.Shop}";
                expenseValues[i].Text = item.Total.ToEuroFormat();
            }

            // Clear unused rows
            for (int i = 4; i >= calcResults.BiggestExpensesData.TopExpenses.Length; i--)
            {
                expenseLabels[i].Text = string.Empty;
                expenseValues[i].Text = string.Empty;
            }

            /* Spending Anomalies Data */
            ListSpendingAnomalies.Items.Clear();
            foreach (var insight in calcResults.SpendingAnomaliesData)
                ListSpendingAnomalies.Items.Add(insight.Message);
        }

        private static BudgetData CalculateBudget()
        {
            var totalSpend = PurchaseDatabase.GetAllTimeExpenses();
            var settingsControl = AppForm.Instance.GetInstance<SettingsControl>(Views.Settings);
            var monthlyBudget = settingsControl.Settings.MonthlyBudget;
            var (start, end) = DateTime.Now.GetMonthRange();
            var spendThisMonth = (decimal)PurchaseDatabase.GetExpensesByDates(start, end);
            var budgetLeft = monthlyBudget - spendThisMonth;

            return new BudgetData
            {
                MonthlyBudget = monthlyBudget,
                BudgetLeft = budgetLeft,
                TotalSpend = (decimal)totalSpend
            };
        }

        private static PeriodData CalculatePeriod(PeriodType period)
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

            return new PeriodData
            {
                ThisPeriodTotal = thisPeriodTotal,
                LastPeriodTotal = lastPeriodTotal,
                Prediction = prediction,
                PercentChange = percentChange
            };
        }

        private static AveragesData CalculateAverages()
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

            return new AveragesData
            {
                AvgDaily = Math.Round(avgDaily, 2),
                AvgWeekly = Math.Round(avgWeekly, 2),
                AvgMonthly = Math.Round(avgMonthly, 2),
                AvgYearly = Math.Round(avgYearly, 2)
            };
        }

        private static CategorySummaryData CalculateCategorySummary()
        {
            var (start, end) = GetPeriodDateRange(DateTime.Now, PeriodType.Monthly);
            var purchases = PurchaseDatabase.GetByDates(start, end);

            var topCategories = purchases
                .GroupBy(p => p.Category, StringComparer.OrdinalIgnoreCase)
                .Select(g => new CategorySummaryData.CategorySummaryItem
                {
                    Category = g.Key,
                    Total = g.Sum(p => p.Price)
                })
                .OrderByDescending(item => item.Total)
                .Take(5)
                .ToArray();

            return new CategorySummaryData
            {
                TopCategories = topCategories
            };
        }

        private static BiggestExpensesData CalculateBiggestExpenses()
        {
            var (start, end) = GetPeriodDateRange(DateTime.Now, PeriodType.Monthly);
            var purchases = PurchaseDatabase.GetByDates(start, end);

            // Group by shop and sum totals
            var biggestExpenses = purchases
                .GroupBy(p => p.Shop, StringComparer.OrdinalIgnoreCase)
                .Select(g => new BiggestExpensesData.BigExpenseItem()
                {
                    Shop = g.Key,
                    Total = g.Sum(p => p.Price)
                })
                .OrderByDescending(g => g.Total)
                .Take(5)
                .ToArray();

            return new BiggestExpensesData
            {
                TopExpenses = biggestExpenses
            };
        }

        private static SpendingInsight[] CalculateSpendingAnomalies()
        {
            return InsightGenerators.GetInsights();
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