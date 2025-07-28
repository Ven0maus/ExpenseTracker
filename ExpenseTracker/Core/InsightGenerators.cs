namespace ExpenseTracker.Core
{
    internal class InsightGenerators
    {
        private static readonly Func<SpendingInsight>[] _generators =
        [
            CompareWeeklySpending,
            DetectSpendingStreak,
            DetectBiggestPurchaseThisWeek,
            CompareMonthlyDailyAverage,
            DetectTopCategoryChange,
            DetectConsecutiveWeeklyTrend,
            DetectQuietCategory,
            GetHighestSpendingMonthSince,
            DetectWeekendSpendingSurge,
            DetectPurchaseStreakBreak,
            DetectMostFrequentShops,
            DetectAveragePurchaseChange,
            DetectDailyAverageAboveUsual,
            DetectMostCommonPurchaseDay,
            DetectBudgetProgress
        ];

        private static readonly string[] _definedCategories =
        [
            "Groceries",
            "Clothes & Goods",
            "Entertainment",
            "Subscriptions",
            "Others"
        ];

        private static int _month, _year;

        public static SpendingInsight[] GetInsights(int month, int year)
        {
            _month = month;
            _year = year;

            return [.. _generators
                .Select(generator => generator())
                .Where(insight => insight != null)];
        }

        private static DateTime TodayByMonthAndYear()
        {
            var today = DateTime.Today;
            if (today.Year != _year || today.Month != _month)
            {
                int day = DateTime.DaysInMonth(_year, _month);
                today = new DateTime(_year, _month, day);
            }
            return today;
        }

        public static SpendingInsight DetectBudgetProgress()
        {
            var today = TodayByMonthAndYear();

            // Month start and end
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);

            var purchasesThisMonth = PurchaseDatabase.GetByDates(monthStart, today);
            var totalSpent = purchasesThisMonth.Sum(p => p.Price);

            var settingsControl = AppForm.Instance.GetInstance<SettingsControl>(Views.Settings);
            var monthlyBudget = settingsControl.Settings.MonthlyBudget;

            // Percent of budget spent so far
            decimal percentSpent = monthlyBudget == 0 ? 0 : totalSpent / monthlyBudget * 100m;

            // Percent of month completed
            int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
            decimal percentMonthComplete = (decimal)today.Day / daysInMonth * 100m;

            // Only generate insight if spending is significantly ahead or behind schedule (e.g., 10%)
            const decimal threshold = 10m;

            decimal difference = percentSpent - percentMonthComplete;
            if (Math.Abs(difference) < threshold)
                return null;

            string message;
            if (difference > 0)
                message = $"You've already spent {Math.Round(percentSpent, 1)}% of your monthly budget, You’re spending faster than expected.";
            else
                return null;

            return new SpendingInsight
            {
                Category = InsightCategory.Comparative,
                Message = message
            };
        }

        public static SpendingInsight DetectMostCommonPurchaseDay()
        {
            var today = TodayByMonthAndYear();
            var startDate = today.AddDays(-90); // last 3 months

            var purchases = PurchaseDatabase.GetByDates(startDate, today);
            if (purchases.Length == 0)
                return null;

            // Group purchases by day of week and count
            var dayCounts = purchases
                .GroupBy(p => p.Date.DayOfWeek)
                .Select(g => new { Day = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            if (dayCounts.Count == 0)
                return null;

            // Check if the top day is significantly higher than others (e.g. at least 20% more purchases than second highest)
            var topDay = dayCounts[0];
            var secondDayCount = dayCounts.Count > 1 ? dayCounts[1].Count : 0;

            if (secondDayCount == 0 || topDay.Count < 5) // require at least 5 purchases for significance
                return null;

            decimal increasePercent = (decimal)(topDay.Count - secondDayCount) / secondDayCount * 100m;

            if (increasePercent < 20) // not significant enough difference
                return null;

            return new SpendingInsight
            {
                Category = InsightCategory.Behavioral,
                Message = $"Most of your purchases happen on {topDay.Day} — pattern spotted."
            };
        }

        public static SpendingInsight DetectAveragePurchaseChange()
        {
            var today = TodayByMonthAndYear();

            // Get this month range
            var thisMonthStart = new DateTime(today.Year, today.Month, 1);
            var thisMonthEnd = thisMonthStart.AddMonths(1).AddDays(-1);

            // Get last month range
            var lastMonthStart = thisMonthStart.AddMonths(-1);
            var lastMonthEnd = thisMonthStart.AddDays(-1);

            var thisMonthPurchases = PurchaseDatabase.GetByDates(thisMonthStart, thisMonthEnd);
            var lastMonthPurchases = PurchaseDatabase.GetByDates(lastMonthStart, lastMonthEnd);

            if (thisMonthPurchases.Length == 0 || lastMonthPurchases.Length == 0)
                return null;

            var thisMonthAvg = thisMonthPurchases.Average(p => p.Price);
            var lastMonthAvg = lastMonthPurchases.Average(p => p.Price);

            if (lastMonthAvg == 0) // avoid division by zero
                return null;

            var changePercent = (thisMonthAvg - lastMonthAvg) / lastMonthAvg * 100m;

            if (Math.Abs(changePercent) < 20)
                return null;

            var direction = changePercent > 0 ? "increased" : "decreased";
            var absChange = Math.Round(Math.Abs(changePercent), 1);

            return new SpendingInsight
            {
                Category = InsightCategory.Behavioral,
                Message = $"Your average purchase cost has {direction} by {absChange}% this month."
            };
        }

        private static SpendingInsight CompareWeeklySpending()
        {
            var today = TodayByMonthAndYear();
            var (thisWeekStart, thisWeekEnd) = today.GetWeekRange();
            var (lastWeekStart, lastWeekEnd) = today.AddDays(-7).GetWeekRange();

            var thisWeekTotal = PurchaseDatabase.GetByDates(thisWeekStart, thisWeekEnd).Sum(p => p.Price);
            var lastWeekTotal = PurchaseDatabase.GetByDates(lastWeekStart, lastWeekEnd).Sum(p => p.Price);

            if (lastWeekTotal == 0) return null;

            var percent = (thisWeekTotal - lastWeekTotal) / lastWeekTotal * 100;
            if (Math.Abs(percent) < 10) return null;

            var direction = percent > 0 ? "more" : "less";
            return new SpendingInsight
            {
                Category = InsightCategory.Comparative,
                Message = $"You spent {Math.Abs(Math.Round(percent))}% {direction} this week than last week."
            };
        }

        private static SpendingInsight DetectSpendingStreak()
        {
            int streak = 0;
            for (int i = 0; i < 10; i++)
            {
                var date = TodayByMonthAndYear().AddDays(-i);
                var purchases = PurchaseDatabase.GetByDates(date, date);
                if (purchases.Length != 0) streak++;
                else break;
            }

            if (streak >= 5)
            {
                return new SpendingInsight
                {
                    Category = InsightCategory.Behavioral,
                    Message = $"You’ve spent something every day for the last {streak} days — streak!"
                };
            }

            return null;
        }

        private static SpendingInsight DetectBiggestPurchaseThisWeek()
        {
            var (start, end) = TodayByMonthAndYear().GetWeekRange();
            var purchases = PurchaseDatabase.GetByDates(start, end);

            var biggest = purchases.OrderByDescending(p => p.Price).FirstOrDefault();
            if (biggest == null || biggest.Price < 10) return null;

            return new SpendingInsight
            {
                Category = InsightCategory.Behavioral,
                Message = $"Your biggest purchase this week was at {biggest.Shop}, costing {biggest.Price.ToEuroFormat()}."
            };
        }

        private static SpendingInsight CompareMonthlyDailyAverage()
        {
            var today = TodayByMonthAndYear();
            var thisMonthStart = new DateTime(today.Year, today.Month, 1);
            var lastMonthStart = thisMonthStart.AddMonths(-1);
            var lastMonthEnd = thisMonthStart.AddDays(-1);

            var thisMonthDays = (today - thisMonthStart).Days + 1;
            var thisMonthTotal = PurchaseDatabase.GetByDates(thisMonthStart, today).Sum(p => p.Price);
            var thisAvg = thisMonthTotal / thisMonthDays;

            var lastMonthTotal = PurchaseDatabase.GetByDates(lastMonthStart, lastMonthEnd).Sum(p => p.Price);
            var lastMonthDays = (lastMonthEnd - lastMonthStart).Days + 1;
            var lastAvg = lastMonthTotal / lastMonthDays;

            if (lastAvg == 0) return null;

            var percentChange = (thisAvg - lastAvg) / lastAvg * 100;
            if (Math.Abs(percentChange) < 15) return null;

            var dir = percentChange > 0 ? "above" : "below";

            return new SpendingInsight
            {
                Category = InsightCategory.Trend,
                Message = $"Your daily average this month is {Math.Round(Math.Abs(percentChange))}% {dir} last month’s average."
            };
        }

        private static SpendingInsight DetectTopCategoryChange()
        {
            var today = TodayByMonthAndYear();
            var thisMonthStart = new DateTime(today.Year, today.Month, 1);
            var lastMonthStart = thisMonthStart.AddMonths(-1);
            var lastMonthEnd = thisMonthStart.AddDays(-1);

            var thisMonth = PurchaseDatabase.GetByDates(thisMonthStart, today)
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, Total = g.Sum(p => p.Price) })
                .OrderByDescending(x => x.Total)
                .FirstOrDefault();

            var lastMonth = PurchaseDatabase.GetByDates(lastMonthStart, lastMonthEnd)
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, Total = g.Sum(p => p.Price) })
                .OrderByDescending(x => x.Total)
                .FirstOrDefault();

            if (thisMonth == null)
                return null;

            if (lastMonth != null && thisMonth.Category != lastMonth.Category)
            {
                return new SpendingInsight
                {
                    Category = InsightCategory.Category,
                    Message = $"Your top spending category this month is {thisMonth.Category}, instead of {lastMonth.Category} last month."
                };
            }

            return new SpendingInsight
            {
                Category = InsightCategory.Category,
                Message = $"Your top spending category this month is {thisMonth.Category}."
            };
        }

        public static SpendingInsight DetectConsecutiveWeeklyTrend()
        {
            const int weeks = 4; // must be 1 more then expected
            if (weeks < 2) throw new ArgumentException("weeks must be at least 2");

            decimal[] totals = new decimal[weeks];

            for (int i = 0; i < weeks; i++)
            {
                var (start, end) = TodayByMonthAndYear().AddDays(-7 * i).GetWeekRange();
                totals[weeks - 1 - i] = PurchaseDatabase.GetByDates(start, end).Sum(p => p.Price);
            }

            bool increasing = true;
            bool decreasing = true;

            for (int i = 1; i < weeks; i++)
            {
                if (totals[i] <= totals[i - 1]) increasing = false;
                if (totals[i] >= totals[i - 1]) decreasing = false;
            }

            if (increasing)
            {
                return new SpendingInsight
                {
                    Category = InsightCategory.Trend,
                    Message = $"Your spending has increased for {weeks - 1} consecutive weeks."
                };
            }

            if (decreasing)
            {
                return new SpendingInsight
                {
                    Category = InsightCategory.Trend,
                    Message = $"Your spending has decreased for {weeks - 1} consecutive weeks."
                };
            }

            return null;
        }

        public static SpendingInsight DetectDailyAverageAboveUsual()
        {
            var today = TodayByMonthAndYear();
            var thisMonthStart = new DateTime(today.Year, today.Month, 1);
            var thisMonthEnd = thisMonthStart.AddMonths(1).AddDays(-1);

            var last3MonthsStart = thisMonthStart.AddMonths(-3);
            var lastMonthEnd = thisMonthStart.AddDays(-1);

            var thisMonthPurchases = PurchaseDatabase.GetByDates(thisMonthStart, thisMonthEnd);
            var last3MonthsPurchases = PurchaseDatabase.GetByDates(last3MonthsStart, lastMonthEnd);

            int daysSoFar = (today - thisMonthStart).Days + 1;
            decimal thisMonthDailyAvg = daysSoFar > 0 ? thisMonthPurchases.Sum(p => p.Price) / daysSoFar : 0;

            int last3MonthsDays = (lastMonthEnd - last3MonthsStart).Days + 1;
            decimal last3MonthsDailyAvg = last3MonthsDays > 0 ? last3MonthsPurchases.Sum(p => p.Price) / last3MonthsDays : 0;

            if (last3MonthsDailyAvg == 0 || thisMonthDailyAvg <= last3MonthsDailyAvg)
                return null;

            decimal percentAbove = (thisMonthDailyAvg - last3MonthsDailyAvg) / last3MonthsDailyAvg * 100m;

            return new SpendingInsight
            {
                Category = InsightCategory.Trend,
                Message = $"Average daily spending this month is trending {Math.Round(percentAbove, 1)}% higher than the previous 3-month average."
            };
        }

        public static SpendingInsight DetectQuietCategory()
        {
            var (start, end) = TodayByMonthAndYear().GetMonthRange();
            var purchases = PurchaseDatabase.GetByDates(start, end);

            var categories = purchases.GroupBy(p => p.Category)
                                      .ToDictionary(g => g.Key, g => g.Sum(p => p.Price));

            var unusedCategories = _definedCategories.Where(a => !categories.ContainsKey(a))
                .ToArray();
            if (unusedCategories.Length > 0)
            {
                return new SpendingInsight
                {
                    Category = InsightCategory.Category,
                    Message = $"No purchases this month in the following categories: {string.Join(", ", unusedCategories)}"
                };
            }

            return null;
        }

        public static SpendingInsight GetHighestSpendingMonthSince()
        {
            var now = TodayByMonthAndYear();
            var currentMonthStart = new DateTime(now.Year, now.Month, 1);
            var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1);

            var currentTotal = PurchaseDatabase.GetByDates(currentMonthStart, now).Sum(p => p.Price);
            if (currentTotal == 0) return null;

            DateTime? lastHigherMonth = null;

            for (int i = 1; i <= 12; i++)
            {
                var monthStart = currentMonthStart.AddMonths(-i);
                var monthEnd = monthStart.AddMonths(1).AddDays(-1);
                var total = PurchaseDatabase.GetByDates(monthStart, monthEnd).Sum(p => p.Price);

                if (total > currentTotal)
                {
                    lastHigherMonth = monthStart;
                    break;
                }
            }

            if (lastHigherMonth == null)
            {
                return new SpendingInsight
                {
                    Category = InsightCategory.Comparative,
                    Message = $"This month’s spending is your highest in over a year."
                };
            }

            return new SpendingInsight
            {
                Category = InsightCategory.Comparative,
                Message = $"This month’s spending is your highest since {lastHigherMonth.Value:MMMM yyyy}."
            };
        }

        public static SpendingInsight DetectWeekendSpendingSurge()
        {
            var now = TodayByMonthAndYear();
            var start = now.AddDays(-28); // Last 4 weeks
            var purchases = PurchaseDatabase.GetByDates(start, now);

            var weekendTotal = purchases.Where(p => p.Date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday).Sum(p => p.Price);
            var weekdayTotal = purchases.Where(p => p.Date.DayOfWeek is >= DayOfWeek.Monday and <= DayOfWeek.Friday).Sum(p => p.Price);

            if (weekdayTotal == 0 || weekendTotal / weekdayTotal < 1.5m) return null;

            var percent = (weekendTotal - weekdayTotal) / weekdayTotal * 100;
            return new SpendingInsight
            {
                Category = InsightCategory.Trend,
                Message = $"You spent {Math.Round(percent)}% more on weekends than weekdays over the last month."
            };
        }

        public static SpendingInsight DetectPurchaseStreakBreak()
        {
            int noSpendStreak = 0;

            for (int i = 1; i <= 7; i++)
            {
                var date = TodayByMonthAndYear().AddDays(-i);
                var purchases = PurchaseDatabase.GetByDates(date, date);
                if (purchases.Length == 0) noSpendStreak++;
                else break;
            }

            if (noSpendStreak >= 3)
            {
                return new SpendingInsight
                {
                    Category = InsightCategory.Behavioral,
                    Message = $"You didn’t spend anything for {noSpendStreak} days straight last week — rare!"
                };
            }

            return null;
        }

        public static SpendingInsight DetectMostFrequentShops()
        {
            var (start, end) = TodayByMonthAndYear().GetMonthRange();
            var purchases = PurchaseDatabase.GetByDates(start, end);
            if (purchases == null || purchases.Length == 0)
                return null;

            var totalPurchases = purchases.Length;

            var topShops = purchases
                .GroupBy(p => p.Shop, StringComparer.OrdinalIgnoreCase)
                .Select(g => new
                {
                    Shop = g.Key,
                    Count = g.Count(),
                    Percentage = (decimal)g.Count() / totalPurchases * 100m
                })
                .OrderByDescending(x => x.Count)
                .Take(3)
                .ToList();

            if (topShops.Count == 0)
                return null;

            var summary = string.Join(", ", topShops.Select(s =>
                $"{s.Shop} ({Math.Round(s.Percentage, 1)}%)"));

            var combinedShare = (decimal)topShops.Sum(x => x.Count) / totalPurchases * 100m;

            return new SpendingInsight
            {
                Category = InsightCategory.Behavioral,
                Message = $"You made {Math.Round(combinedShare, 1)}% of your purchases at your top shops: {summary}."
            };
        }
    }
}
