using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFNaloga1
{
    /// <summary>
    /// Interaction logic for DodajOglasWindow.xaml
    /// </summary>
    public partial class DodajOglasWindow : Window
    {
        public Avto NovAvto { get; private set; }
        private string _potDoSlike = null;

        public DodajOglasWindow()
        {
            InitializeComponent();
        }

        private void SlikaImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Slike (*.jpeg;*.jpg;*.png)|*.jpeg;*.jpg;*.png";

            if (dlg.ShowDialog() == true)
            {
                _potDoSlike = dlg.FileName;
                SlikaImage.Source = new BitmapImage(new Uri(_potDoSlike));
            }
        }

        private void Shrani_Click(object sender, RoutedEventArgs e)
        {
            // Preverjanje
            if (string.IsNullOrWhiteSpace(ZnamkaTextBox.Text) ||
                string.IsNullOrWhiteSpace(ModelTextBox.Text) ||
                !int.TryParse(LetoTextBox.Text, out int leto) ||
                !int.TryParse(CenaTextBox.Text, out int cena) ||
                !float.TryParse(ProstorninaTextBox.Text, out float prostornina) ||
                GorivoComboBox.SelectedItem == null)
            {
                MessageBox.Show("Prosim izpolnite vsa polja pravilno!", "Napaka", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Ustvari nov avto
            NovAvto = new Avto
            {
                Znamka = ZnamkaTextBox.Text,
                Model = ModelTextBox.Text,
                Leto = leto,
                Cena = cena,
                ProstorninaMotorja = prostornina,
                Gorivo = (Fuel)Enum.Parse(typeof(Fuel), ((ComboBoxItem)GorivoComboBox.SelectedItem).Content.ToString()),
                Slika = _potDoSlike
            };

            DialogResult = true; // zapri modalno okno
        }

        private void Preklici_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
