using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WPFNaloga1
{
    public class AvtoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Avto> Avtomobili { get; set; } = new ObservableCollection<Avto>();
        public ObservableCollection<Avto> FilterAvtomobili { get; set; } = new ObservableCollection<Avto>();

        public event Action<Avto> ZahtevajUrejanjeAvta;
        public ICommand DodajAvtoCommand { get; }
        public ICommand DodajAvtoStaticnoCommand { get; }
        public ICommand OdstraniAvtoCommand { get; }
        public ICommand IzhodCommand { get; }
        public ICommand UrediAvtoStaticnoCommand { get; }
        public ICommand UrediAvtoCommand { get; }
        public ICommand FiltrirajCommand { get; }

        private bool urediOkno = false;
        private UrediAvtoWindow urediWindow;

        private Avto _izbraniAvto;
        private string filterText;
        public ObservableCollection<Fuel> allFuels { get; set; }
        public Fuel _selectedFuel;
        public Fuel SelectedFuel
        {
            get => _selectedFuel;
            set
            {
                if (_selectedFuel != value)
                {
                    _selectedFuel = value;
                    OnPropertyChanged();
                    FilterCars();
                }
            }
        }

        public Avto IzbraniAvto
        {
            get => _izbraniAvto;
            set
            {
                _izbraniAvto = value;
                OnPropertyChanged();
            }
        }

        public string FilterText
        {
            get => filterText;
            set
            {
                SetProperty(ref filterText, value);
                FilterCars(); // Filter cars whenever the filter text changes
            }
        }

        public AvtoViewModel()
        {
            Avtomobili.Add(new Avto { Znamka = "VW", Model = "Golf", Leto = 2020, Slika = "/Slike/golf.jpg", Cena = 20000, ProstorninaMotorja = 2.0f });
            Avtomobili.Add(new Avto { Znamka = "Audi", Model = "A4", Leto = 2019, Slika = "/Slike/a4.jpg", Cena = 30000, ProstorninaMotorja = 2.0f });
            Avtomobili.Add(new Avto { Znamka = "BMW", Model = "X5", Leto = 2021, Slika = "/Slike/x5.jpg", Cena = 50000, ProstorninaMotorja = 3.0f });
            Avtomobili.Add(new Avto { Znamka = "Ford", Model = "Focus", Leto = 2018, Slika = "/Slike/focus.jpg", Cena = 15000, ProstorninaMotorja = 1.5f });
            allFuels = new ObservableCollection<Fuel>((Fuel[])Enum.GetValues(typeof(Fuel)));
            DodajAvtoStaticnoCommand = new RelayCommand(o => DodajAvtoStaticno());
            DodajAvtoCommand = new RelayCommand(o => DodajAvto());
            UrediAvtoStaticnoCommand = new RelayCommand(o => UrediStaticnoAvto(), o => IzbraniAvto != null);
            UrediAvtoCommand = new RelayCommand(o=> OdpriUrediAvto(), o => IzbraniAvto != null);
            OdstraniAvtoCommand = new RelayCommand(o => OdstraniAvto(), o => IzbraniAvto != null);
            IzhodCommand = new RelayCommand(o => IzhodIzAplikacije());
            FiltrirajCommand = new RelayCommand(o => FilterCars());
            FilterCars();
        }

        private void OdpriUrediAvto()
        {
            ZahtevajUrejanjeAvta?.Invoke(IzbraniAvto);
        }

      
        private void DodajAvto()
        {
            var dodajWindow = new DodajOglasWindow();
            if (dodajWindow.ShowDialog() == true)
            {
                Avto novAvto = dodajWindow.NovAvto;
                Avtomobili.Add(novAvto);
                FilterCars();// tvoj ObservableCollection<Avto>
            }
        }

        private void IzhodIzAplikacije()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void UrediStaticnoAvto()
        {
            if (IzbraniAvto != null)
            {
                IzbraniAvto.Model = "novo ime";
                if (IzbraniAvto.Cena < 10000)
                {
                    IzbraniAvto.Cena = 15000;
                }
                else
                {
                    IzbraniAvto.Cena = 7500;
                }
            }
        }

        private void DodajAvtoStaticno()
        {
            Avtomobili.Add(new Avto { Znamka = "VW", Model = "Golf", Leto = 2020, Slika = "/Slike/golf.jpg", Cena = 20000, ProstorninaMotorja = 2.0f });
            FilterCars();
        }

        private void OdstraniAvto()
        {
            if (IzbraniAvto != null)
                Avtomobili.Remove(IzbraniAvto);
            FilterCars();
        }

        private void FilterCars()
        {
            // Filter based on text
            var filteredList = Avtomobili.Where(a =>
                (string.IsNullOrEmpty(FilterText) || a.Znamka.Contains(FilterText, StringComparison.OrdinalIgnoreCase) || a.Model.Contains(FilterText, StringComparison.OrdinalIgnoreCase))
                && (SelectedFuel == null || a.Gorivo == SelectedFuel) // Add a filter for fuel type if needed
            ).ToList();

            // Clear FilterAvtomobili and add the filtered items
            FilterAvtomobili.Clear();
            foreach (var avto in filteredList)
            {
                FilterAvtomobili.Add(avto);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }
    }
}

