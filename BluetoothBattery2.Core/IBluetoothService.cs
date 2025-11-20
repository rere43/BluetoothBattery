using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluetoothBattery2.Core
{
    public interface IBluetoothService
    {
        Task<IEnumerable<string>> GetAllDevicesAsync();
        Task<string> GetBatteryAsync(string deviceName);
    }
}
