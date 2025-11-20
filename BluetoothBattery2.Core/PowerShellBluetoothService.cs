using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BluetoothBattery2.Core
{
    public class PowerShellBluetoothService : IBluetoothService
    {
        private const string PowerShellExecutable = "pwsh";

        public async Task<IEnumerable<string>> GetAllDevicesAsync()
        {
            var deviceList = GetDevices().Split(Environment.NewLine);
            Regex r = new(" {2,}");
            List<Task<Task<string>>> taskList = new();

            for (int index = 3; index < deviceList.Length; index++)
            {
                string s = deviceList[index];
                s = r.Replace(s, "\t");
                if (s.Split('\t').Length > 2)
                {
                    var deviceName = s.Split('\t')[2];

                    async Task<string> function() =>
                        int.TryParse(await GetBatteryAsync(deviceName), out _) ? deviceName : string.Empty;

                    Task<Task<string>> task = new(function);
                    task.Start();
                    taskList.Add(task);
                }
            }

            var taskResult = await Task.WhenAll(taskList);
            return taskResult.Select(r => r.Result);
        }

        public async Task<string> GetBatteryAsync(string deviceName)
        {
            string command
                = $"-command Get-PnpDevice -Class 'Bluetooth' -friendlyname '{deviceName}'"
                  + "| Get-PnpDeviceProperty -KeyName '{104EA319-6EE2-4701-BD47-8DDBF425BBE5} 2' "
                  + "| select-Object -ExpandProperty Data";

            var start = new ProcessStartInfo
            {
                FileName = PowerShellExecutable,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = command,
                CreateNoWindow = true
            };

            using var process = Process.Start(start);
            using var reader = process!.StandardOutput;
            process.EnableRaisingEvents = true;
            return await reader.ReadToEndAsync();
        }

        private string GetDevices()
        {
            const string command = "-command Get-PnpDevice -Class 'Bluetooth'";

            var start = new ProcessStartInfo
            {
                FileName = PowerShellExecutable,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = command,
                CreateNoWindow = true
            };

            using var process = Process.Start(start);
            using var reader = process!.StandardOutput;
            process.EnableRaisingEvents = true;
            return reader.ReadToEnd();
        }
    }
}
