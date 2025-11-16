using System.IO;
using Newtonsoft.Json;

namespace BluetoothBattery2.Core
{
    public class SettingsManager
    {
        public Settings Load(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new Settings();
                }

                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<Settings>(json) ?? new Settings();
            }
            catch
            {
                return new Settings();
            }
        }

        public void Save(string filePath, Settings settings)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(filePath, json);
        }
    }
}
