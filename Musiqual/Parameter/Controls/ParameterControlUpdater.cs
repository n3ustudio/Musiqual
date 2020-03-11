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
                parameter.HorizontalAlignment = HorizontalAlignment.Left;
                parameter.VerticalAlignment = VerticalAlignment.Top;
                parameter.Visibility = Visibility.Collapsed;
                FrameParameterContainer.Children.Add(parameter);
            }

            HorizontalScross.PropertyChanged += (o, args) => UpdateView();
            VerticalScross.PropertyChanged += (o, args) => UpdateView();
            UpdateView();
        }

        /// <summary>
        /// Update control view.
        /// </summary>
        private void UpdateView()
        {
            foreach (Models.Parameter parameter in ParameterData.ParameterList)
            {
                var (visibility, left) = parameter.Position.GetPosition(HorizontalScross, ActualWidth);
                if (visibility == Visibility.Collapsed)
                {
                    parameter.Visibility = Visibility.Collapsed;
                    continue;
                }
                parameter.Margin = new Thickness(
                    left, parameter.ViewTotal - parameter.Value, 0, 0);
            }
        }

        #endregion

    }

}
