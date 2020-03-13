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

        protected MusiqualControl(Posit<int> position)
        {
            _position = position;
        }

        #region DataContext

        private Posit<int> _position;

        public Posit<int> Position
        {
            get => _position;
            set
            {
                _position = value;
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
