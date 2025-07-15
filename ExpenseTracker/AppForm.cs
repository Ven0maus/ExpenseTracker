using ExpenseTracker.UserControls;

namespace ExpenseTracker
{
    public partial class AppForm : Form
    {
        private readonly Dictionary<Views, UserControl> _views = [];

        public AppForm()
        {
            InitializeComponent();
            InitDatabase();
            InitViews();
        }

        private static void InitDatabase()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "expenses.db");
            PurchaseDatabase.InitializeDatabase(dbPath);
        }

        private void InitViews()
        {
            _views[Views.Dashboard] = new DashboardControl();
            _views[Views.Calendar] = new CalendarControl();
            _views[Views.Purchases] = new PurchasesControl();
            _views[Views.Settings] = new SettingsControl();
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            LoadContent(_views[Views.Dashboard]);
        }

        private void BtnCalendar_Click(object sender, EventArgs e)
        {
            LoadContent(_views[Views.Calendar]);
        }

        private void BtnPurchases_Click(object sender, EventArgs e)
        {
            LoadContent(_views[Views.Purchases]);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            LoadContent(_views[Views.Settings]);
        }

        private void LoadContent(UserControl control)
        {
            ContentPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            control.Font = (Font)control.Font.Clone();
            ContentPanel.Controls.Add(control);

            // Execute interface methods
            var controlInterface = control as IUserControl;
            controlInterface?.OnLoad();
        }

        private enum Views
        {
            Dashboard,
            Calendar,
            Purchases,
            Settings
        }
    }
}
