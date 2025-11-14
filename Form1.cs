using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json;




namespace BluetoothBattery2
{
    public partial class Form1 : Form
    {
        public const int DefaultRefreshTimer = 600000;
        private const string FileName_PowerShell = "pwsh";
        private Font font;

        /// <summary>
        /// 空白图标, 每次刷新都在空白图标上写数字
        /// </summary>
        private readonly Icon defaultIcon;

        private DateTime lastDateTime;
        private int lastBattery;
        private Settings settings;
        private static Form1? form1;

        /// <summary>
        /// 刷新数字的线程
        /// </summary>
        private Thread refreshThread;

        private readonly string SettingsFile_Path = Path.Combine(Application.StartupPath, "settings.txt");
        private readonly string IconCacheFolder_Path = Path.Combine(Application.StartupPath, "iconcache");

        private enum UiLanguage
        {
            Chinese,
            English
        }

        private enum DeviceLabelState
        {
            Default,
            Loading
        }

        private static readonly string[] SupportedLanguages = { "zh-CN", "en-US" };

        private readonly Dictionary<string, (string zh, string en)> localizedTexts = new()
        {
            ["LabelDeviceName_Default"] = ("设备名", "Device Name"),
            ["LabelDeviceName_Loading"] = ("正在获取设备...", "Loading devices..."),
            ["LabelRefreshInterval"] = ("刷新间隔(毫秒),小于1千会×1000", "Refresh interval (ms). Values < 1000 will be multiplied by 1000."),
            ["LabelFontFamily"] = ("字体family", "Font family"),
            ["ButtonSave"] = ("保存并刷新", "Save && refresh"),
            ["ButtonReset"] = ("恢复默认", "Reset to default"),
            ["ButtonRefreshDevices"] = ("刷新", "Reload"),
            ["CheckBoxCloseToTray"] = ("点击关闭按钮时最小化到托盘", "Minimize to tray when the close button is clicked"),
            ["LabelOffsetX"] = ("X偏移", "X offset"),
            ["LabelOffsetY"] = ("Y偏移", "Y offset"),
            ["LabelFontSize"] = ("字号偏移", "Font size offset"),
            ["ButtonApplyIconLayout"] = ("应用", "Apply"),
            ["MenuToggleWindow"] = ("显示或隐藏主窗口", "Show / hide main window"),
            ["MenuExit"] = ("退出", "Exit"),
            ["LabelLanguage"] = ("语言", "Language"),
            ["LanguageOptionChinese"] = ("中文", "Chinese"),
            ["LanguageOptionEnglish"] = ("英文", "English"),
            ["Message_LoadSettingsFallback"] = ("使用默认配置", "Default settings will be used."),
            ["Message_LoadSettingsTitle"] = ("加载设置时出错", "Failed to load settings"),
            ["Message_CacheIconsError"] = ("缓存图标失败", "Failed to cache icons"),
            ["Message_CacheIconsTitle"] = ("缓存错误", "Cache error"),
            ["Message_ApplyLayoutQuestion"] = ("是否保存当前图标偏移和字号设置？\n是: 写入配置并重建图标缓存\n否: 仅预览本次效果", "Save current icon offsets and font size?\nYes: write to config and rebuild icon cache\nNo: preview only"),
            ["Message_ApplyLayoutTitle"] = ("保存图标布局设置", "Save icon layout"),
            ["Message_HowToUse"] = (@"1: 点击刷新, 并等待(选项1的标签变回 '设备名')\n2: 第1个选项选择设备\n2: 第2个选项设置刷新间隔, 不建议太快, 默认600秒\n3: 点击保存 并刷新", "1: Click Reload and wait until the label shows \"Device Name\" again.\n2: Choose the device from the first dropdown.\n2: Set the refresh interval (default 600 seconds) in the second input.\n3: Click Save && Refresh."),
            ["Message_HowToUseTitle"] = ("使用方法", "How to use"),
            ["TooltipBatteryFormat"] = ("电量: {0}", "Battery: {0}"),
            ["TooltipLastRefreshFormat"] = ("上次刷新: {0}秒前", "Last refresh: {0} seconds ago"),
            ["TooltipIntervalFormat"] = ("间隔: {0}秒", "Interval: {0} seconds"),
            ["TooltipFontFormat"] = ("字体: {0}", "Font: {0}"),
            ["TooltipToggleHint"] = ("双击显示/隐藏主界面", "Double-click to show/hide the main window")
        };

        private bool isUpdatingLanguageUi;
        private DeviceLabelState deviceLabelState = DeviceLabelState.Default;

