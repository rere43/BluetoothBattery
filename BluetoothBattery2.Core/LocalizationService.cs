using System;
using System.Collections.Generic;
using System.Linq;

namespace BluetoothBattery2.Core
{
    public class LocalizationService
    {
        public static readonly string[] SupportedLanguages = { "zh-CN", "en-US" };
        public const string DefaultLanguage = "zh-CN";

        private readonly Dictionary<string, (string zh, string en)> localizedTexts = new(StringComparer.OrdinalIgnoreCase)
        {
            // Buttons / labels
            ["LabelDeviceName_Default"] = ("设备名", "Device Name"),
            ["LabelDeviceName_Loading"] = ("正在获取设备...", "Loading devices..."),
            ["LabelRefreshInterval"] = ("刷新间隔 (秒)", "Refresh interval (s)"),
            ["LabelFontFamily"] = ("字体", "Font family"),
            ["ButtonSave"] = ("保存并刷新", "Save && refresh"),
            ["ButtonReset"] = ("恢复默认", "Reset to default"),
            ["ButtonRefreshDevices"] = ("刷新", "Reload"),
            ["CheckBoxCloseToTray"] = ("点击关闭按钮时最小化到托盘", "Minimize to tray when closing"),
            ["CheckBoxRunOnStartup"] = ("开机启动", "Run on startup"),
            ["LabelOffsetX"] = ("X 偏移", "X offset"),
            ["LabelOffsetY"] = ("Y 偏移", "Y offset"),
            ["LabelFontSize"] = ("字号偏移", "Font size offset"),
            ["ButtonApplyIconLayout"] = ("应用", "Apply"),
            ["MenuToggleWindow"] = ("显示或隐藏主窗口", "Show / hide main window"),
            ["MenuExit"] = ("退出", "Exit"),
            ["LabelLanguage"] = ("语言", "Language"),
            ["LabelHowToUse"] = ("使用说明?", "Help?"),
            ["LanguageOptionChinese"] = ("中文", "Chinese"),
            ["LanguageOptionEnglish"] = ("英文", "English"),

            // Status
            ["StatusReady"] = ("准备就绪", "Ready"),
            ["StatusLoaded"] = ("设置已加载", "Settings loaded"),
            ["StatusLoadingDevices"] = ("正在刷新设备列表...", "Refreshing device list..."),
            ["StatusNoDevices"] = ("未找到蓝牙设备", "No Bluetooth devices found"),
            ["StatusDevicesRefreshed"] = ("设备列表已刷新", "Device list refreshed"),
            ["StatusRefreshFailed"] = ("刷新失败: {0}", "Refresh failed: {0}"),
            ["StatusApplyPreview"] = ("已应用预览（WPF 版本暂未接入托盘图标）", "Preview applied (tray icon pending)"),
            ["StatusSaved"] = ("设置已保存", "Settings saved"),
            ["StatusSaveFailed"] = ("保存失败: {0}", "Save failed: {0}"),
            ["StatusReset"] = ("已恢复默认设置", "Settings reset to default"),
            ["StatusBatteryUpdated"] = ("{0:HH:mm:ss} 获取到电量 {1}%（耗时 {2:F1}s）", "Battery {1}% @ {0:HH:mm:ss} ({2:F1}s)"),
            ["StatusBatteryFailed"] = ("读取电量失败: {0}", "Failed to read battery: {0}"),
            ["StatusStartupFailed"] = ("更新开机启动失败: {0}", "Failed to update startup setting: {0}"),
            ["StatusIconCacheRebuilt"] = ("图标缓存已重建", "Icon cache rebuilt"),
            ["StatusIconCacheFailed"] = ("重建图标缓存失败: {0}", "Failed to rebuild icon cache: {0}"),
            ["StatusIconPreviewApplied"] = ("图标设置已应用，正在刷新缓存", "Icon layout applied, refreshing cache"),

            // Tooltips
            ["TooltipBatteryFormat"] = ("电量: {0}%", "Battery: {0}%"),
            ["TooltipLastRefreshFormat"] = ("上次刷新: {0}", "Last refresh: {0}"),
            ["TooltipIntervalFormat"] = ("间隔: {0}秒", "Interval: {0}s"),
            ["TooltipNotRefreshed"] = ("尚未刷新", "Not refreshed yet"),
            ["TooltipDeviceNotSelected"] = ("未选择设备", "No device selected"),
            ["TooltipToggleHint"] = ("双击显示/隐藏主界面", "Double-click to toggle window"),

            // UI labels
            ["HelpPrompt"] = ("需要帮助？", "Need help?"),
            ["HelpButton"] = ("查看", "View"),
            ["HeaderDevices"] = ("蓝牙设备", "Bluetooth Devices"),
            ["DeviceSelectionHint"] = ("请选择设备", "Select a device"),
            ["RefreshDevicesButton"] = ("刷新设备", "Refresh devices"),
            ["HeaderRefreshFont"] = ("刷新与字体", "Refresh & Font"),
            ["RefreshIntervalLabel"] = ("刷新间隔 (秒)", "Refresh interval (s)"),
            ["FontLabel"] = ("字体", "Font"),
            ["LanguageLabel"] = ("界面语言", "Language"),
            ["RunOnStartupLabel"] = ("开机启动", "Run at startup"),
            ["CloseButtonMinimizeLabel"] = ("关闭按钮最小化", "Minimize on close"),
            ["HeaderIconLayout"] = ("图标布局", "Icon layout"),
            ["IconOffsetXLabel"] = ("X 偏移", "X offset"),
            ["IconOffsetYLabel"] = ("Y 偏移", "Y offset"),
            ["IconFontSizeLabel"] = ("字号偏移", "Font size offset"),
            ["HeaderActions"] = ("操作", "Actions"),
            ["RefreshLogLabel"] = ("刷新日志", "Refresh log"),
            ["RefreshLogPlaceholder"] = ("尚无刷新记录", "No refresh logs yet"),
            ["RefreshLogStarting"] = ("{0:HH:mm:ss} 开始刷新，目标间隔 {1} 秒", "{0:HH:mm:ss} refresh started, target interval {1}s"),
            ["RefreshLogCompleted"] = ("{0:HH:mm:ss} 刷新完成，耗时 {1:F1} 秒", "{0:HH:mm:ss} refresh completed in {1:F1}s"),
            ["RefreshLogToggleShow"] = ("显示刷新日志", "Show refresh log"),
            ["RefreshLogToggleHide"] = ("关闭刷新日志", "Hide refresh log"),
            ["SaveButton"] = ("保存并刷新", "Save & refresh"),
            ["ResetButton"] = ("恢复默认", "Reset to default"),
            ["StatusHeader"] = ("实时状态", "Live status"),

            // Messages
            ["Message_LoadSettingsFallback"] = ("使用默认配置", "Default settings will be used."),
            ["Message_LoadSettingsTitle"] = ("加载设置时出错", "Failed to load settings"),
            ["Message_HowToUse"] = (
                string.Join(Environment.NewLine,
                    "1: 点击刷新, 并等待(选项1的标签变回 '设备名')",
                    "2: 第1个选项选择设备",
                    "3: 第2个选项设置刷新间隔, 不建议太快, 默认600秒",
                    "4: 点击保存 并刷新"),
                string.Join(Environment.NewLine,
                    "1: Click Reload and wait until the label shows 'Device Name' again.",
                    "2: Choose the device from the first dropdown.",
                    "3: Set an appropriate refresh interval (default 600 seconds).",
                    "4: Click Save && Refresh.")),
            ["Message_HowToUseTitle"] = ("使用方法", "How to use")
        };

        public string NormalizeLanguageCode(string? code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return DefaultLanguage;
            }

            return SupportedLanguages.FirstOrDefault(lang =>
                       string.Equals(lang, code, StringComparison.OrdinalIgnoreCase))
                   ?? DefaultLanguage;
        }

        public string GetText(string key, string? languageCode = null)
        {
            languageCode = NormalizeLanguageCode(languageCode);
            if (!localizedTexts.TryGetValue(key, out var tuple))
            {
                return key;
            }

            return string.Equals(languageCode, SupportedLanguages[1], StringComparison.OrdinalIgnoreCase)
                ? tuple.en
                : tuple.zh;
        }

        public string Format(string key, string? languageCode = null, params object[] args)
        {
            var text = GetText(key, languageCode);
            return string.Format(text, args);
        }
    }
}
