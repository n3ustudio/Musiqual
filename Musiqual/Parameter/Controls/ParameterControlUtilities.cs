using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Scrosser.Models;

namespace Musiqual.Parameter.Controls
{
    /// <summary>
    /// ParameterControl.xaml 的交互逻辑
    /// </summary>
    public partial class ParameterControl : UserControl
    {

        private void ResetControlState()
        {
            FrameDrag.Visibility = Visibility.Collapsed;
            _rectStartState = null;
            FrameDrag.Width = 0;
            _isMouseDown = false;
            if (IsMouseCaptured) ReleaseMouseCapture();
        }

        private void EditModeOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ResetControlState();
        }

        #region FrameRect

        private void DragRect(double x)
        {
            if (_rectStartState is null)
            {
                FrameDrag.Visibility = Visibility.Visible;
                _rectStartState = x;
                return;
            }

            FrameDrag.Margin = new Thickness(Math.Min(x, (double)_rectStartState), 0, 0, 0);
            FrameDrag.Width = Math.Abs(x - (double)_rectStartState);

        }

        #endregion

        #region Variables

        private bool _isMouseDown;

        private double? _rectStartState;

        private Models.Parameter _target;

        private KeyValuePair<int, double> _mousePosition;

        private bool _hitTarget;

        #endregion

        #region DataContext

        private string _topPos = "";

        public string TopPos
        {
            get => _topPos;
            set
            {
                _topPos = value;
                OnPropertyChanged();
            }
        }

        private string _centerPos = "";

        public string CenterPos
        {
            get => _centerPos;
            set
            {
                _centerPos = value;
                OnPropertyChanged();
            }
        }

        private string _bottomPos = "";

        public string BottomPos
        {
            get => _bottomPos;
            set
            {
                _bottomPos = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private void UpdatePos()
        {
            TopPos = Posit<double>.GetValueFromViewer(ActualHeight, VerticalScross,
                ActualHeight, ParameterData.VerticalTotal).Position.ToString("####");
            CenterPos = Posit<double>.GetValueFromViewer(ActualHeight / 2, VerticalScross,
                ActualHeight, ParameterData.VerticalTotal).Position.ToString("####");
            BottomPos = Posit<double>.GetValueFromViewer(0, VerticalScross,
                ActualHeight, ParameterData.VerticalTotal).Position.ToString("####");
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
