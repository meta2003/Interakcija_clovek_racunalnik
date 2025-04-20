// UrediAvtoWindow.xaml.cs - namenjen urejanju obstoječega oglasa

using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WPFNaloga1
{
    public partial class UrediAvtoWindow : Window
    {
        private Avto _trenutniAvto;
        private string _potDoSlike;

        public UrediAvtoWindow()
        {
            InitializeComponent();
        }

        public void NastaviAvto(Avto avto)
        {
            _trenutniAvto = avto;

            ZnamkaTextBox.Text = avto.Znamka;
            ModelTextBox.Text = avto.Model;
            LetoTextBox.Text = avto.Leto.ToString();
            CenaTextBox.Text = avto.Cena.ToString();
            ProstorninaTextBox.Text = avto.ProstorninaMotorja.ToString();

            foreach (ComboBoxItem item in GorivoComboBox.Items)
            {
                if (item.Content.ToString() == avto.Gorivo.ToString())
                {
                    GorivoComboBox.SelectedItem = item;
                    break;
                }
            }

            _potDoSlike = avto.Slika;
            if (!string.IsNullOrEmpty(_potDoSlike))
            {
                try
                {
                    SlikaImage.Source = new BitmapImage(new Uri(_potDoSlike, UriKind.Relative));
                }
                catch
                {
                    SlikaImage.Source = null;
                }
            }
            else
            {
                SlikaImage.Source = null;
            }
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

            _trenutniAvto.Znamka = ZnamkaTextBox.Text;
            _trenutniAvto.Model = ModelTextBox.Text;
            _trenutniAvto.Leto = leto;
            _trenutniAvto.Cena = cena;
            _trenutniAvto.ProstorninaMotorja = prostornina;
            _trenutniAvto.Gorivo = (Fuel)Enum.Parse(typeof(Fuel), ((ComboBoxItem)GorivoComboBox.SelectedItem).Content.ToString());
            _trenutniAvto.Slika = _potDoSlike ?? _trenutniAvto.Slika;
        }

        private void Preklici_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 
