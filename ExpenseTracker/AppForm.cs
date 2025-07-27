using ExpenseTracker.UserControls;
using System.ComponentModel;

namespace ExpenseTracker
{
    public partial class AppForm : Form
    {
        public static AppForm Instance { get; private set; }

        private readonly Dictionary<Views, UserControl> _views = [];

        private IUserControl _currentOpenControl;

        public AppForm()
        {
            Instance = this;
            InitializeComponent();
            InitDatabase();
            InitViews();

            // Base view
            LoadContent(_views[Views.Dashboard]);
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
            if (_currentOpenControl is DashboardControl) return;
            LoadContent(_views[Views.Dashboard]);
        }

        private void BtnCalendar_Click(object sender, EventArgs e)
        {
            if (_currentOpenControl is CalendarControl) return;
            LoadContent(_views[Views.Calendar]);
        }

        private void BtnPurchases_Click(object sender, EventArgs e)
        {
            if (_currentOpenControl is PurchasesControl) return;
            LoadContent(_views[Views.Purchases]);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            if (_currentOpenControl is SettingsControl) return;
            LoadContent(_views[Views.Settings]);
        }

        private void LoadContent(UserControl control, bool executeOnLoad = true)
        {
            ContentPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            control.Font = (Font)control.Font.Clone();
            ContentPanel.Controls.Add(control);
            _currentOpenControl = control as IUserControl;

            // Execute interface methods
            if (executeOnLoad)
                _currentOpenControl?.OnLoad();
        }

        public void LoadContentCustom<T>(Views view, Action<T> onLoad)
            where T : UserControl, IUserControl
        {
            var control = _views[view];
            if (control is not T castedControl)
                return;

            LoadContent(castedControl, false);
            onLoad?.Invoke(castedControl);
        }

        public T GetInstance<T>(Views view) where T : UserControl, IUserControl
        {
            var control = _views[view];
            if (control is not T castedControl)
                return default;
            return castedControl;
        }
    }

    public enum Views
    {
        Dashboard,
        Calendar,
        Purchases,
        Settings
    }
}
