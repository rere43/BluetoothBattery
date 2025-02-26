using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
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
        private static Form1 form1;

        /// <summary>
        /// 刷新数字的线程
        /// </summary>
        private Thread refreshThread;

        private string SettingsFile_Path = Application.StartupPath + "/settings.txt";
        private const int IconFontSize = 108;
        private const int iconPosDeltaX = -20;




        public Form1()
        {
            form1 = this;

            InitializeComponent();

            //窗体位置设置在右下角
            var width = Screen.PrimaryScreen.WorkingArea.Width;
            var height = Screen.PrimaryScreen.WorkingArea.Height;
            Location = new Point(width - form1.Size.Width, height - form1.Size.Height);

            //初始化字体下拉选项
            InstalledFontCollection fontCollection = new();
            foreach (var fontFamily in fontCollection.Families)
            {
                comboBox_FontFamily.Items.Add(fontFamily.Name);
            }

            //读取设置
            try
            {
                var settingsJson = File.ReadAllText(SettingsFile_Path);
                settings = JsonConvert.DeserializeObject<Settings>(settingsJson);
                if (settings.refreshTimer < 1000)
                    settings.refreshTimer *= 1000;
                font = CreateNewFont_FromSettings();
            }
            catch (Exception e)
            {
                settings = new Settings();
                font = CreateNewFont_FromSettings();
                MessageBox.Show(e.Message + $"\n使用默认配置", @"加载设置时出错");
            }
            RefreshFormUI();

            SaveSettings();

            //空白图标
            defaultIcon = new Icon(notifyIcon1.Icon, 128, 128);

            lastDateTime = DateTime.Now;

            async void task() => await RefreshIconRepeatly();
            refreshThread = new Thread(task);
            refreshThread.Start();

            //右键图标菜单→显示或隐藏主窗口
            contextMenuStrip1.Items[0].Click += (_, _) => { notifyIcon1_MouseDoubleClick(default, default); };
            //右键图标菜单→退出 以此方式退出,否则重复刷新的线程还会运行
            contextMenuStrip1.Items[1].Click += (_, _) => { System.Environment.Exit(0); };

            //关闭按钮 隐藏还是退出
            form1.Closing += (_, e) =>
            {
                if (checkBox_CloseBtnMinimize.Checked)
                {
                    e.Cancel = true;
                    form1.Hide();
                }
                else
                {
                    Environment.Exit(0);
                }
            };
        }

        private void SaveSettings()
        {
            File.WriteAllText(SettingsFile_Path, JsonConvert.SerializeObject(settings));
        }

        private Font CreateNewFont_FromSettings()
        {
            return new(settings.fontFamilyName, IconFontSize, GraphicsUnit.Pixel);
        }

        private async Task InitDeviceDropdown()
        {
            var deviceNameList = await GetAllDevices();
            comboBox_DeviceName.Items.AddRange(deviceNameList.Where(deviceName => !string.IsNullOrEmpty(deviceName))
                .Select(s => (object)s).ToArray());
            label_DeviceName.Text = @"设备名";
        }

        private async Task<IEnumerable<string>> GetAllDevices()
        {
/*  GetDevices() 返回样例
Status     Class           FriendlyName
------     -----           ------------
OK         Firmware        设备固件
Unknown    HIDClass        符合 HID 标准的用户控制设备
*/
            label_DeviceName.Text = @"正在获取设备...";
            var deviceList = GetDevices().Split(Environment.NewLine); //每一行为单个
            //2个或以上的空格  可能不稳妥
            Regex r = new Regex(@" {2,}");

            List<Task<Task<string>>> taskList = new();

            for (int index = 3; index < deviceList.Length; index++) //从第4行开始 看返回样例
            {
                string s = deviceList[index];
                s = r.Replace(s, "\t"); //将空格替换为制表符, 取第3个(设备名) 可能不稳妥
                if (s.Split("\t").Length > 2)
                {
                    var deviceName = s.Split("\t")[2];

                    async Task<string> function() =>
                        int.TryParse(await GetBattery(deviceName), out var _) ? deviceName : string.Empty;

                    Task<Task<string>> task = new(function);
                    task.Start();
                    taskList.Add(task);
                }
            }
            var taskResult = await Task.WhenAll(taskList);

            return taskResult.Select(r => r.Result);
        }



        private async Task Refresh_IconNumber()
        {
            //form1.Text = GetBattery(settings.deviceName) ;
            var backup = lastBattery;
            int.TryParse(await GetBattery(settings.deviceName), out lastBattery);
            if (backup - lastBattery > 5)
            {
                lastBattery = backup;
                return;
            }
            var bitmap = defaultIcon.ToBitmap();
            var graphics = Graphics.FromImage(bitmap);
            if (lastBattery == 100) lastBattery = 99;
            var color = lastBattery > 15 ? Color.White : Color.OrangeRed;
            graphics.DrawString(lastBattery.ToString(), font, new SolidBrush(color), iconPosDeltaX, 0);
            var icon = System.Drawing.Icon.FromHandle(bitmap.GetHicon());
            notifyIcon1.Icon = icon;
            RefreshTooltips();
            lastDateTime = DateTime.Now;
        }

        private async Task RefreshIconRepeatly()
        {
            while (true)
            {
                await Refresh_IconNumber();
                Thread.CurrentThread.Join(settings.refreshTimer);
            }
        }

        private void RefreshTooltips() => notifyIcon1_RefreshTooltips(null, null);

        /// <summary>
        /// 刷新右下角鼠标提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_RefreshTooltips(object sender, MouseEventArgs e)
        {
            var span = DateTime.Now - lastDateTime;
            notifyIcon1.Text =
                $@"{settings.deviceName}
电量: {lastBattery}
上次刷新: {span.TotalSeconds:##,###}秒前
间隔: {settings.refreshTimer / 1000:##,###}秒
字体: {font.Name}
双击显示/隐藏主界面";
        }

        public async Task<string> GetBattery(string deviceName)
        {
            //powershell获取电量的命令
            string command
                = $"-command Get-PnpDevice -Class 'Bluetooth' -friendlyname '{deviceName}'"
                  + @"| Get-PnpDeviceProperty -KeyName '{104EA319-6EE2-4701-BD47-8DDBF425BBE5} 2' "
                  + @"| select-Object -ExpandProperty Data";
            //pipeline.Commands.AddScript(command,false);
            //PowerShell ps = PowerShell.Create();
            //ps.AddScript(  command).Invoke();
            //pipeline.Invoke();
            //runspace.Close();

            var start = new ProcessStartInfo
            {
                FileName = FileName_PowerShell,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = command,
                CreateNoWindow = true
            };


            using var process = Process.Start(start);
            using var reader = process.StandardOutput;

            process.EnableRaisingEvents = true;
            notifyIcon1.Text = (settings.deviceName);
            return await reader.ReadToEndAsync();
        }


        /// <summary>
        /// 由powershell返回的设备名
        /// </summary>
        /// <returns></returns>
        private string GetDevices()
        {
/*  GetDevices() 返回样例
Status     Class           FriendlyName
------     -----           ------------
OK         Firmware        设备固件
Unknown    HIDClass        符合 HID 标准的用户控制设备
*/
            const string command = @"-command Get-PnpDevice -Class 'Bluetooth'";
            //pipeline.Commands.AddScript(command,false);
            //PowerShell ps = PowerShell.Create();
            //ps.AddScript(  command).Invoke();
            //pipeline.Invoke();
            //runspace.Close();

            var start = new ProcessStartInfo
            {
                FileName = FileName_PowerShell,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = command,
                CreateNoWindow = true
            };
            using var process = Process.Start(start);
            using var reader = process.StandardOutput;

            process.EnableRaisingEvents = true;

            return reader.ReadToEnd();
        }

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// 双击图标 显示或隐藏主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!form1.Visible || form1.WindowState is not FormWindowState.Normal)
            {
                form1.WindowState = FormWindowState.Normal;
                form1.Show();

                //窗口保持在屏幕内
                KeepFormInScreen();
            }
            else if (form1.Visible)
            {
                form1.Hide();
            }
        }

        private static void KeepFormInScreen()
        {
            var width = Screen.PrimaryScreen.WorkingArea.Width;
            var height = Screen.PrimaryScreen.WorkingArea.Height;
            form1.Location = new Point(Math.Clamp(form1.Location.X, 0, width - form1.Width),
                Math.Clamp(form1.Location.Y, 0, height - form1.Height));
        }

        private void notifyIcon1_MouseOver(object sender, MouseEventArgs e)
        {
            Refresh_IconNumber();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        /// <summary>
        /// 主界面设为系统当前前台窗口  (非前台窗口时: 例如点击了其他窗口,然后主界面被遮挡)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Visible)
            {
                SetForegroundWindow(Handle);
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

        private void textBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //无效 不是这样整 
            if (!RegexInteger(this.textBox2.Text))
            {
                textBox2.Text = "";
            }
        }

        public static bool RegexInteger(string IInteger)
        {
            Regex g = new Regex(@"^[0-9]\d*$");
            return g.IsMatch(IInteger);
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            //无效 不是这样整 
            if (!RegexInteger(this.textBox2.Text))
            {
                textBox2.Text = "";
            }
        }

        private void ConfirmBtn_Click(object sender, EventArgs _)
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
                Refresh_IconNumber();
            }
            catch (Exception e)
            {
                settings = backupSetting;
                Refresh_IconNumber();
                MessageBox.Show(e.Message + $"\n使用默认配置", @"加载设置时出错");
            }
            RefreshFormUI();
            RefreshTooltips();
            SaveSettings();
        }

        /// <summary>
        /// 刷新主窗口文本
        /// </summary>
        private void RefreshFormUI()
        {
            comboBox_DeviceName.Text = settings.deviceName;
            textBox2.Text = settings.refreshTimer.ToString();
            comboBox_FontFamily.Text = settings.fontFamilyName;
            checkBox_CloseBtnMinimize.Checked = settings.isCloseBtnMinimize;
        }

        private void SetToDefaultBtn_Click(object sender, EventArgs e)
        {
            settings = new Settings();
            font = CreateNewFont_FromSettings();
            Refresh_IconNumber();
            RefreshFormUI();
            RefreshTooltips();
            SaveSettings();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void refresh_Btn_Click(object sender, EventArgs e)
        {
            InitDeviceDropdown();
        }

        private void checkBox_CloseBtnMinimize_CheckedChanged(object sender, EventArgs e)
        {
            settings.isCloseBtnMinimize = checkBox_CloseBtnMinimize.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(@"1: 点击刷新, 并等待(选项1的标签变回 '设备名')
2: 第1个选项选择设备
2: 第2个选项设置刷新间隔, 不建议太快, 默认600秒
3: 点击保存 并刷新", "使用方法");
        }
    }

    public class Settings
    {
        public string deviceName = "MIIIW MECH-KB Pro";
        public int refreshTimer = Form1.DefaultRefreshTimer;
        public string fontFamilyName = "jb";
        public List<string> availableDevice = new();
        public bool isCloseBtnMinimize;
    }
}
