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

      // Setting the DataContext so all controls are bound to this Avto object.
        public void NastaviAvto(Avto avto)
        {
            _trenutniAvto = avto;
            this.DataContext = avto; // Set DataContext to Avto object.
        }

        // Open image dialog and update the Avto's image.
        private void SlikaImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Slike (*.jpeg;*.jpg;*.png)|*.jpeg;*.jpg;*.png";

            if (dlg.ShowDialog() == true)
            {
                _potDoSlike = dlg.FileName;
                SlikaImage.Source = new BitmapImage(new Uri(_potDoSlike));
                if (_trenutniAvto != null)
                {
                    _trenutniAvto.Slika = _potDoSlike; // Update image path in the Avto object.
                }
            }
        }

        // Saving the Avto object with validation.
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

            // Update the Avto object properties
            _trenutniAvto.Znamka = ZnamkaTextBox.Text;
            _trenutniAvto.Model = ModelTextBox.Text;
            _trenutniAvto.Leto = leto;
            _trenutniAvto.Cena = cena;
            _trenutniAvto.ProstorninaMotorja = prostornina;
            _trenutniAvto.Gorivo = (Fuel)Enum.Parse(typeof(Fuel), ((ComboBoxItem)GorivoComboBox.SelectedItem).Content.ToString());
            
            // If image path is not empty, save it.
            _trenutniAvto.Slika = _potDoSlike ?? _trenutniAvto.Slika;

            this.Close();
        }

        // Close the window without saving changes.
        private void Preklici_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }} 
