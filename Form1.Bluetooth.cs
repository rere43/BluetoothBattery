using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BluetoothBattery2
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 异步获取所有可用设备名称列表。
        /// </summary>
        private async Task<IEnumerable<string>> GetAllDevices()
        {
            /*  GetDevices() 返回样例
            Status     Class           FriendlyName
            ------     -----           ------------
            OK         Firmware        设备固件
            Unknown    HIDClass        符合 HID 标准的用户控制设备
            */
            label_DeviceName.Text = "正在获取设备...";
            var deviceList = GetDevices().Split(Environment.NewLine); //每一行为单个
            //2个或以上的空格  可能不稳妥
            Regex r = new Regex(" {2,}");

            List<Task<Task<string>>> taskList = new();

            for (int index = 3; index < deviceList.Length; index++) //从第4行开始 看返回样例
            {
                string s = deviceList[index];
                s = r.Replace(s, "\t"); //将空格替换为制表符, 取第3个(设备名) 可能不稳妥
                if (s.Split('\t').Length > 2)
                {
                    var deviceName = s.Split('\t')[2];

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

        /// <summary>
        /// 异步通过 PowerShell 获取指定设备电量字符串。
        /// </summary>
        public async Task<string> GetBattery(string deviceName)
        {
            //powershell获取电量的命令
            string command
                = $"-command Get-PnpDevice -Class 'Bluetooth' -friendlyname '{deviceName}'"
                  + "| Get-PnpDeviceProperty -KeyName '{104EA319-6EE2-4701-BD47-8DDBF425BBE5} 2' "
                  + "| select-Object -ExpandProperty Data";

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
            notifyIcon1.Text = settings.deviceName;
            return await reader.ReadToEndAsync();
        }

        /// <summary>
        /// 调用 PowerShell 获取设备列表原始文本。
        /// </summary>
        private string GetDevices()
        {
            const string command = "-command Get-PnpDevice -Class 'Bluetooth'";

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
    }
}
