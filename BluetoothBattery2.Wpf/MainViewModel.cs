using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using BluetoothBattery2.Core;

namespace BluetoothBattery2.Wpf
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly SettingsManager settingsManager = new();
        private readonly BluetoothService bluetoothService = new();
        private readonly StartupRegistrationService startupRegistrationService = new();
        private readonly LocalizationService localizationService = new();
        private readonly string settingsPath;
        private readonly string legacySettingsPath;

        private Settings settings = new();
        private string? selectedDevice;
        private readonly ObservableCollection<string> refreshLogs = new();
        private string refreshLogText = string.Empty;
        private bool isRefreshLogEnabled;
        public ObservableCollection<string> RefreshLogs => refreshLogs;
        public string RefreshLogText
        {
            get => refreshLogText;
            private set => SetProperty(ref refreshLogText, value);
        }
        public bool IsRefreshLogEnabled
        {
            get => isRefreshLogEnabled;
            set
            {
                if (SetProperty(ref isRefreshLogEnabled, value))
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RefreshLogToggleButtonText)));
                    if (!value)
                    {
                        refreshLogs.Clear();
                        RefreshLogText = string.Empty;
                    }
                }
            }
        }

        private int refreshInterval;
        private string? selectedFontFamily;
        private int iconOffsetX;
        private int iconOffsetY;
        private int iconFontSize;
        private bool isCloseButtonMinimize;
        private bool runOnStartup;
        private LanguageOption? selectedLanguage;
        private string statusMessage;
        private bool isBusy;
        private bool isApplyingSettings;

        private static readonly string[] LocalizedPropertyNames =
        {
            nameof(HelpPrompt),
            nameof(HelpButtonText),
            nameof(HeaderDevicesText),
            nameof(DeviceSelectionHint),
            nameof(RefreshDevicesButtonText),
            nameof(HeaderRefreshFontText),
            nameof(RefreshIntervalLabelText),
            nameof(RefreshLogLabelText),
            nameof(RefreshLogToggleButtonText),
            nameof(FontLabelText),
            nameof(LanguageLabelText),
            nameof(RunOnStartupLabelText),
            nameof(CloseButtonMinimizeLabelText),
            nameof(HeaderIconLayoutText),
            nameof(IconOffsetXLabelText),
            nameof(IconOffsetYLabelText),
            nameof(IconFontSizeLabelText),
            nameof(HeaderActionsText),
            nameof(SaveButtonText),
            nameof(ResetButtonText),
            nameof(StatusHeaderText)
        };

        public ObservableCollection<string> AvailableDevices { get; } = new();
        public ObservableCollection<string> FontFamilies { get; } = new();
        public ObservableCollection<LanguageOption> Languages { get; } = new();

        public RelayCommand RefreshDevicesCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetCommand { get; }
        public RelayCommand ToggleLogsCommand { get; }

        public Settings Settings => settings;

        public event EventHandler<SettingsAppliedEventArgs>? SettingsApplied;
        public event EventHandler? LayoutPreviewRequested;

        private string CurrentLanguageCode => localizationService.NormalizeLanguageCode(SelectedLanguage?.Code ?? settings.language);

        public MainViewModel()
        {
            var appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BluetoothBattery2");
            Directory.CreateDirectory(appDataFolder);
            settingsPath = Path.Combine(appDataFolder, "settings.txt");
            legacySettingsPath = Path.Combine(AppContext.BaseDirectory, "settings.txt");
            statusMessage = localizationService.GetText("StatusReady");
            LoadFonts();
            LoadLanguages();
            MigrateLegacySettingsIfNeeded();
            LoadSettings();

            RefreshDevicesCommand = new RelayCommand(async _ => await RefreshDevicesAsync(), _ => !IsBusy);
            SaveCommand = new RelayCommand(_ => SaveSettings(), _ => !IsBusy);
            ResetCommand = new RelayCommand(_ => ResetToDefault(), _ => !IsBusy);
            ToggleLogsCommand = new RelayCommand(_ => IsRefreshLogEnabled = !IsRefreshLogEnabled);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? SelectedDevice
        {
            get => selectedDevice;
            set => SetProperty(ref selectedDevice, value);
        }

        public int RefreshInterval
        {
            get => refreshInterval;
            set => SetProperty(ref refreshInterval, value);
        }

        public string? SelectedFontFamily
        {
            get => selectedFontFamily;
            set
            {
                if (SetProperty(ref selectedFontFamily, value))
                {
                    settings.fontFamilyName = value ?? settings.fontFamilyName;
                    RaiseLayoutPreview();
                }
            }
        }

        public int IconOffsetX
        {
            get => iconOffsetX;
            set
            {
                if (SetProperty(ref iconOffsetX, value))
                {
                    settings.iconOffsetX = value;
                    RaiseLayoutPreview();
                }
            }
        }

        public int IconOffsetY
        {
            get => iconOffsetY;
            set
            {
                if (SetProperty(ref iconOffsetY, value))
                {
                    settings.iconOffsetY = value;
                    RaiseLayoutPreview();
                }
            }
        }

        public int IconFontSize
        {
            get => iconFontSize;
            set
            {
                if (SetProperty(ref iconFontSize, value))
                {
                    settings.iconFontSize = value;
                    RaiseLayoutPreview();
                }
            }
        }

        public bool IsCloseButtonMinimize
        {
            get => isCloseButtonMinimize;
            set => SetProperty(ref isCloseButtonMinimize, value);
        }

        public bool RunOnStartup
        {
            get => runOnStartup;
            set
            {
                if (SetProperty(ref runOnStartup, value))
                {
                    settings.runOnStartup = value;
                    if (!isApplyingSettings)
                    {
                        UpdateStartupRegistration();
                    }
                }
            }
        }

        public LanguageOption? SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                if (SetProperty(ref selectedLanguage, value) && value != null)
                {
                    settings.language = localizationService.NormalizeLanguageCode(value.Code);
                    UpdateStatus("StatusLoaded");
                    NotifyLocalizationChanged();
                    NotifySettingsApplied();
                }
            }
        }

        public string StatusMessage
        {
            get => statusMessage;
            set => SetProperty(ref statusMessage, value);
        }

        public bool IsBusy
        {
            get => isBusy;
            private set
            {
                if (SetProperty(ref isBusy, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string HelpPrompt => GetLocalizedText("HelpPrompt");
        public string HelpButtonText => GetLocalizedText("HelpButton");
        public string HeaderDevicesText => GetLocalizedText("HeaderDevices");
        public string DeviceSelectionHint => GetLocalizedText("DeviceSelectionHint");
        public string RefreshDevicesButtonText => GetLocalizedText("RefreshDevicesButton");
        public string HeaderRefreshFontText => GetLocalizedText("HeaderRefreshFont");
        public string RefreshIntervalLabelText => GetLocalizedText("RefreshIntervalLabel");
        public string RefreshLogLabelText => GetLocalizedText("RefreshLogLabel");
        public string RefreshLogToggleButtonText => GetLocalizedText(IsRefreshLogEnabled ? "RefreshLogToggleHide" : "RefreshLogToggleShow");
        public string FontLabelText => GetLocalizedText("FontLabel");
        public string LanguageLabelText => GetLocalizedText("LanguageLabel");
        public string RunOnStartupLabelText => GetLocalizedText("RunOnStartupLabel");
        public string CloseButtonMinimizeLabelText => GetLocalizedText("CloseButtonMinimizeLabel");
        public string HeaderIconLayoutText => GetLocalizedText("HeaderIconLayout");
        public string IconOffsetXLabelText => GetLocalizedText("IconOffsetXLabel");
        public string IconOffsetYLabelText => GetLocalizedText("IconOffsetYLabel");
        public string IconFontSizeLabelText => GetLocalizedText("IconFontSizeLabel");
        public string HeaderActionsText => GetLocalizedText("HeaderActions");
        public string SaveButtonText => GetLocalizedText("SaveButton");
        public string ResetButtonText => GetLocalizedText("ResetButton");
        public string StatusHeaderText => GetLocalizedText("StatusHeader");

        private void LoadSettings()
        {
            settings = settingsManager.Load(settingsPath);
            if (!settings.iconLayoutMigrated)
            {
                settings.iconOffsetX -= IconLayoutDefaults.BaseOffsetX;
                settings.iconOffsetY -= IconLayoutDefaults.BaseOffsetY;
                settings.iconFontSize -= IconLayoutDefaults.LegacyFontSizeOffset;
                settings.iconLayoutMigrated = true;
                settingsManager.Save(settingsPath, settings);
            }
            settings.language = localizationService.NormalizeLanguageCode(settings.language);
            ApplySettingsToProperties(settings);
            UpdateStatus("StatusLoaded");
            NotifySettingsApplied();
        }

        private void ApplySettingsToProperties(Settings value)
        {
            isApplyingSettings = true;
            try
            {
                settings.language = localizationService.NormalizeLanguageCode(value.language);
                SelectedDevice = value.deviceName;
                EnsureSelectedDeviceVisible();
                RefreshInterval = Math.Max(1, value.refreshTimer);
                SelectedFontFamily = value.fontFamilyName;
                IconOffsetX = value.iconOffsetX;
                IconOffsetY = value.iconOffsetY;
                IconFontSize = value.iconFontSize;
                IsCloseButtonMinimize = value.isCloseBtnMinimize;
                RunOnStartup = value.runOnStartup;
                SelectedLanguage = Languages.FirstOrDefault(l => string.Equals(l.Code, settings.language, StringComparison.OrdinalIgnoreCase))
                                   ?? Languages.FirstOrDefault();
            }
            finally
            {
                isApplyingSettings = false;
            }
            NotifySettingsApplied();
        }

        private Settings CreateSettingsFromProperties()
        {
            settings.deviceName = SelectedDevice ?? settings.deviceName;
            settings.refreshTimer = Math.Max(1, RefreshInterval);
            settings.fontFamilyName = SelectedFontFamily ?? settings.fontFamilyName;
            settings.iconOffsetX = IconOffsetX;
            settings.iconOffsetY = IconOffsetY;
            settings.iconFontSize = IconFontSize;
            settings.isCloseBtnMinimize = IsCloseButtonMinimize;
            settings.runOnStartup = RunOnStartup;
            settings.language = SelectedLanguage?.Code ?? settings.language;
            return settings;
        }

        private void MigrateLegacySettingsIfNeeded()
        {
            try
            {
                if (!File.Exists(settingsPath) && File.Exists(legacySettingsPath))
                {
                    File.Copy(legacySettingsPath, settingsPath, overwrite: true);
                }
            }
            catch
            {
                // 忽略迁移失败，继续使用默认配置
            }
        }

        private async Task RefreshDevicesAsync()
        {
            try
            {
                IsBusy = true;
                UpdateStatus("StatusLoadingDevices");
                AvailableDevices.Clear();
                var devices = await bluetoothService.GetAllDevicesAsync();
                foreach (var device in devices.Where(d => !string.IsNullOrWhiteSpace(d)))
                {
                    AvailableDevices.Add(device);
                }
                if (AvailableDevices.Count == 0)
                {
                    UpdateStatus("StatusNoDevices");
                }
                else
                {
                    if (!string.IsNullOrEmpty(settings.deviceName) && AvailableDevices.Contains(settings.deviceName))
                    {
                        SelectedDevice = settings.deviceName;
                    }
                    UpdateStatus("StatusDevicesRefreshed");
                }
                EnsureSelectedDeviceVisible();
            }
            catch (Exception ex)
            {
                UpdateStatus("StatusRefreshFailed", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void SaveSettings()
        {
            try
            {
                CreateSettingsFromProperties();
                settingsManager.Save(settingsPath, settings);
                UpdateStartupRegistration();
                UpdateStatus("StatusSaved");
                NotifySettingsApplied(forceTrayRefresh: true);
            }
            catch (Exception ex)
            {
                UpdateStatus("StatusSaveFailed", ex.Message);
            }
        }

        private void ResetToDefault()
        {
            settings = new Settings();
            ApplySettingsToProperties(settings);
            UpdateStartupRegistration();
            UpdateStatus("StatusReset");
            NotifySettingsApplied(forceTrayRefresh: true);
        }

        private void LoadFonts()
        {
            FontFamilies.Clear();
            try
            {
                InstalledFontCollection fontCollection = new();
                foreach (var font in fontCollection.Families.Select(f => f.Name).Distinct().OrderBy(f => f))
                {
                    FontFamilies.Add(font);
                }
            }
            catch
            {
                FontFamilies.Add("Segoe UI");
            }
        }

        private void LoadLanguages()
        {
            Languages.Clear();
            Languages.Add(new LanguageOption("zh-CN", localizationService.GetText("LanguageOptionChinese", "zh-CN")));
            Languages.Add(new LanguageOption("en-US", localizationService.GetText("LanguageOptionEnglish", "en-US")));
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        private void NotifySettingsApplied(bool forceTrayRefresh = false) =>
            SettingsApplied?.Invoke(this, new SettingsAppliedEventArgs(settings, forceTrayRefresh));

        private void NotifyLocalizationChanged()
        {
            foreach (var propertyName in LocalizedPropertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void RaiseLayoutPreview()
        {
            if (isApplyingSettings)
            {
                return;
            }
            CreateSettingsFromProperties();
            LayoutPreviewRequested?.Invoke(this, EventArgs.Empty);
        }

        private void EnsureSelectedDeviceVisible()
        {
            var device = settings.deviceName;
            if (string.IsNullOrWhiteSpace(device))
            {
                return;
            }

            if (!AvailableDevices.Contains(device))
            {
                AvailableDevices.Add(device);
            }
        }

        private void UpdateStartupRegistration()
        {
            if (!startupRegistrationService.TryUpdateStartupRegistration(settings.runOnStartup, out var error))
            {
                UpdateStatus("StatusStartupFailed", error!);
            }
        }

        public void UpdateStatus(string key, params object[] args) =>
            StatusMessage = localizationService.Format(key, CurrentLanguageCode, args);

        public void AppendRefreshLog(string message)
        {
            if (!IsRefreshLogEnabled)
            {
                return;
            }
            refreshLogs.Insert(0, message);
            while (refreshLogs.Count > 30)
            {
                refreshLogs.RemoveAt(refreshLogs.Count - 1);
            }
            RefreshLogText = string.Join(Environment.NewLine, refreshLogs);
        }

        public string GetLocalizedText(string key, params object[] args) =>
            localizationService.Format(key, CurrentLanguageCode, args);
    }

    public record LanguageOption(string Code, string Display)
    {
        public override string ToString() => Display;
    }

    public class SettingsAppliedEventArgs : EventArgs
    {
        public SettingsAppliedEventArgs(Settings settings, bool forceTrayRefresh)
        {
            Settings = settings;
            ForceTrayRefresh = forceTrayRefresh;
        }

        public Settings Settings { get; }
        public bool ForceTrayRefresh { get; }
    }
}
