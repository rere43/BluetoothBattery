using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BluetoothBattery2
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "DestroyIcon")]
        private static extern bool DestroyIcon(IntPtr hIcon);

        /// <summary>
        /// 根据数字在空白图标上绘制电量文本并生成图标。
        /// </summary>
        private Icon CreateIconFromFont(int number)
        {
            using var bitmap = defaultIcon.ToBitmap();
            using var graphics = Graphics.FromImage(bitmap);
            var color = number > 15 ? Color.White : Color.Orange;
            using var brush = new SolidBrush(color);
            graphics.DrawString(number.ToString(), font, brush, settings.iconOffsetX, settings.iconOffsetY);
            var iconHandle = bitmap.GetHicon();
            var icon = (Icon)Icon.FromHandle(iconHandle).Clone();
            DestroyIcon(iconHandle);
            return icon;
        }

        /// <summary>
        /// 获取指定电量对应的缓存图标文件路径。
        /// </summary>
        private string GetCacheFilePath(int number) =>
            Path.Combine(IconCacheFolder_Path, $"battery_{number:D2}.ico");

        /// <summary>
        /// 尝试从磁盘缓存中加载指定电量的图标。
        /// </summary>
        private bool TryGetCachedIcon(int number, out Icon icon)
        {
            icon = default;
            if (number is < 1 or > 99)
            {
                return false;
            }

            if (!string.Equals(settings.fontFamilyName, settings.iconCacheFontFamilyName,
                    StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var iconPath = GetCacheFilePath(number);
            if (!File.Exists(iconPath))
            {
                return false;
            }

            using var fileStream = new FileStream(iconPath, FileMode.Open, FileAccess.Read);
            icon = new Icon(fileStream);
            return true;
        }

        /// <summary>
        /// 异步为当前字体生成 1-99 的缓存图标文件。
        /// </summary>
        private async Task CacheIconsForCurrentFontAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    Directory.CreateDirectory(IconCacheFolder_Path);

                    Parallel.For(1, 100, number =>
                    {
                        using var icon = CreateIconFromFont(number);
                        var cachePath = GetCacheFilePath(number);
                        using var fileStream = new FileStream(cachePath, FileMode.Create, FileAccess.Write, FileShare.None);
                        icon.Save(fileStream);
                    });

                    settings.iconCacheFontFamilyName = settings.fontFamilyName;
                });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n缓存图标失败", "缓存错误");
            }
        }

        /// <summary>
        /// 异步刷新托盘图标上的电量以及提示文本。
        /// </summary>
        private async Task Refresh_IconNumber(bool forceRecreate = false)
        {
            var backup = lastBattery;
            int.TryParse(await GetBattery(settings.deviceName), out lastBattery);
            if (backup - lastBattery > 5)
            {
                lastBattery = backup;
                return;
            }
            if (lastBattery == 100) lastBattery = 99;
            Icon icon;
            if (!forceRecreate && TryGetCachedIcon(lastBattery, out var cachedIcon))
            {
                icon = cachedIcon;
            }
            else
            {
                icon = CreateIconFromFont(lastBattery);
            }
            notifyIcon1.Icon = icon;
            RefreshTooltips();
            lastDateTime = DateTime.Now;
        }

        /// <summary>
        /// 在后台循环刷新托盘图标电量。
        /// </summary>
        private async Task RefreshIconRepeatly()
        {
            while (true)
            {
                await Refresh_IconNumber();
                Thread.CurrentThread.Join(settings.refreshTimer);
            }
        }

        /// <summary>
        /// 刷新托盘图标的提示文本。
        /// </summary>
        private void RefreshTooltips() => notifyIcon1_RefreshTooltips(null, null);

        /// <summary>
        /// 刷新右下角鼠标提示。
        /// </summary>
        private void notifyIcon1_RefreshTooltips(object sender, MouseEventArgs e)
        {
            var span = DateTime.Now - lastDateTime;
            notifyIcon1.Text =
                $"{settings.deviceName}\n电量: {lastBattery}\n上次刷新: {span.TotalSeconds:##,###}秒前\n间隔: {settings.refreshTimer / 1000:##,###}秒\n字体: {font.Name}\n双击显示/隐藏主界面";
        }

        /// <summary>
        /// 双击托盘图标，显示或隐藏主窗口。
        /// </summary>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!Visible || WindowState is not FormWindowState.Normal)
            {
                WindowState = FormWindowState.Normal;
                Show();
                KeepFormInScreen();
            }
            else if (Visible)
            {
                Hide();
            }
        }

        /// <summary>
        /// 保证窗口位置在屏幕可见范围内。
        /// </summary>
        private static void KeepFormInScreen()
        {
            if (form1 == null) return;
            var width = Screen.PrimaryScreen.WorkingArea.Width;
            var height = Screen.PrimaryScreen.WorkingArea.Height;
            form1.Location = new Point(Math.Clamp(form1.Location.X, 0, width - form1.Width),
                Math.Clamp(form1.Location.Y, 0, height - form1.Height));
        }

        /// <summary>
        /// 鼠标移动到托盘图标时刷新一次电量。
        /// </summary>
        private void notifyIcon1_MouseOver(object sender, MouseEventArgs e)
        {
            Refresh_IconNumber();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        /// <summary>
        /// 主界面设为系统当前前台窗口。
        /// </summary>
        private void NotifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Visible)
            {
                SetForegroundWindow(Handle);
            }
        }
    }
}
