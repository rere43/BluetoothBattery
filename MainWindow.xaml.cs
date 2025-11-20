using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BluetoothBattery2.Core;
using MessageBox = System.Windows.MessageBox;

namespace BluetoothBattery2.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainViewModel viewModel;
    private readonly TrayIconManager trayIconManager;

    public MainWindow()
    {
        InitializeComponent();
        viewModel = new MainViewModel();
        DataContext = viewModel;
        PositionWindowBottomRight();
        Loaded += (_, _) => PositionWindowBottomRight();
        trayIconManager = new TrayIconManager(viewModel, this);
        Closing += OnClosing;
    }

    private void PositionWindowBottomRight()
    {
        var workArea = SystemParameters.WorkArea;
        var windowWidth = ActualWidth > 0 ? ActualWidth : Width;
        var windowHeight = ActualHeight > 0 ? ActualHeight : Height;
        Left = workArea.Right - windowWidth - 3;
        Top = workArea.Bottom - windowHeight - 3;
    }

    private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var textBox = sender as System.Windows.Controls.TextBox;
        var isSigned = textBox?.Tag?.ToString() == "signed";
        var pattern = isSigned ? "^-?[0-9]*$" : "^[0-9]*$";
        var prospectiveText = textBox?.Text?.Insert(textBox.CaretIndex, e.Text) ?? e.Text;
        e.Handled = !Regex.IsMatch(prospectiveText, pattern);
    }

    private void WindowRoot_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    private void NumericTextBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (sender is not System.Windows.Controls.TextBox textBox)
        {
            return;
        }

        var delta = e.Delta > 0 ? 1 : -1;
        AdjustNumericTextBox(textBox, delta);
        e.Handled = true;
    }

    private void NumericSpinnerButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not System.Windows.Controls.Button button)
        {
            return;
        }

        if (button.Tag is not System.Windows.Controls.TextBox textBox)
        {
            return;
        }

        if (!int.TryParse(button.CommandParameter?.ToString(), out var delta))
        {
            delta = 1;
        }

        AdjustNumericTextBox(textBox, delta);
    }

    private static void AdjustNumericTextBox(System.Windows.Controls.TextBox textBox, int delta)
    {
        var isSigned = textBox.Tag?.ToString() == "signed";
        if (!int.TryParse(textBox.Text, out var value))
        {
            value = 0;
        }

        var newValue = value + delta;
        if (!isSigned && newValue < 0)
        {
            newValue = 0;
        }

        textBox.Text = newValue.ToString();
        textBox.CaretIndex = textBox.Text.Length;
    }

    private void HelpButton_Click(object sender, RoutedEventArgs e)
    {
        var helpText = viewModel.GetHelpText();
        var title = viewModel.GetLocalizedText("Message_HowToUseTitle");

        System.Windows.MessageBox.Show(
            helpText,
            title,
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (viewModel.IsCloseButtonMinimize)
        {
            e.Cancel = true;
            Hide();
            return;
        }

        trayIconManager.Dispose();
        System.Windows.Application.Current.Shutdown();
    }

    private void ComboBoxContentBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not System.Windows.Controls.Border border)
        {
            return;
        }

        if (border.TemplatedParent is System.Windows.Controls.ComboBox comboBox)
        {
            comboBox.IsDropDownOpen = true;
            e.Handled = true;
        }
    }
}