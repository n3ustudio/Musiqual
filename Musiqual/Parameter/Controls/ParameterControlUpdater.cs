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
                var (vL, left) = parameter.Position.GetPosition(HorizontalScross, ActualWidth, (d, i) => d - i);
                var (vT, top) = parameter.Value.GetPosition(VerticalScross, ActualHeight, (d, d1) => d - d1);
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

        private void UpdateTarget(Point position)
        {
            Models.Parameter p = null;
            foreach (Models.Parameter parameter in ParameterData.ParameterList)
            {
                double left = parameter.Margin.Left;
                if (left > position.X) continue;
                if (p is null) p = parameter;
                else if (left < p.Margin.Left) p = parameter;
            }

            _target = p;
            _mousePosition = Posit<int>.GetPositFromViewer(position.X, HorizontalScross, ActualWidth, ParameterData.HorizontalTotal).Position;
            if (_target is null) _hitTarget = false;
            else  _hitTarget = _mousePosition == _target.Position.Position;
        }

        #endregion

    }

}
