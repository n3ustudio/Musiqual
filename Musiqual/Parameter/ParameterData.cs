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
            List<double> parameterList = null,
            double tolerance = 0.1)
        {
            IsNatural = isNatural;
            Name = name;
            Total = parameterList.Count;
            ObservableCollection<Models.Parameter> collection = new ObservableCollection<Models.Parameter>();
            int index = 0;
            double prev = 0;
            foreach (double d in parameterList)
            {
                if (index == 0)
                {
                    prev = d;
                    collection.Add(new Models.Parameter(index, d));
                    index++;
                    continue;
                }

                if (Math.Abs(d - prev) < tolerance)
                {
                    index++;
                    continue;
                }

                prev = d;
                collection.Add(new Models.Parameter(index, d));
                index++;
            }
            ParameterList = collection;
        }

        public bool IsNatural { get; }

        public string Name { get; }

        public int Total { get; }

        public bool Rendered { get; set; } = false;

        private ObservableCollection<Models.Parameter> _parameterList = new ObservableCollection<Models.Parameter>();

        public ObservableCollection<Models.Parameter> ParameterList
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
