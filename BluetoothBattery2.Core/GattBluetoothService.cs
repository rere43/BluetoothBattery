using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;

namespace BluetoothBattery2.Core
{
    public class GattBluetoothService : IBluetoothService
    {
        // ✅ 保持设备连接状态
        private BluetoothLEDevice? _currentDevice;
        private string? _currentDeviceId;

        // ✅ 维护设备名称到ID的映射
        private readonly Dictionary<string, string> _deviceNameToId = new();

        public async Task<IEnumerable<string>> GetAllDevicesAsync()
        {
            try
            {
                // ✅ 修复1：使用正确的设备选择器（只查找已配对的BLE设备）
                var selector = BluetoothLEDevice.GetDeviceSelector();
                var devices = await DeviceInformation.FindAllAsync(selector);

                // 清理旧的映射
                _deviceNameToId.Clear();

                // 使用并发任务检查哪些设备有电池服务
                List<Task<(string name, string id)>> tasks = new();

                foreach (var device in devices)
                {
                    tasks.Add(CheckDeviceHasBatteryAsync(device));
                }

                var results = await Task.WhenAll(tasks);

                // 构建名称到ID的映射并返回设备名称列表
                var deviceNames = new List<string>();
                foreach (var (name, id) in results)
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        _deviceNameToId[name] = id; // 缓存名称到ID的映射
                        deviceNames.Add(name);
                    }
                }

                return deviceNames;
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
        }

        private async Task<(string name, string id)> CheckDeviceHasBatteryAsync(DeviceInformation deviceInfo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(deviceInfo.Name))
                    return (string.Empty, string.Empty);

                // ✅ 临时连接检查电池服务（这里使用using是可以的）
                using var device = await BluetoothLEDevice.FromIdAsync(deviceInfo.Id);
                if (device == null)
                    return (string.Empty, string.Empty);

                var servicesResult = await device.GetGattServicesForUuidAsync(GattServiceUuids.Battery);
                if (servicesResult.Status == GattCommunicationStatus.Success && servicesResult.Services.Count > 0)
                {
                    return (deviceInfo.Name, deviceInfo.Id); // 返回名称和ID
                }
            }
            catch
            {
                // 忽略单个设备的错误
            }

            return (string.Empty, string.Empty);
        }

        // ✅ 保持接口签名不变，使用deviceName
        public async Task<string> GetBatteryAsync(string deviceName)
        {
            try
            {
                // ✅ 从缓存中获取设备ID
                if (!_deviceNameToId.TryGetValue(deviceName, out var deviceId))
                {
                    // 如果缓存中没有，尝试重新扫描
                    await GetAllDevicesAsync();

                    if (!_deviceNameToId.TryGetValue(deviceName, out deviceId))
                    {
                        System.Diagnostics.Debug.WriteLine($"设备未找到: {deviceName}");
                        return string.Empty;
                    }
                }

                // ✅ 如果不是同一个设备或没有连接，重新连接
                if (_currentDevice == null || _currentDeviceId != deviceId)
                {
                    // 释放旧连接
                    _currentDevice?.Dispose();

                    // 连接到新设备并保持连接
                    _currentDevice = await BluetoothLEDevice.FromIdAsync(deviceId);
                    _currentDeviceId = deviceId;

                    if (_currentDevice == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"连接设备失败: {deviceName}");
                        return string.Empty;
                    }
                }

                // ✅ 使用已连接的设备读取电量
                var servicesResult = await _currentDevice.GetGattServicesForUuidAsync(GattServiceUuids.Battery);
                if (servicesResult.Status != GattCommunicationStatus.Success || servicesResult.Services.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine($"获取电池服务失败: {servicesResult.Status}");
                    return string.Empty;
                }

                // 不使用using，因为我们要保持服务引用有效
                var batteryService = servicesResult.Services[0];

                // 获取电池电量特性
                var characteristicsResult = await batteryService.GetCharacteristicsForUuidAsync(GattCharacteristicUuids.BatteryLevel);
                if (characteristicsResult.Status != GattCommunicationStatus.Success || characteristicsResult.Characteristics.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine($"获取电池特性失败: {characteristicsResult.Status}");
                    return string.Empty;
                }

                var batteryLevelCharacteristic = characteristicsResult.Characteristics[0];

                // 读取电量值
                var readResult = await batteryLevelCharacteristic.ReadValueAsync();
                if (readResult.Status != GattCommunicationStatus.Success)
                {
                    System.Diagnostics.Debug.WriteLine($"读取电量失败: {readResult.Status}");
                    return string.Empty;
                }

                // 解析数据(第一个字节即为电量百分比)
                var reader = DataReader.FromBuffer(readResult.Value);
                var batteryLevel = reader.ReadByte();

                System.Diagnostics.Debug.WriteLine($"成功获取电量: {deviceName} = {batteryLevel}%");
                return batteryLevel.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"获取电量异常: {deviceName}, {ex.Message}");
                return string.Empty;
            }
        }

        // ✅ 新增：添加断开连接方法（可选）
        public void Disconnect()
        {
            _currentDevice?.Dispose();
            _currentDevice = null;
            _currentDeviceId = null;
        }
    }
}
