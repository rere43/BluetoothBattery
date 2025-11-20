using System.Collections.Generic;

namespace BluetoothBattery2.Core
{
    public class Settings
    {
        public const int DefaultRefreshTimer = 600;

        public string deviceName = "MIIIW MECH-KB Pro";
        public int refreshTimer = DefaultRefreshTimer;
        public string fontFamilyName = "jb";
        public List<string> availableDevice = new();
        public bool isCloseBtnMinimize;
        public string iconCacheFontFamilyName = string.Empty;
        public int iconOffsetX = 0;
        public int iconOffsetY = 0;
        public int iconFontSize = 0;
        public string language = "zh-CN";
        public bool runOnStartup;
        public bool iconLayoutMigrated = true;
        public string batteryMode = "PowerShell"; // PowerShell 或 Gatt
    }
}
