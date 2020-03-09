using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Musiqual.Editor.Models
{

    public class EditMode : INotifyPropertyChanged
    {

        EditMode(EditModeEnum mode = 0)
        {
            Mode = mode;
        }

        private EditModeEnum _mode = 0;

        public EditModeEnum Mode
        {
            get => _mode;
            set
            {
                _mode = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum EditModeEnum
    {
        Arrow = 0,
        Pencil = 1,
        Eraser = 2,
        Scissors = 3,
        Glue = 4,
        Stretch = 5,
        Playback = 6
    }

}
