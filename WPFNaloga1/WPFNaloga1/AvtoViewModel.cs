using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFNaloga1
{
    public class AvtoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Avto> Avtomobili { get; set; } = new ObservableCollection<Avto>();
        public ICommand DodajAvtoCommand { get; }
        public ICommand OdstraniAvtoCommand { get; }
        private Avto _izbraniAvto;

        public Avto IzbraniAvto
        {
            get => _izbraniAvto;
            set { _izbraniAvto = value; OnPropertyChanged(); }
        }

        public AvtoViewModel()
        {
            DodajAvtoCommand = new RelayCommand(o => DodajAvto());
            OdstraniAvtoCommand = new RelayCommand(o => OdstraniAvto(), o => IzbraniAvto != null);
        }

        private void DodajAvto()
        {
            Avtomobili.Add(new Avto { Znamka = "VW", Model = "Golf", Leto = 2020, Slika = "golf.jpg" });
        }

        private void OdstraniAvto()
        {
            if (IzbraniAvto != null)
                Avtomobili.Remove(IzbraniAvto);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
