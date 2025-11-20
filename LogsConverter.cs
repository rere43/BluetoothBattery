using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace BluetoothBattery2.Wpf
{
    public class LogsConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is IEnumerable<string> logs)
            {
                return string.Join("\n", logs);
            }
            return string.Empty;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
