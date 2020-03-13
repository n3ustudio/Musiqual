﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using Scrosser.Models;

namespace Musiqual.Models
{

    public sealed class Parameter : MusiqualControl
    {

        public Parameter(
            Posit position,
            double value,
            double viewTotal,
            double viewMin,
            double viewMax)
            : base(position, viewTotal, viewMin, viewMax)
        {
            _value = value;
        }

        #region DataContext

        private double _value;

        public double Value
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
            //if (!(_dc is null)) OnRender(_dc);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region View
        
        protected override void OnRender(DrawingContext drawingContext)
        {
            //if (_dc is null) _dc = drawingContext;
            base.OnRender(drawingContext);
            double num = ViewTotal - 5.0;
            _rect.X = 0.0;
            _rect.Y = num - (Value - ViewMin) * (num / (ViewMax - ViewMin)) + 2.5;
            _rect.Width = 0.0;
            _rect.Height = 0.0;
            _rect.Inflate(2.5, 2.5);
            EllipseGeometry ellipseGeometry = new EllipseGeometry
            {
                Center = new Point(_rect.Left + _rect.Width / 2.0, _rect.Top + _rect.Height / 2.0),
                RadiusX = _rect.Width / 2.0,
                RadiusY = _rect.Height / 2.0
            };
            drawingContext.DrawGeometry(new SolidColorBrush(Colors.White), null, ellipseGeometry);
            //drawingContext.DrawRectangle(new SolidColorBrush(Colors.Blue), null,
            //    new Rect(new Point(Position.Position, Value), new Point(Position.Position + 10, Value + 10)));
        }

        private Rect _rect;

        //private DrawingContext _dc;

        #endregion

    }

}
