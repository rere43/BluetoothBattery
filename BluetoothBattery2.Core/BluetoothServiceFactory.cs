namespace BluetoothBattery2.Core
{
    public static class BluetoothServiceFactory
    {
        public static IBluetoothService Create(string mode)
        {
            return mode switch
            {
                "Gatt" => new GattBluetoothService(),
                _ => new PowerShellBluetoothService()
            };
        }
    }
}
