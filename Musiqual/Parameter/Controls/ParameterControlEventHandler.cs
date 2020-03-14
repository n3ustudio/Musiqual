using System;
using System.Collections.Generic;
using System.Linq;
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
using Musiqual.Editor.Models;
using Scrosser.Models;

namespace Musiqual.Parameter.Controls
{
    /// <summary>
    /// ParameterControl.xaml 的交互逻辑
    /// </summary>
    public partial class ParameterControl : UserControl
    {

        private void ParameterControl_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) > 0)
                if ((Keyboard.Modifiers & ModifierKeys.Shift) > 0)
                    if (e.Delta > 0)
                        HorizontalScross.ZoomIn();
                    else
                        HorizontalScross.ZoomOut();
                else
                {
                    if (e.Delta > 0)
                        VerticalScross.ZoomIn();
                    else
                        VerticalScross.ZoomOut();
                }
            else
            {
                if ((Keyboard.Modifiers & ModifierKeys.Shift) > 0)
                    if (e.Delta > 0)
                        HorizontalScross.ScrollDelta();
                    else
                        HorizontalScross.ScrollDelta(false);
                else
                {
                    if (e.Delta > 0)
                        VerticalScross.ScrollDelta(false);
                    else
                        VerticalScross.ScrollDelta();
                }
            }
            e.Handled = true;
        }

        private void ParameterControl_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ResetControlState();
                e.Handled = true;
                return;
            }
        }

        private void ParameterControl_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isMouseDown) return;
            _isMouseDown = true;
        }

        private void ParameterControl_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Ignore
        }

        private void ParameterControl_OnMouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            KeyValuePair<int, double> newPosition = new KeyValuePair<int, double>(
                Posit<int>.GetPositFromViewer(position.X, HorizontalScross, ActualWidth, ParameterData.HorizontalTotal).Position,
                ActualHeight - position.Y);

            int posDelta = Math.Abs(_mousePosition.Key - newPosition.Key);
            double valueDelta = newPosition.Value - _mousePosition.Value;
            if (newPosition.Key > _mousePosition.Key)
                for (int i = _mousePosition.Key; i <= newPosition.Key; i++)
                    UpdateTargetAndData(position.X, new KeyValuePair<int, double>(
                        i,
                        _mousePosition.Value + valueDelta / posDelta * (i - _mousePosition.Key)));
            else if (newPosition.Key < _mousePosition.Key)
                for (int i = newPosition.Key; i <= _mousePosition.Key; i++)
                    UpdateTargetAndData(position.X, new KeyValuePair<int, double>(
                        i,
                        _mousePosition.Value - valueDelta / posDelta * (i - _mousePosition.Key)));
            else UpdateTargetAndData(position.X, newPosition);

            _mousePosition = newPosition;
        }
        
        private void ParameterControl_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isMouseDown) return;
            switch (EditMode.Mode)
            {
                case EditModeEnum.Pencil: // Break
                    break;
                case EditModeEnum.Eraser: // Break
                    break;
                default: // Select
                {
                    // TODO
                    break;
                }
            }
            ResetControlState();
        }

        private void ParameterControl_OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            ResetControlState();
        }

        private void ParameterControl_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            HorizontalScross.ViewportSize = ActualWidth / HorizontalScross.Zoom;
            VerticalScross.ViewportSize = ActualHeight / VerticalScross.Zoom;
            UpdateView();
        }
    }
}
