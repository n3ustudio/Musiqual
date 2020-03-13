using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using Scrosser.Models;

namespace Musiqual.Models
{

    public sealed class Parameter : MusiqualControl
    {

        public Parameter(Posit<int> position, Posit<double> value)
            : base(position)
        {
            _value = value;
        }

        #region DataContext

        private Posit<double> _value;

        public Posit<double> Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        private double _nextPosition;

        public double NextPosition
        {
            get => _nextPosition;
            set
            {
                _nextPosition = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        public new event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region View
        
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            _rect.Inflate(2.5, 2.5);
            EllipseGeometry ellipseGeometry = new EllipseGeometry
            {
                Center = new Point(_rect.Left + _rect.Width / 2.0, _rect.Top + _rect.Height / 2.0),
                RadiusX = _rect.Width / 2.0,
                RadiusY = _rect.Height / 2.0
            };
            drawingContext.DrawGeometry(new SolidColorBrush(Colors.White), null, ellipseGeometry);
        }

        private Rect _rect;

        #endregion

    }

}
