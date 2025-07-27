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
            var purchases = PurchaseDatabase.GetByDates(today, today);
            PurchasesGrid.Rows.Clear();
            foreach (var purchase in purchases)
            {
                PurchasesGrid.Rows.Add(purchase.Id, purchase.Shop, purchase.Price.ToString(CultureInfo.InvariantCulture));
            }
            DatePicker.Value = today;
            DatePicker.Enabled = true;
            LblTitle.Text = "Purchases | Today";
            BtnShowToday.Visible = false;
            CmbCategory.SelectedIndex = 0;

            SetTotal();
        }

        public void LoadCustom(DateTime from, DateTime to)
        {
            var purchases = PurchaseDatabase.GetByDates(from, to);
            PurchasesGrid.Rows.Clear();
            foreach (var purchase in purchases)
            {
                PurchasesGrid.Rows.Add(purchase.Id, purchase.Shop, purchase.Price.ToString(CultureInfo.InvariantCulture));
            }

            // Adjust date picker properly
            if (from.Date != to.Date)
            {
                DatePicker.Enabled = false;
                LblTitle.Text = $"Purchases | {from.Date:d} - {to.Date:d}";
                BtnShowToday.Visible = true;
            }
            else
            {
                DatePicker.Enabled = true;
                DatePicker.Value = from.Date;

                if (from.Date == DateTime.Today.AddDays(-1).Date)
                {
                    LblTitle.Text = "Purchases | Yesterday";
                    BtnShowToday.Visible = true;
                }
                else if (from.Date == DateTime.Today.Date)
                {
                    LblTitle.Text = "Purchases | Today";
                    BtnShowToday.Visible = false;
                }
                else
                {
                    LblTitle.Text = $"Purchases | {from.Date:d}";
                    BtnShowToday.Visible = true;
                }
            }

            SetTotal();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var purchase = new Purchase();
            if (decimal.TryParse(NrAmount.Text.Replace(",", "."), CultureInfo.InvariantCulture, out var price))
            {
                purchase.Shop = TxtShopName.Text;
                purchase.Price = price;
                purchase.Date = DatePicker.Value;
                purchase.Category = CmbCategory.SelectedItem.ToString();

                PurchaseDatabase.Create(purchase);
                PurchasesGrid.Rows.Add(purchase.Id, purchase.Shop, purchase.Price.ToString(CultureInfo.InvariantCulture));

                TxtShopName.Clear();
                NrAmount.Clear();

                SetTotal();
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
                    SetTotal();
                }
            }
        }

        private void PurchasesGrid_SelectionChanged(object sender, EventArgs e)
        {
            BtnDelete.Enabled = PurchasesGrid.SelectedRows.Count > 0;
        }

        private void SetTotal()
        {
            var total = PurchasesGrid.Rows
                .OfType<DataGridViewRow>()
                .Sum(a => decimal.Parse(a.Cells["AmountCol"].Value.ToString(), CultureInfo.InvariantCulture));
            NrTotal.Text = "€ " + total.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnShowToday_Click(object sender, EventArgs e)
        {
            OnLoad();
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            LoadCustom(DatePicker.Value, DatePicker.Value);
        }
    }
}
