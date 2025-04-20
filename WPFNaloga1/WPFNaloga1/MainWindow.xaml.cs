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
        private UrediAvtoWindow urediWindow;

        public MainWindow()
        {
            InitializeComponent();

            if (DataContext is AvtoViewModel vm)
            {
                vm.ZahtevajUrejanjeAvta += OdpriAliPosodobiUrediOkno;
            }
        }
        private void OdpriAliPosodobiUrediOkno(Avto avto)
        {
            if (avto == null)
                return;

            if (urediWindow != null)
            {
                urediWindow.NastaviAvto(avto);
                urediWindow.Focus();
                return;
            }

            urediWindow = new UrediAvtoWindow();
            urediWindow.Owner = this;
            urediWindow.NastaviAvto(avto);
            urediWindow.Closed += (s, e) => urediWindow = null;
            urediWindow.Show();
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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is AvtoViewModel vm && vm.IzbraniAvto != null)
            {
                // Če je urejevalno okno že odprto, ga posodobimo
                if (urediWindow != null)
                {
                    urediWindow.NastaviAvto(vm.IzbraniAvto);
                    urediWindow.Focus();
                }
            }
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