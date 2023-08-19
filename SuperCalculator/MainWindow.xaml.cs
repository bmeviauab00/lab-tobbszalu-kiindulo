using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SuperCalculator.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SuperCalculator
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly DataFifo _fifo = new DataFifo();

        public MainWindow()
        {
            this.InitializeComponent();
            param1TextBox.Text = 12.34.ToString();
            param2TextBox.Text = 56.78.ToString();

            AddKeyboardAcceleratorToChangeTheme();
        }

        private void AddKeyboardAcceleratorToChangeTheme()
        {
            // A bit of extra fun (not related to threading): change theme with ctrl+T key combination
            rootPanel.RequestedTheme = Application.Current.RequestedTheme == ApplicationTheme.Light ? ElementTheme.Light : ElementTheme.Dark;
            var accelerator = new KeyboardAccelerator()
            {
                Modifiers = VirtualKeyModifiers.Control,
                Key = VirtualKey.T,
            };
            accelerator.Invoked += (UIElement, args) =>
            {
                rootPanel.RequestedTheme = rootPanel.RequestedTheme == ElementTheme.Light
                ? rootPanel.RequestedTheme = ElementTheme.Dark
                : ElementTheme.Light;
                args.Handled = true;
            };
            rootPanel.KeyboardAcceleratorPlacementMode = KeyboardAcceleratorPlacementMode.Hidden;
            rootPanel.KeyboardAccelerators.Add(accelerator);
        }

        private void ShowResult(double[] parameters, double result)
        {
            var item = new ListBoxItem()
            {
                Content = $"{parameters[0]} #  {parameters[1]} = {result}"
            };
            resultListBox.Items.Add(item);
            resultListBox.ScrollIntoView(item);
        }

        private void CalculateResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(param1TextBox.Text, out var p1) && double.TryParse(param2TextBox.Text, out var p2))
            {
                var parameters = new double[] { p1, p2 };

                // TODO Call Algorithms.dll
            }
            else
                DisplayInvalidElementDialog();
        }

        async void DisplayInvalidElementDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                XamlRoot = rootPanel.XamlRoot,
                Title = "Super Calculator",
                Content = "Invalid parameter!",
                CloseButtonText = "Ok",
            };

            await noWifiDialog.ShowAsync();
        }

    }
}
