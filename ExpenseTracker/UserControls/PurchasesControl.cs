using ExpenseTracker.UserControls;
using System.Globalization;

namespace ExpenseTracker
{
    public partial class PurchasesControl : UserControl, IUserControl
    {
        public PurchasesControl()
        {
            InitializeComponent();
        }

        public void OnLoad()
        {
            var today = DateTime.Today.Date;
            var todaysPurchases = PurchaseDatabase.GetByDates(today, today);
            PurchasesGrid.Rows.Clear();
            foreach (var purchase in todaysPurchases)
            {
                PurchasesGrid.Rows.Add(purchase.Id, purchase.Shop, purchase.Price);
            }
            DatePicker.Value = today;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var purchase = new Purchase();
            if (decimal.TryParse(NrAmount.Text, CultureInfo.InvariantCulture, out var price))
            {
                purchase.Shop = TxtShopName.Text;
                purchase.Price = price;
                purchase.Date = DatePicker.Value;

                PurchaseDatabase.Create(purchase);
                PurchasesGrid.Rows.Add(purchase.Id, purchase.Shop, purchase.Price);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (PurchasesGrid.SelectedRows.Count == 1)
            {
                var row = PurchasesGrid.SelectedRows[0];
                var id = row.Cells["IdCol"].Value?.ToString() ?? "";
                if (!string.IsNullOrWhiteSpace(id) && int.TryParse(id, out var idInteger))
                {
                    PurchaseDatabase.Delete(new Purchase { Id = idInteger });
                    PurchasesGrid.ClearSelection();
                    PurchasesGrid.Rows.Remove(row);
                }
            }
        }

        private void PurchasesGrid_SelectionChanged(object sender, EventArgs e)
        {
            BtnDelete.Enabled = PurchasesGrid.SelectedRows.Count > 0;
        }
    }
}
