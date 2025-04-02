using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPFNaloga1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Avto izbraniAvto)
            {
                MessageBox.Show($"Izbran avto: {izbraniAvto.Znamka} {izbraniAvto.Model}", "Podrobnosti", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool invert = parameter != null && parameter.ToString().Equals("Invert", StringComparison.OrdinalIgnoreCase);
            bool isNull = value == null;

            return (isNull ^ invert) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    public class PriceLessThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int price)
            {
                return price < 10000; // You can change the threshold here
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}