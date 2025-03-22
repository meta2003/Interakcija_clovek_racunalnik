using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFNaloga1
{
   public class Avto : INotifyPropertyChanged
    {
        private string _znamka;
        private string _model;
        private int _leto;
        private string _slika;

        public string Znamka { get => _znamka; set { _znamka = value; OnPropertyChanged(); } }
        public string Model { get => _model; set { _model = value; OnPropertyChanged(); } }
        public int Leto { get => _leto; set { _leto = value; OnPropertyChanged(); } }
        public string Slika { get => _slika; set { _slika = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
