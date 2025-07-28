namespace ExpenseTracker.Core
{
    internal class TestDataCreator
    {
        public const bool RegenerateOnRun = false;
        public static int? DebugDataSeed = null;
        public const int DebugDataYears = 5;

        private class CategoryInfo
        {
            public int Weight { get; set; }
            public string[] Shops { get; set; }
            public decimal MinPrice { get; set; }
            public decimal MaxPrice { get; set; }
        }

        private static readonly Dictionary<string, CategoryInfo> CategoryData = new()
        {
            { "Groceries", new CategoryInfo { Weight = 60, Shops = ["Aldi", "Frituur", "Carrefour", "Delhaize", "Colruyt", "Lidl"], MinPrice = 10, MaxPrice = 30 } },
            { "Clothes & Goods", new CategoryInfo { Weight = 10, Shops = ["Action", "Zeeman", "Primark", "Hema", "Wibra"], MinPrice = 15, MaxPrice = 120 } },
            { "Entertainment", new CategoryInfo { Weight = 5, Shops = ["Cinema", "Cafe", "Dinner"], MinPrice = 10, MaxPrice = 60 } },
            { "Subscriptions", new CategoryInfo { Weight = 3, Shops = ["Internet", "Electrabel", "Pidpa", "Spotify", "Netflix"], MinPrice = 20, MaxPrice = 70 } },
            { "Others", new CategoryInfo { Weight = 1, Shops = ["IKEA", "Kraker", "MegaMeubel", "Krefel", "Hubo"], MinPrice = 30, MaxPrice = 300 } }
        };

        public static void InitDebugDatabase(string dbPath, int daysOfData, int? seed = null)
        {
            var fileExists = File.Exists(dbPath);
            if (fileExists && RegenerateOnRun)
                File.Delete(dbPath);

            PurchaseDatabase.InitializeDatabase(dbPath);

            if (fileExists && !RegenerateOnRun) return;

            var now = DateTime.Now.Date;
            var rand = seed != null ? new Random(seed.Value) : new Random();
            const int chanceForNoExpense = 20;

            var weightedCategories = BuildWeightedCategoryList();

            for (int i=1; i < daysOfData + 1; i++)
            {
                var day = now.AddDays(-i);

                if (rand.Next(100) >= chanceForNoExpense)
                {
                    int purchases = rand.Next(1, 3);
                    for (int b = 0; b < purchases; b++)
                    {
                        // Select a category based on weight
                        var category = weightedCategories[rand.Next(weightedCategories.Count)];
                        var info = CategoryData[category];

                        // Pick a shop from the category
                        var shop = info.Shops[rand.Next(info.Shops.Length)];

                        // Generate a price in range
                        var price = Math.Round((decimal)(rand.NextDouble() * (double)(info.MaxPrice - info.MinPrice) + (double)info.MinPrice), 2);

                        PurchaseDatabase.Create(new Purchase
                        {
                            Category = category,
                            Shop = shop,
                            Date = day,
                            Price = price
                        });
                    }
                }
            };
        }

        private static List<string> BuildWeightedCategoryList()
        {
            var weightedList = new List<string>();

            foreach (var kv in CategoryData)
            {
                for (int i = 0; i < kv.Value.Weight; i++)
                {
                    weightedList.Add(kv.Key);
                }
            }

            return weightedList;
        }
    }
}
