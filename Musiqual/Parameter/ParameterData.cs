using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Musiqual.Parameter
{

    /// <summary>
    /// The ParameterData base class.
    /// </summary>
    public class ParameterData : INotifyPropertyChanged
    {

        public ParameterData(
            bool isNatural = false,
            string name = "Undefined",
            ObservableCollection<double> parameterList = null)
        {
            IsNatural = isNatural;
            Name = name;
            ParameterList = parameterList;
        }

        public bool IsNatural { get; }

        public string Name { get; }

        public bool Rendered { get; set; } = false;

        private ObservableCollection<double> _parameterList = new ObservableCollection<double>();

        public ObservableCollection<double> ParameterList
        {
            get => _parameterList;
            set
            {
                _parameterList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