        private class LanguageOption
        {
            public string Code { get; }
            public string Display { get; }

            public LanguageOption(string code, string display)
            {
                Code = code;
                Display = display;
            }

            public override string ToString() => Display;
        }

        private UiLanguage CurrentLanguage => string.Equals(settings?.language, SupportedLanguages[1],
            StringComparison.OrdinalIgnoreCase)
            ? UiLanguage.English
            : UiLanguage.Chinese;

        private static string NormalizeLanguageCode(string? code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return SupportedLanguages[0];
            }

            return SupportedLanguages.FirstOrDefault(lang =>
                string.Equals(lang, code, StringComparison.OrdinalIgnoreCase)) ?? SupportedLanguages[0];
        }

        private string GetLocalizedText(string key)
        {
            if (!localizedTexts.TryGetValue(key, out var value))
            {
                return key;
            }

            return CurrentLanguage == UiLanguage.English ? value.en : value.zh;
        }

        private string FormatLocalizedText(string key, params object[] args) =>
            string.Format(GetLocalizedText(key), args);

        private string GetLanguageDisplayByCode(string code) =>
            string.Equals(code, SupportedLanguages[1], StringComparison.OrdinalIgnoreCase)
                ? GetLocalizedText("LanguageOptionEnglish")
                : GetLocalizedText("LanguageOptionChinese");

