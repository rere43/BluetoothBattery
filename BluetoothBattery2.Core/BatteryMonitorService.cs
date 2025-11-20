using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BluetoothBattery2.Core
{
    public class BatteryStatusChangedEventArgs : EventArgs
    {
        public BatteryStatusChangedEventArgs(int percentage, DateTime timestamp, TimeSpan duration)
        {
            Percentage = percentage;
            Timestamp = timestamp;
            Duration = duration;
        }

        public int Percentage { get; }
        public DateTime Timestamp { get; }
        public TimeSpan Duration { get; }
    }

    public class BatteryMonitorService : IDisposable
    {
        private readonly IBluetoothService bluetoothService;
        private readonly Func<string?> deviceSelector;
        private Timer? refreshTimer;
        private TimeSpan refreshInterval;
        private int lastKnownPercentage = -1;
        private bool isDisposed;
        private readonly SemaphoreSlim refreshLock = new(1, 1);
        private readonly string logFilePath = Path.Combine(AppContext.BaseDirectory, "battery_monitor.log");

        public event EventHandler? RefreshStarting;
        public event EventHandler<BatteryStatusChangedEventArgs>? BatteryUpdated;
        public event EventHandler<Exception>? BatteryUpdateFailed;

        public BatteryMonitorService(IBluetoothService bluetoothService, Func<string?> deviceSelector)
        {
            this.bluetoothService = bluetoothService ?? throw new ArgumentNullException(nameof(bluetoothService));
            this.deviceSelector = deviceSelector ?? throw new ArgumentNullException(nameof(deviceSelector));
        }

        public void Start(TimeSpan interval)
        {
            refreshInterval = interval <= TimeSpan.Zero ? TimeSpan.FromMinutes(5) : interval;
            refreshTimer?.Dispose();
            refreshTimer = new Timer(async _ => await RefreshAsync(), null, TimeSpan.Zero, refreshInterval);
        }

        public void Stop()
        {
            refreshTimer?.Dispose();
            refreshTimer = null;
        }

        public async Task RefreshAsync(bool forceNotify = false)
        {
            var deviceName = deviceSelector();
            if (string.IsNullOrWhiteSpace(deviceName))
            {
                return;
            }

            RefreshStarting?.Invoke(this, EventArgs.Empty);

            await refreshLock.WaitAsync().ConfigureAwait(false);
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var raw = await bluetoothService.GetBatteryAsync(deviceName).ConfigureAwait(false);
                stopwatch.Stop();
                if (!int.TryParse(raw?.Trim(), out var percentage))
                {
                    throw new InvalidOperationException($"无法解析电量数据: {raw}");
                }

                if (percentage == 100)
                {
                    percentage = 99;
                }

                if (forceNotify || percentage != lastKnownPercentage)
                {
                    lastKnownPercentage = percentage;
                    BatteryUpdated?.Invoke(this, new BatteryStatusChangedEventArgs(percentage, DateTime.Now, stopwatch.Elapsed));
                }
                else
                {
                    // 即使电量未变化，也要触发事件以记录刷新日志
                    BatteryUpdated?.Invoke(this, new BatteryStatusChangedEventArgs(percentage, DateTime.Now, stopwatch.Elapsed));
                }
            }
            catch (Exception ex)
            {
                BatteryUpdateFailed?.Invoke(this, ex);
                Log($"RefreshAsync failed: {ex}");
            }
            finally
            {
                try
                {
                    refreshLock.Release();
                }
                catch (ObjectDisposedException ex)
                {
                    Log($"Attempted to release disposed semaphore: {ex}");
                }
            }
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;
            Stop();
            refreshLock.Dispose();
            Log("BatteryMonitorService disposed");
        }

        private void Log(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, $"{DateTime.Now:O} {message}{Environment.NewLine}");
            }
            catch
            {
                // ignored
            }
        }
    }
}
