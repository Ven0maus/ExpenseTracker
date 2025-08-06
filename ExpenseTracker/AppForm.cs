using ExpenseTracker.Core;
using ExpenseTracker.UserControls;

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

        private string _dbPath;
        public string DbPath
        {
            get
            {
                if (!string.IsNullOrEmpty(_dbPath))
                    return _dbPath;
#if DEBUG
                _dbPath = Path.Combine(SettingsControl.GetAppStoragePath(), "expenses_debug.db");
#else
                _dbPath = Path.Combine(SettingsControl.GetAppStoragePath(), "expenses.db");
#endif
                return _dbPath;
            }
        }

        public void InitDatabase()
        {
            var settings = SettingsControl.Settings;
            if (settings.ResetDatabase)
            {
                File.Delete(DbPath);
                settings.ResetDatabase = false;
                SettingsControl.SaveFile();
            }

#if DEBUG
            TestDataCreator.InitDebugDatabase(DbPath, 365 * TestDataCreator.DebugDataYears, TestDataCreator.DebugDataSeed);
#else
            PurchaseDatabase.InitializeDatabase(DbPath);
#endif
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
    }

    public enum Views
    {
        Dashboard,
        Calendar,
        Purchases,
        Settings
    }
}
