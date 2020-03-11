using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Scrosser.Models;

namespace Musiqual.Models
{

    public abstract class MusiqualControl : Control, INotifyPropertyChanged
    {

        public MusiqualControl(
            Posit position,
            double viewTotal,
            double viewMin,
            double viewMax)
        {
            _position = position;
            _viewTotal = viewTotal;
            _viewMin = viewMin;
            _viewMax = viewMax;
        }

        #region DataContext

        private Posit _position;

        public Posit Position
        {
            get => _position;
            set
            {
                _position = value;
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

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }

}
