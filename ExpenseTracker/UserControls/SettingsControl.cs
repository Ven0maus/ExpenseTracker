using ExpenseTracker.UserControls;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json;

namespace ExpenseTracker
{
    public partial class SettingsControl : UserControl, IUserControl
    {
        private static readonly Lazy<string> _settingsFilePath = new(GetSettingsFilePath);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Settings Settings { get; set; }

        private static readonly JsonSerializerOptions _serializerOptions = new()
        {
            WriteIndented = true
        };

        public SettingsControl()
        {
            InitializeComponent();
            LoadFile();
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

        private void SaveFile()
        {
            var directory = Path.GetDirectoryName(_settingsFilePath.Value);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var json = JsonSerializer.Serialize(Settings, _serializerOptions);
            File.WriteAllText(_settingsFilePath.Value, json);
        }

        private void LoadFile()
        {
            if (File.Exists(_settingsFilePath.Value))
            {
                var json = File.ReadAllText(_settingsFilePath.Value);

                try
                {
                    Settings = JsonSerializer.Deserialize<Settings>(json);
                }
                catch(Exception)
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
    }

    public class Settings
    {
        public decimal MonthlyBudget { get; set; }
    }
}
