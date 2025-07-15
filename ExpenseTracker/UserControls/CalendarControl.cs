using ExpenseTracker.UserControls;

namespace ExpenseTracker
{
    public partial class CalendarControl : UserControl, IUserControl
    {
        public CalendarControl()
        {
            InitializeComponent();
        }

        public void OnLoad()
        {

        }

        private void BtnViewPurchases_Click(object sender, EventArgs e)
        {
            AppForm.Instance.LoadContentCustom<PurchasesControl>(Views.Purchases, (control) =>
            {
                control.LoadCustom(Calendar.SelectionStart.Date, Calendar.SelectionEnd.Date);
            });
        }
    }
}
