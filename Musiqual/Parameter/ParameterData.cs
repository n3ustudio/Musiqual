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
    /// The ParameterData interface.
    /// </summary>
    public interface IParameterData : INotifyPropertyChanged
    {

        bool IsNatural { get; }

    }

    /// <summary>
    /// The ParameterData base class.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    public abstract class ParameterData<T> : IParameterData
    {

        public bool IsNatural { get; }

        private ObservableCollection<T> _parameterList = new ObservableCollection<T>();

        public ObservableCollection<T> ParameterList
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

    /// <summary>
    /// The positive/negative parameter data.
    /// </summary>
    public abstract class NormalParameterData<T> : ParameterData<T>
    {

        public new bool IsNatural => false;

    }

    /// <summary>
    /// The positive parameter data.
    /// </summary>
    public abstract class NaturalParameterData<T> : ParameterData<T>
    {

        public new bool IsNatural => true;

    }

    /// <summary>
    /// The placeholder parameter data.
    /// NO NOT USE THIS CLASS AS PARAMETER UNLESS YOU KNOW WHAT YOU ARE DOING.
    /// </summary>
    public sealed class PlaceHolderParameterData : NormalParameterData<double>
    {

    }

}
