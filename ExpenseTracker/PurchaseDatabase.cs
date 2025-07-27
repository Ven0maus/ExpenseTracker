using Microsoft.Data.Sqlite;

namespace ExpenseTracker
{
    internal static class PurchaseDatabase
    {
        private static string _dbPath = null;

        public static bool IsInitialized => !string.IsNullOrWhiteSpace(_dbPath);

        public static void InitializeDatabase(string dbPath)
        {
            if (string.IsNullOrWhiteSpace(dbPath))
                throw new Exception("Invalid db path.");

            _dbPath = dbPath;

            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"
    CREATE TABLE IF NOT EXISTS Purchases (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Shop TEXT,
        Date INTEGER,
        Price REAL,
        Category TEXT
    );";
            tableCmd.ExecuteNonQuery();
        }

        public static Purchase Read(int id)
        {
            if (!IsInitialized)
                throw new Exception("You must initialize the database first.");

            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Purchases WHERE Id = $id";
            cmd.Parameters.AddWithValue("$id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Purchase
                {
                    Id = reader.GetInt32(0),
                    Shop = reader.GetString(1),
                    Date = reader.GetInt64(2).FromUnixTimestamp(),
                    Price = reader.GetDecimal(3)
                };
            }

            return null; // or throw if you prefer
        }

        public static void Create(Purchase purchase)
        {
            if (!IsInitialized)
                throw new Exception("You must initialize the database first.");

            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var transaction = connection.BeginTransaction();

            var purchaseCmd = connection.CreateCommand();
            purchaseCmd.CommandText = @"
        INSERT INTO Purchases (Shop, Date, Price)
        VALUES ($shop, $date, $price, $category)";
            purchaseCmd.Parameters.AddWithValue("$shop", purchase.Shop);
            purchaseCmd.Parameters.AddWithValue("$date", purchase.Date.ToUnixTimestamp());
            purchaseCmd.Parameters.AddWithValue("$price", purchase.Price);
            purchaseCmd.Parameters.AddWithValue("$category", purchase.Category);
            purchaseCmd.ExecuteNonQuery();

            // Get the last inserted row ID
            var idCmd = connection.CreateCommand();
            idCmd.Transaction = transaction; // keep within the transaction scope
            idCmd.CommandText = "SELECT last_insert_rowid();";
            var id = (long)idCmd.ExecuteScalar();
            purchase.Id = (int)id;

            transaction.Commit();
        }

        public static void Update(Purchase purchase)
        {
            if (!IsInitialized)
                throw new Exception("You must initialize the database first.");

            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var transaction = connection.BeginTransaction();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
        UPDATE Purchases
        SET Shop = $shop,
            Date = $date,
            Price = $price,
            Category = $category
        WHERE Id = $id";

            cmd.Parameters.AddWithValue("$id", purchase.Id);
            cmd.Parameters.AddWithValue("$shop", purchase.Shop);
            cmd.Parameters.AddWithValue("$date", purchase.Date.ToUnixTimestamp());
            cmd.Parameters.AddWithValue("$price", purchase.Price);
            cmd.Parameters.AddWithValue("$category", purchase.Category);

            cmd.ExecuteNonQuery();
            transaction.Commit();
        }

        public static void Delete(Purchase purchase)
        {
            if (!IsInitialized)
                throw new Exception("You must initialize the database first.");

            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var transaction = connection.BeginTransaction();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Purchases WHERE Id = $id";
            cmd.Parameters.AddWithValue("$id", purchase.Id);

            cmd.ExecuteNonQuery();
            transaction.Commit();
        }

        public static IEnumerable<Purchase> ListAll()
        {
            if (!IsInitialized)
                throw new Exception("You must initialize the database first.");

            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var purchaseCmd = connection.CreateCommand();
            purchaseCmd.CommandText = "SELECT * FROM Purchases";
            using var reader = purchaseCmd.ExecuteReader();

            while (reader.Read())
            {
                var purchase = new Purchase
                {
                    Id = reader.GetInt32(0),
                    Shop = reader.GetString(1),
                    Date = reader.GetInt64(2).FromUnixTimestamp(),
                    Price = reader.GetDecimal(3),
                    Category = reader.GetString(4)
                };

                yield return purchase;
            }
        }

        public static IEnumerable<Purchase> GetByDates(DateTime from, DateTime to)
        {
            if (!IsInitialized)
                throw new Exception("You must initialize the database first.");

            long fromTs = from.ToUnixTimestamp();
            long toTs = to.ToUnixTimestamp();

            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
        SELECT * FROM Purchases
        WHERE Date BETWEEN $from AND $to
        ORDER BY Date ASC";

            cmd.Parameters.AddWithValue("$from", fromTs);
            cmd.Parameters.AddWithValue("$to", toTs);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new Purchase
                {
                    Id = reader.GetInt32(0),
                    Shop = reader.GetString(1),
                    Date = reader.GetInt64(2).FromUnixTimestamp(),
                    Price = reader.GetDecimal(3),
                    Category = reader.GetString(4)
                };
            }
        }

        public static double GetAllTimeExpenses()
        {
            if (!IsInitialized)
                throw new Exception("You must initialize the database first.");

            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT SUM(Price) from Purchases";

            var value = cmd.ExecuteScalar();
            return (double)(value is DBNull ? 0d : value);
        }
    }
}
