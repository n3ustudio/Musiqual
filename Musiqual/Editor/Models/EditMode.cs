using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Musiqual.Editor.Models
{

    public class EditMode : INotifyPropertyChanged
    {

        public EditMode(EditModeEnum mode = 0)
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

        public static EditModeConverter EditModeConverter { get; } = new EditModeConverter();

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

    public class EditModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value != null && (EditModeEnum)value == (EditModeEnum)int.Parse(parameter.ToString());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && !(bool)value) return null;
            return (EditModeEnum)int.Parse(parameter.ToString());
        }
    }

}
