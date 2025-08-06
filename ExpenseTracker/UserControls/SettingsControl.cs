using ExpenseTracker.UserControls;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;

namespace ExpenseTracker
{
    public partial class SettingsControl : UserControl, IUserControl
    {
        private static readonly Lazy<string> _settingsFilePath = new(GetSettingsFilePath);

        public static Settings Settings { get; set; }

        private static readonly JsonSerializerOptions _serializerOptions = new()
        {
            WriteIndented = true
        };

        static SettingsControl()
        {
            LoadFile();
        }

        public SettingsControl()
        {
            InitializeComponent();
        }

        public void OnLoad()
        {
            NrMonthlyBudget.Text = Settings.MonthlyBudget.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(NrMonthlyBudget.Text.Replace(",", "."), CultureInfo.InvariantCulture, out var price))
            {
                NrMonthlyBudget.Text = price.ToString(CultureInfo.InvariantCulture);
                Settings.MonthlyBudget = price;
            }

            SaveFile();
        }

        public static string GetAppStoragePath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ExpenseTracker"
            );
        }

        private static string GetSettingsFilePath()
        {
            return Path.Combine(GetAppStoragePath(), "settings.json");
        }

        public static void SaveFile()
        {
            var directory = Path.GetDirectoryName(_settingsFilePath.Value);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var json = JsonSerializer.Serialize(Settings, _serializerOptions);
            File.WriteAllText(_settingsFilePath.Value, json);
        }

        private static void LoadFile()
        {
            if (File.Exists(_settingsFilePath.Value))
            {
                var json = File.ReadAllText(_settingsFilePath.Value);

                try
                {
                    Settings = JsonSerializer.Deserialize<Settings>(json);
                }
                catch (Exception)
                {
                    File.Delete(_settingsFilePath.Value);
                }
            }

            if (Settings == null)
            {
                Settings = new Settings();
                SaveFile();
            }
        }

        private void BtnResetDb_Click(object sender, EventArgs e)
        {
            var msgBox = MessageBox.Show("This is a destructive action that cannot be undone, are you sure?", "Are you sure?", MessageBoxButtons.YesNo);
            if (msgBox == DialogResult.Yes && File.Exists(AppForm.Instance.DbPath))
            {
                Settings.ResetDatabase = true;
                SaveFile();

                _ = MessageBox.Show("A restart of the application is required to continue.", "Restart required", MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        private void BtnOpenDbFolder_Click(object sender, EventArgs e)
        {
            _ = Process.Start("explorer.exe", Path.GetDirectoryName(AppForm.Instance.DbPath));
        }
    }

    public class Settings
    {
        public decimal MonthlyBudget { get; set; }
        public bool ResetDatabase { get; set; } = false;
    }
}
