using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Threading.Tasks;

namespace BluetoothBattery2.Core
{
    public class IconService
    {
        public Icon CreateIconFromFont(Icon baseIcon, Font font, int number, int offsetX, int offsetY)
        {
            using var bitmap = new Bitmap(baseIcon.Width, baseIcon.Height);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            var color = number > 15 ? Color.White : Color.Orange;
            using var brush = new SolidBrush(color);
            var finalOffsetX = IconLayoutDefaults.BaseOffsetX + offsetX;
            var finalOffsetY = IconLayoutDefaults.BaseOffsetY + offsetY;
            graphics.DrawString(number.ToString(), font, brush, finalOffsetX, finalOffsetY);
            var iconHandle = bitmap.GetHicon();
            var icon = (Icon)Icon.FromHandle(iconHandle).Clone();
            DestroyIcon(iconHandle);
            return icon;
        }

        public async Task CacheIconsForFontAsync(Icon baseIcon, Font font, string cacheFolder, int offsetX, int offsetY)
        {
            Directory.CreateDirectory(cacheFolder);
            await Task.Run(() =>
            {
                var parallelOptions = new ParallelOptions
                {
                    MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount / 2)
                };

                Parallel.For(1, 100, parallelOptions, number =>
                {
                    using var icon = CreateIconFromFont(baseIcon, font, number, offsetX, offsetY);
                    var cachePath = GetCacheFilePath(cacheFolder, number);
                    using var fileStream = new FileStream(cachePath, FileMode.Create, FileAccess.Write, FileShare.None);
                    icon.Save(fileStream);
                });
            });
        }

        public bool TryGetCachedIcon(string cacheFolder, string fontFamilyName, string iconCacheFontFamilyName, int number, out Icon icon)
        {
            icon = default!;
            if (number is < 1 or > 99)
            {
                return false;
            }

            if (!string.Equals(fontFamilyName, iconCacheFontFamilyName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var iconPath = GetCacheFilePath(cacheFolder, number);
            if (!File.Exists(iconPath))
            {
                return false;
            }

            using var fileStream = new FileStream(iconPath, FileMode.Open, FileAccess.Read);
            icon = new Icon(fileStream);
            return true;
        }

        public string GetCacheFilePath(string cacheFolder, int number) =>
            Path.Combine(cacheFolder, $"battery_{number:D2}.ico");

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "DestroyIcon")]
        private static extern bool DestroyIcon(IntPtr hIcon);
    }
}
