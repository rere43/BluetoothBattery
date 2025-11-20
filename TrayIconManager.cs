using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BluetoothBattery2.Core;

namespace BluetoothBattery2.Wpf
{
    public class TrayIconManager : IDisposable
    {
        private const int NotifyIconTextMaxLength = 127;
        private const int NotifyIconLineMaxLength = 40;
        private readonly MainViewModel viewModel;
        private readonly Window mainWindow;
        private readonly NotifyIcon notifyIcon;
        private readonly ContextMenuStrip contextMenu;
        private readonly IconService iconService = new();
        private BatteryMonitorService? batteryMonitor;
        private Settings? currentSettings;
        private Icon baseIcon;
        private Icon? currentIcon;
        private Font? currentFont;
        private readonly string iconCacheFolder = Path.Combine(AppContext.BaseDirectory, "iconcache");
        private DateTime lastRefreshTime = DateTime.MinValue;
        private int? lastPercentage;
        private bool disposed;

        public TrayIconManager(MainViewModel viewModel, Window mainWindow)
        {
            this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            this.mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));

            Directory.CreateDirectory(iconCacheFolder);

            baseIcon = LoadBaseIcon();
            notifyIcon = new NotifyIcon
            {
                Icon = baseIcon,
                Visible = true,
                Text = "Bluetooth Battery"
            };

            contextMenu = new ContextMenuStrip
            {
                Font = new Font("Microsoft YaHei UI", 10)
            };
            var toggleWindowItem = new ToolStripMenuItem(viewModel.GetLocalizedText("MenuToggleWindow"));
            toggleWindowItem.Click += (_, _) => ToggleMainWindow();
            var exitItem = new ToolStripMenuItem(viewModel.GetLocalizedText("MenuExit")) { ForeColor = Color.OrangeRed };
            exitItem.Click += (_, _) => System.Windows.Application.Current.Shutdown();
            contextMenu.Items.Add(toggleWindowItem);
            contextMenu.Items.Add(exitItem);
            notifyIcon.ContextMenuStrip = contextMenu;

            notifyIcon.MouseDoubleClick += (_, _) => ToggleMainWindow();
            notifyIcon.MouseMove += (_, _) => UpdateTooltipText();

            viewModel.SettingsApplied += ViewModelOnSettingsApplied;
            viewModel.LayoutPreviewRequested += ViewModelOnLayoutPreviewRequested;
            viewModel.PropertyChanged += ViewModelOnPropertyChanged;

