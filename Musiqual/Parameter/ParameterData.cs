using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Scrosser.Models;

namespace Musiqual.Parameter
{

    /// <summary>
    /// The ParameterData base class.
    /// </summary>
    public class ParameterData : INotifyPropertyChanged
    {

        public ParameterData(
            int total = 0,
            bool isNatural = false,
            string name = "Undefined",
            List<double> parameterList = null,
            double tolerance = 0.0001,
            double viewTotal = 500,
            double viewMin = -250,
            double viewMax = 250)
        {
            IsNatural = isNatural;
            Name = name;
            Total = total;
            _viewTotal = viewTotal;
            _viewMin = viewMin;
            _viewMax = viewMax;
            if (parameterList is null) parameterList = new List<double>();

            #region Tolerance Combine

            ObservableCollection<Models.Parameter> collection = new ObservableCollection<Models.Parameter>();
            int index = 0;
            double prev = 0;
            foreach (double d in parameterList)
            {
                if (index == 0)
                {
                    prev = d;
                    collection.Add(new Models.Parameter(new Posit(total, index), d, viewTotal, viewMin, viewMax));
                    index++;
                    continue;
                }

                if (Math.Abs(d - prev) < tolerance)
                {
                    index++;
                    continue;
                }

                prev = d;
                collection.Add(new Models.Parameter(new Posit(total, index), d, viewTotal, viewMin, viewMax));
                index++;
            }
            ParameterList = collection;

            #endregion

            //CalcNextPosition();

        }

        #region DataContext

        public bool IsNatural { get; }

        public string Name { get; }

        public int Total { get; }

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
        
        private double _viewTotal;

        public double ViewTotal
        {
            get => _viewTotal;
            set
            {
                _viewTotal = value;
                OnPropertyChanged();
            }
        }

        private double _viewMin;

        public double ViewMin
        {
            get => _viewMin;
            set
            {
                _viewMin = value;
                OnPropertyChanged();
            }
        }

        private double _viewMax;

        public double ViewMax
        {
            get => _viewMax;
            set
            {
                _viewMax = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Utilities

        public void CalcNextPosition(Scross scross, double actualWidth)
        {

            // TODO: Remove the next line and check what's wrong when UpdateView() called.
            return;
            double value = actualWidth;
            bool visible = false;
            bool last = false;
            for (int i = ParameterList.Count - 1; i >= 0; i--)
            {
                Models.Parameter parameter = ParameterList[i];
                if (parameter is null) continue;
                var (v, x) = parameter.Position.GetPosition(scross, actualWidth);
                if (last)
                {
                    parameter.NextPosition = value;
                    break;
                }
                if (v == Visibility.Visible)
                {
                    visible = true;
                    parameter.NextPosition = value;
                }
                else
                {
                    if (visible) last = true;
                }
                value = x;
            }

        }

        #endregion

    }

}