        private void UpdateLanguageSelectorItems()
        {
            if (comboBox_Language == null)
            {
                return;
            }

            var selectedCode = settings?.language ?? SupportedLanguages[0];
            isUpdatingLanguageUi = true;
            comboBox_Language.BeginUpdate();
            comboBox_Language.Items.Clear();
            foreach (var code in SupportedLanguages)
            {
                comboBox_Language.Items.Add(new LanguageOption(code, GetLanguageDisplayByCode(code)));
            }

            var index = -1;
            for (var i = 0; i < comboBox_Language.Items.Count; i++)
            {
                if (comboBox_Language.Items[i] is LanguageOption option &&
                    string.Equals(option.Code, selectedCode, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }

            comboBox_Language.SelectedIndex = index >= 0 ? index : 0;
            comboBox_Language.EndUpdate();
            isUpdatingLanguageUi = false;
        }

        private void SetDeviceLabelState(DeviceLabelState state)
        {
            deviceLabelState = state;
            UpdateDeviceLabelText();
        }

        private void UpdateDeviceLabelText()
        {
            if (label_DeviceName == null)
            {
                return;
            }

            var key = deviceLabelState == DeviceLabelState.Loading
                ? "LabelDeviceName_Loading"
                : "LabelDeviceName_Default";
            label_DeviceName.Text = GetLocalizedText(key);
        }

        public Form1()
        {
            form1 = this;

            Directory.CreateDirectory(IconCacheFolder_Path);

            InitializeComponent();

            var width = Screen.PrimaryScreen.WorkingArea.Width;
            var height = Screen.PrimaryScreen.WorkingArea.Height;
            Location = new Point(width - Size.Width, height - Size.Height);

            InstalledFontCollection fontCollection = new();
            foreach (var fontFamily in fontCollection.Families)
            {
                comboBox_FontFamily.Items.Add(fontFamily.Name);
            }

            try
            {
                var settingsJson = File.ReadAllText(SettingsFile_Path);
                settings = JsonConvert.DeserializeObject<Settings>(settingsJson) ?? new Settings();
                if (settings.refreshTimer < 1000)
                    settings.refreshTimer *= 1000;
                settings.language = NormalizeLanguageCode(settings.language);
                font = CreateNewFont_FromSettings();
            }
            catch (Exception e)
            {
                settings = new Settings();
                settings.language = NormalizeLanguageCode(settings.language);
                font = CreateNewFont_FromSettings();
                MessageBox.Show(
                    $"{e.Message}\n{GetLocalizedText("Message_LoadSettingsFallback")}",
                    GetLocalizedText("Message_LoadSettingsTitle"));
            }
            settings.iconCacheFontFamilyName ??= string.Empty;
            settings.language = NormalizeLanguageCode(settings.language);
            RefreshFormUI();

            SaveSettings();

            defaultIcon = new Icon(notifyIcon1.Icon, 128, 128);

            lastDateTime = DateTime.Now;

            async void task() => await RefreshIconRepeatly();
            refreshThread = new Thread(task);
            refreshThread.Start();

            contextMenuStrip1.Items[0].Click += (_, _) => { notifyIcon1_MouseDoubleClick(default, default); };
            contextMenuStrip1.Items[1].Click += (_, _) => { System.Environment.Exit(0); };

            Closing += (_, e) =>
            {
                if (checkBox_CloseBtnMinimize.Checked)
                {
                    e.Cancel = true;
                    Hide();
                }
                else
                {
                    Environment.Exit(0);
                }
            };
        }

        private void SaveSettings()
        {
            settings.language = NormalizeLanguageCode(settings.language);
            File.WriteAllText(SettingsFile_Path, JsonConvert.SerializeObject(settings));
        }

        private Font CreateNewFont_FromSettings()
        {
            var baseFontSize = 108; 
            var actualFontSize = baseFontSize + settings.iconFontSize; 
            return new(settings.fontFamilyName, actualFontSize, GraphicsUnit.Pixel);
        }

        private async Task InitDeviceDropdown()
        {
            SetDeviceLabelState(DeviceLabelState.Loading);
            comboBox_DeviceName.Items.Clear();
            try
            {
                var deviceNameList = await GetAllDevices();
                comboBox_DeviceName.Items.AddRange(deviceNameList.Where(deviceName => !string.IsNullOrEmpty(deviceName))
                    .Select(s => (object)s).ToArray());
            }
            finally
            {
                SetDeviceLabelState(DeviceLabelState.Default);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private async void refresh_Btn_Click(object sender, EventArgs e)
        {
            await InitDeviceDropdown();
        }

        private void checkBox_CloseBtnMinimize_CheckedChanged(object sender, EventArgs e)
        {
            settings.isCloseBtnMinimize = checkBox_CloseBtnMinimize.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyLocalization();
        }

        private void textBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!RegexInteger(this.textBox2.Text))
            {
                e.Cancel = true;
                textBox2.Text = string.Empty;
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            if (!RegexInteger(this.textBox2.Text))
            {
                textBox2.Text = string.Empty;
            }
        }

        public static bool RegexInteger(string IInteger)
        {
            Regex g = new Regex(@"^[0-9]\d*$");
            return g.IsMatch(IInteger);
        }

        private async void ConfirmBtn_Click(object sender, EventArgs _)
        {
            var backupSetting = settings;
            try
            {
                settings.deviceName = comboBox_DeviceName.Text;
                settings.refreshTimer = int.Parse(textBox2.Text);
                if (settings.refreshTimer < 1000)
                    settings.refreshTimer *= 1000;
                settings.fontFamilyName = comboBox_FontFamily.Text;
                font = CreateNewFont_FromSettings();
                if (!string.Equals(settings.fontFamilyName, settings.iconCacheFontFamilyName,
                        StringComparison.OrdinalIgnoreCase))
                {
                    await CacheIconsForCurrentFontAsync();
                }
                await Refresh_IconNumber(true);
            }
            catch (Exception e)
            {
                settings = backupSetting;
                await Refresh_IconNumber();
                MessageBox.Show(
                    $"{e.Message}\n{GetLocalizedText("Message_LoadSettingsFallback")}",
                    GetLocalizedText("Message_LoadSettingsTitle"));
            }
            RefreshFormUI();
            SaveSettings();
        }

        private void RefreshFormUI()
        {
            comboBox_DeviceName.Text = settings.deviceName;
            textBox2.Text = settings.refreshTimer.ToString();
            comboBox_FontFamily.Text = settings.fontFamilyName;
            checkBox_CloseBtnMinimize.Checked = settings.isCloseBtnMinimize;
            numericUpDown_OffsetX.Value = settings.iconOffsetX;
            numericUpDown_OffsetY.Value = settings.iconOffsetY;
            numericUpDown_FontSize.Value = settings.iconFontSize;
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            UpdateLanguageSelectorItems();
            UpdateDeviceLabelText();

            label2.Text = GetLocalizedText("LabelRefreshInterval");
            label3.Text = GetLocalizedText("LabelFontFamily");
            button1.Text = GetLocalizedText("ButtonSave");
            button2.Text = GetLocalizedText("ButtonReset");
            refresh_Btn.Text = GetLocalizedText("ButtonRefreshDevices");
            checkBox_CloseBtnMinimize.Text = GetLocalizedText("CheckBoxCloseToTray");
            label_OffsetX.Text = GetLocalizedText("LabelOffsetX");
            label_OffsetY.Text = GetLocalizedText("LabelOffsetY");
            label_FontSize.Text = GetLocalizedText("LabelFontSize");
            button_ApplyIconLayout.Text = GetLocalizedText("ButtonApplyIconLayout");
            label_Language.Text = GetLocalizedText("LabelLanguage");

            显示或隐藏主窗口ToolStripMenuItem.Text = GetLocalizedText("MenuToggleWindow");
            exitToolStripMenuItem.Text = GetLocalizedText("MenuExit");
        }

        private async void SetToDefaultBtn_Click(object sender, EventArgs e)
        {
            settings = new Settings();
            settings.language = NormalizeLanguageCode(settings.language);
            font = CreateNewFont_FromSettings();
            await CacheIconsForCurrentFontAsync();
            await Refresh_IconNumber(true);
            RefreshFormUI();
            SaveSettings();
        }

        private void comboBox_Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingLanguageUi)
            {
                return;
            }

            if (comboBox_Language.SelectedItem is not LanguageOption option)
            {
                return;
            }

            settings.language = NormalizeLanguageCode(option.Code);
            SaveSettings();
            ApplyLocalization();
            RefreshTooltips();
        }

        /// <summary>
        /// 应用图标偏移与字号的按钮点击事件（需要配合 UI 控件）。
        /// </summary>
        private async void button_ApplyIconLayout_Click(object sender, EventArgs e)
        {
            // 这里假定你在 Designer 中添加了三个 NumericUpDown 控件
            // numericUpDown_OffsetX, numericUpDown_OffsetY, numericUpDown_FontSize

            var tempOffsetX = settings.iconOffsetX;
            var tempOffsetY = settings.iconOffsetY;
            var tempFontSize = settings.iconFontSize;

            try
            {
                // 从 UI 读取临时值
                settings.iconOffsetX = (int)numericUpDown_OffsetX.Value;
                settings.iconOffsetY = (int)numericUpDown_OffsetY.Value;
                settings.iconFontSize = (int)numericUpDown_FontSize.Value;

                // 更新字体并刷新一次图标预览
                font = CreateNewFont_FromSettings();
                await Refresh_IconNumber();

                var result = MessageBox.Show(
                    GetLocalizedText("Message_ApplyLayoutQuestion"),
                    GetLocalizedText("Message_ApplyLayoutTitle"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // 保存到配置并重建缓存
                    await CacheIconsForCurrentFontAsync();
                    SaveSettings();
                }
                else
                {
                    // 不保存，恢复原设置并刷新
                    settings.iconOffsetX = tempOffsetX;
                    settings.iconOffsetY = tempOffsetY;
                    settings.iconFontSize = tempFontSize;
                    font = CreateNewFont_FromSettings();
                    await Refresh_IconNumber(true);
                }
            }
            catch
            {
                // 出错时恢复旧值
                settings.iconOffsetX = tempOffsetX;
                settings.iconOffsetY = tempOffsetY;
                settings.iconFontSize = tempFontSize;
                font = CreateNewFont_FromSettings();
                await Refresh_IconNumber();
            }

            RefreshFormUI();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(
                GetLocalizedText("Message_HowToUse"),
                GetLocalizedText("Message_HowToUseTitle"));
        }

        /// <summary>
        /// X偏移数值改变事件
        /// </summary>
        private async void numericUpDown_OffsetX_ValueChanged(object sender, EventArgs e)
        {
            // 实时更新设置（不立即保存）
            settings.iconOffsetX = (int)numericUpDown_OffsetX.Value;
            // 可选：实时预览效果
            font = CreateNewFont_FromSettings();
            await Refresh_IconNumber(true);
        }

        /// <summary>
        /// Y偏移数值改变事件
        /// </summary>
        private async void numericUpDown_OffsetY_ValueChanged(object sender, EventArgs e)
        {
            // 实时更新设置（不立即保存）
            settings.iconOffsetY = (int)numericUpDown_OffsetY.Value;
            // 可选：实时预览效果
            font = CreateNewFont_FromSettings();
            await Refresh_IconNumber(true);
        }

        /// <summary>
        /// 字号偏移数值改变事件
        /// </summary>
        private async void numericUpDown_FontSize_ValueChanged(object sender, EventArgs e)
        {
            // 实时更新设置（不立即保存）
            settings.iconFontSize = (int)numericUpDown_FontSize.Value;
            // 可选：实时预览效果
            font = CreateNewFont_FromSettings();
            await Refresh_IconNumber(true);
        }
    }

    public class Settings
    {
        public string deviceName = "MIIIW MECH-KB Pro";
        public int refreshTimer = Form1.DefaultRefreshTimer;
        public string fontFamilyName = "jb";
        public List<string> availableDevice = new();
        public bool isCloseBtnMinimize;
        public string iconCacheFontFamilyName = string.Empty;
        public int iconOffsetX = -20;
        public int iconOffsetY = 0;
        public int iconFontSize = 0;
        public string language = "zh-CN";
    }
}