            ApplySettings(viewModel.Settings, true);
        }

        private void ViewModelOnSettingsApplied(object? sender, SettingsAppliedEventArgs e)
        {
            ApplySettings(e.Settings, e.ForceTrayRefresh);
        }

        private Icon LoadBaseIcon()
        {
            var iconPath = Path.Combine(AppContext.BaseDirectory, "trayIcon.ico");
            return File.Exists(iconPath)
                ? new Icon(iconPath, 128, 128)
                : System.Drawing.SystemIcons.Information;
        }

        private void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.StatusMessage))
            {
                UpdateTooltipText();
            }
        }

        private void ApplySettings(Settings settings, bool forceRefresh)
        {
            currentSettings = settings;
            UpdateFont(settings);
            StartBatteryMonitor(settings, forceRefresh);
            _ = EnsureIconCacheAsync(settings, forceRefresh);
            UpdateTooltipText();
        }

        private void UpdateFont(Settings settings)
        {
            currentFont?.Dispose();
            var fontSize = Math.Max(12, 108 - 53 + settings.iconFontSize);
            currentFont = new Font(settings.fontFamilyName, fontSize, GraphicsUnit.Pixel);
        }

        private void StartBatteryMonitor(Settings settings, bool forceRefresh)
        {
            batteryMonitor?.Dispose();
            var bluetoothService = BluetoothServiceFactory.Create(settings.batteryMode);
            batteryMonitor = new BatteryMonitorService(bluetoothService, () => viewModel.SelectedDevice);
            batteryMonitor.RefreshStarting += (_, _) => System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                var intervalSeconds = Math.Max(1, settings.refreshTimer);
                viewModel.AppendRefreshLog(viewModel.GetLocalizedText("RefreshLogStarting", DateTime.Now, intervalSeconds));
            });
            batteryMonitor.BatteryUpdated += (_, args) => System.Windows.Application.Current.Dispatcher.Invoke(() => OnBatteryUpdated(args));
            batteryMonitor.BatteryUpdateFailed += (_, ex) => viewModel.UpdateStatus("StatusBatteryFailed", ex.Message);
            var intervalSeconds = Math.Max(1, settings.refreshTimer);
            batteryMonitor.Start(TimeSpan.FromSeconds(intervalSeconds));
            _ = batteryMonitor.RefreshAsync(forceRefresh);
        }

        private void OnBatteryUpdated(BatteryStatusChangedEventArgs args)
        {
            lastRefreshTime = args.Timestamp;
            lastPercentage = args.Percentage;
            viewModel.UpdateStatus("StatusBatteryUpdated", args.Timestamp, args.Percentage, args.Duration.TotalSeconds);
            viewModel.AppendRefreshLog(viewModel.GetLocalizedText("RefreshLogCompleted", DateTime.Now, args.Duration.TotalSeconds));
            UpdateIcon(args.Percentage);
            UpdateTooltipText(args.Percentage);
        }

        private void UpdateIcon(int percentage)
        {
            if (currentFont == null)
            {
                return;
            }

            if (currentSettings != null && iconService.TryGetCachedIcon(iconCacheFolder,
                    currentSettings.fontFamilyName, currentSettings.iconCacheFontFamilyName, percentage,
                    out var cachedIcon))
            {
                ApplyIconToTray(cachedIcon);
                cachedIcon.Dispose();
                return;
            }

            var newIcon = iconService.CreateIconFromFont(baseIcon, currentFont, percentage,
                GetEffectiveOffset(viewModel.IconOffsetX, IconLayoutDefaults.BaseOffsetX),
                GetEffectiveOffset(viewModel.IconOffsetY, IconLayoutDefaults.BaseOffsetY));
            ApplyIconToTray(newIcon);
            newIcon.Dispose();
        }

        private async Task EnsureIconCacheAsync(Settings settings, bool forceRebuild)
        {
            try
            {
                if (!Directory.Exists(iconCacheFolder))
                {
                    Directory.CreateDirectory(iconCacheFolder);
                }

                if (currentFont == null)
                {
                    UpdateFont(settings);
                }

                var fontChanged = !string.Equals(settings.fontFamilyName, settings.iconCacheFontFamilyName,
                    StringComparison.OrdinalIgnoreCase);

                if ((fontChanged || forceRebuild) && currentFont != null)
                {
                    await iconService.CacheIconsForFontAsync(baseIcon, currentFont, iconCacheFolder,
                        GetEffectiveOffset(settings.iconOffsetX, IconLayoutDefaults.BaseOffsetX),
                        GetEffectiveOffset(settings.iconOffsetY, IconLayoutDefaults.BaseOffsetY));
                    settings.iconCacheFontFamilyName = settings.fontFamilyName;
                    System.Windows.Application.Current.Dispatcher.Invoke(() => viewModel.UpdateStatus("StatusIconCacheRebuilt"));
                }
            }
            catch (Exception ex)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() => viewModel.UpdateStatus("StatusIconCacheFailed", ex.Message));
            }
        }

        private static int GetEffectiveOffset(int value, int baseValue) => baseValue + value;

        private void ApplyIconToTray(Icon icon)
        {
            var clone = (Icon)icon.Clone();
            notifyIcon.Icon = clone;
            currentIcon?.Dispose();
            currentIcon = clone;
        }

        private void ViewModelOnLayoutPreviewRequested(object? sender, EventArgs e)
        {
            if (currentSettings == null)
            {
                return;
            }

            UpdateFont(currentSettings);
            var percentage = lastPercentage ?? 100;
            UpdateIcon(percentage);
        }

        private void UpdateTooltipText(int? percentageOverride = null)
        {
            var percentageText = percentageOverride.HasValue
                ? viewModel.GetLocalizedText("TooltipBatteryFormat", percentageOverride.Value)
                : viewModel.StatusMessage;

            var span = lastRefreshTime == DateTime.MinValue
                ? viewModel.GetLocalizedText("TooltipNotRefreshed")
                : viewModel.GetLocalizedText("TooltipLastRefreshFormat",
                    $"{(DateTime.Now - lastRefreshTime).TotalSeconds:0}s");

            var deviceText = viewModel.SelectedDevice ?? viewModel.GetLocalizedText("TooltipDeviceNotSelected");
            deviceText = TruncateWithEllipsis(deviceText, NotifyIconLineMaxLength);

            var tooltip = string.Join("\n", new[]
            {
                deviceText,
                percentageText,
                span,
                viewModel.GetLocalizedText("TooltipIntervalFormat", Math.Max(1, viewModel.RefreshInterval)),
                viewModel.GetLocalizedText("TooltipToggleHint")
            });

            notifyIcon.Text = TruncateWithEllipsis(tooltip, NotifyIconTextMaxLength);
        }

        private static string TruncateWithEllipsis(string? value, int maxLength)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            {
                return value ?? string.Empty;
            }

            if (maxLength <= 1)
            {
                return value[..1];
            }

            return string.Concat(value.AsSpan(0, maxLength - 1), "…");
        }

        private void ToggleMainWindow()
        {
            if (mainWindow.WindowState == WindowState.Minimized || !mainWindow.IsVisible)
            {
                mainWindow.Show();
                mainWindow.WindowState = WindowState.Normal;
                mainWindow.Activate();
            }
            else
            {
                mainWindow.Hide();
            }
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            batteryMonitor?.Dispose();
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            contextMenu.Dispose();
            currentIcon?.Dispose();
            currentFont?.Dispose();
            baseIcon?.Dispose();
            viewModel.LayoutPreviewRequested -= ViewModelOnLayoutPreviewRequested;
            viewModel.SettingsApplied -= ViewModelOnSettingsApplied;
            viewModel.PropertyChanged -= ViewModelOnPropertyChanged;
        }
    }
}
