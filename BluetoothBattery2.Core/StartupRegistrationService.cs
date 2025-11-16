using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace BluetoothBattery2.Core
{
    public class StartupRegistrationService
    {
        private const string StartupRegistryPath = @"Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        private const string StartupApprovedRegistryPath = @"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\StartupApproved\\Run";
        private const string StartupRegistryValueName = "BluetoothBattery2";
        private static readonly byte[] EnabledState = { 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private static readonly byte[] DisabledState = { 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        public bool TryUpdateStartupRegistration(bool enable, out string? errorMessage)
        {
            errorMessage = null;
            try
            {
                using var runKey = Registry.CurrentUser.OpenSubKey(StartupRegistryPath, writable: true) ??
                                   Registry.CurrentUser.CreateSubKey(StartupRegistryPath, writable: true);
                using var approvedKey = Registry.CurrentUser.OpenSubKey(StartupApprovedRegistryPath, writable: true) ??
                                         Registry.CurrentUser.CreateSubKey(StartupApprovedRegistryPath, writable: true);

                if (runKey == null || approvedKey == null)
                {
                    throw new InvalidOperationException("无法访问注册表项。");
                }

                var executablePath = Environment.ProcessPath ?? Process.GetCurrentProcess().MainModule?.FileName;
                if (string.IsNullOrWhiteSpace(executablePath))
                {
                    throw new InvalidOperationException("无法获取当前进程路径。");
                }

                runKey.SetValue(StartupRegistryValueName, $"\"{executablePath}\"");
                var state = enable ? EnabledState : DisabledState;
                approvedKey.SetValue(StartupRegistryValueName, state, RegistryValueKind.Binary);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
