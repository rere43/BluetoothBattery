using System;
using System.Windows.Input;

namespace BluetoothBattery2.Wpf
{
    public class RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null) : ICommand
    {
        private readonly Action<object?> executeAction = execute ?? throw new ArgumentNullException(nameof(execute));
        private readonly Func<object?, bool>? canExecuteFunc = canExecute;

        public bool CanExecute(object? parameter) => canExecuteFunc?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => executeAction(parameter);

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
