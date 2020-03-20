using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Musiqual.Parameter.Views;
using Musiqual.Playback;
using Scrosser.Models;

namespace Musiqual.Parameter.Controls
{

    /// <summary>
    /// ParameterControl.xaml 的交互逻辑
    /// </summary>
    public partial class ParameterControl : UserControl
    {

        #region UpdateView

        /// <summary>
        /// Load control view.
        /// </summary>
        /// <param name="sender">The control.</param>
        /// <param name="e">Null.</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            foreach (Models.Parameter parameter in ParameterData.ParameterList)
            {
                parameter.Margin = new Thickness(0);
                parameter.HorizontalAlignment = HorizontalAlignment.Left;
                parameter.VerticalAlignment = VerticalAlignment.Top;
                parameter.Visibility = Visibility.Collapsed;
                FrameParameterContainer.Children.Add(parameter);
            }

            EditMode.PropertyChanged += (o, args) =>
            {
                if (EditMode.Mode == EditModeEnum.Pencil || EditMode.Mode == EditModeEnum.Eraser)
                    Cursor = Cursors.Cross;
                else
                    Cursor = Cursors.Arrow;
            };

            PlaybackView.Current.PositionChanged += posit =>
            {
                if (AutoScrollView.Current.IsAutoScrollEnabled)
                {
                    Posit<int> conv = new Posit<int>(
                        ParameterData.HorizontalTotal,
                        (int)Math.Floor(posit.Position * ParameterData.HorizontalTotal / posit.Total),
                        0);
                    var (v, p) = conv.GetHorizontalPosition(HorizontalScross, ActualWidth, (d, i) => d - i);
                    if (v != Visibility.Visible)
                    {
                        //FrameTimeMark.Visibility = Visibility.Collapsed;
                        // TODO
                    }
                    else
                    {
                        FrameTimeMark.Visibility = Visibility.Visible;
                        FrameTimeMark.Margin = new Thickness(p, 0, 0, 0);
                        if (ActualWidth - p < 150 && HorizontalScross.Position + 1200 <= HorizontalScross.Total) HorizontalScross.Position += 1200;
                    }
                }
                else
                {
                    Posit<int> conv = new Posit<int>(
                        ParameterData.HorizontalTotal,
                        (int)Math.Floor(posit.Position * ParameterData.HorizontalTotal / posit.Total),
                        0);
                    var (v, p) = conv.GetHorizontalPosition(HorizontalScross, ActualWidth, (d, i) => d - i);
                    if (v != Visibility.Visible)
                        FrameTimeMark.Visibility = Visibility.Collapsed;
                    else
                    {
                        FrameTimeMark.Visibility = Visibility.Visible;
                        FrameTimeMark.Margin = new Thickness(p, 0, 0, 0);
                    }
                }
            };

            HorizontalScross.PropertyChanged += (o, args) => UpdateView();
            HorizontalScross.PropertyChanged += (o, args) =>
            {
                if (args.PropertyName == nameof(Scross.Zoom))
                    HorizontalScross.ViewportSize = ActualWidth / HorizontalScross.Zoom;
            };
            VerticalScross.PropertyChanged += (o, args) => UpdateView();
            VerticalScross.PropertyChanged += (o, args) =>
            {
                if (args.PropertyName == nameof(Scross.Zoom))
                    VerticalScross.ViewportSize = ActualHeight / VerticalScross.Zoom;
            };
            VerticalScross.PropertyChanged += (o, args) => UpdatePos();
            UpdatePos();
            UpdateView();
        }

        /// <summary>
        /// Unload control view.
        /// </summary>
        /// <param name="sender">The control.</param>
        /// <param name="e">Null.</param>
        private void ParameterControl_OnUnloaded(object sender, RoutedEventArgs e)
        {
            foreach (Models.Parameter parameter in ParameterData.ParameterList)
            {
                parameter.Margin = new Thickness(0);
                parameter.HorizontalAlignment = HorizontalAlignment.Left;
                parameter.VerticalAlignment = VerticalAlignment.Top;
                parameter.Visibility = Visibility.Collapsed;
                FrameParameterContainer.Children.Remove(parameter);
            }

        }

        /// <summary>
        /// Update control view.
        /// </summary>
        private void UpdateView()
        {
            ParameterData.CalcNextPosition(HorizontalScross, ActualWidth);
            foreach (Models.Parameter parameter in ParameterData.ParameterList)
            {
                var (vL, left) = parameter.Position.GetHorizontalPosition(HorizontalScross, ActualWidth, (d, i) => d - i);
                var (vT, top) = parameter.Value.GetVertitalPosition(VerticalScross, ActualHeight, (d, d1) => d - d1);
                if (vL == Visibility.Collapsed || vT == Visibility.Collapsed)
                {
                    parameter.Visibility = Visibility.Collapsed;
                    continue;
                }

                parameter.Visibility = Visibility.Visible;
                parameter.Margin = new Thickness(
                    left, top, 0, 0);
            }
        }

        #endregion

        #region UpdateTarget

        private void UpdateTargetAndData(double x, KeyValuePair<int, double> position)
        {
            UpdateTarget(position);
            UpdateData(x, position);
        }

        private void UpdateTarget(KeyValuePair<int, double> position)
        {
            Models.Parameter p = null;
            foreach (Models.Parameter parameter in ParameterData.ParameterList)
            {
                int pos = parameter.Position.Position;
                if (pos > position.Key) continue;
                if (p is null) p = parameter;
                else if (pos > p.Position.Position) p = parameter;
            }

            _target = p;
            if (_target is null) _hitTarget = false;
            else _hitTarget = position.Key == _target.Position.Position;
        }

        private void UpdateData(double x, KeyValuePair<int, double> position)
        {
            if (_target is null) return;

            if (!_isMouseDown)
            {
                // Idle
                // TODO
                return;
            }
            switch (EditMode.Mode)
            {
                case EditModeEnum.Pencil:
                {
                    if (_hitTarget)
                    {
                        _target.Value.Position = Posit<double>.GetValueFromViewer(position.Value, VerticalScross,
                            ActualHeight, ParameterData.VerticalTotal).Position;
                        UpdateView();
                    }
                    else
                    {
                        Models.Parameter parameter = new Models.Parameter(
                            new Posit<int>(ParameterData.HorizontalTotal, position.Key, 0),
                            Posit<double>.GetValueFromViewer(position.Value, VerticalScross, ActualHeight, ParameterData.VerticalTotal));
                        ParameterData.ParameterList.Add(parameter);
                        FrameParameterContainer.Children.Add(parameter);
                        UpdateView();
                    }
                    break;
                }
                case EditModeEnum.Eraser:
                {
                    if (_hitTarget)
                    {
                        FrameParameterContainer.Children.Remove(_target);
                        ParameterData.ParameterList.Remove(_target);
                        UpdateView();
                    }
                    break;
                }
                default: // Arrow
                {
                    DragRect(x);
                    break;
                }
            }
        }

        #endregion

    }

}
