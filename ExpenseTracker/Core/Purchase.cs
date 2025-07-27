namespace ExpenseTracker.Core
{
    internal class Purchase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Shop { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
