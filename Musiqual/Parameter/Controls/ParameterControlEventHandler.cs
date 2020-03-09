﻿using System;
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
                        VerticalScross.ScrollDelta();
                    else
                        VerticalScross.ScrollDelta(false);
                }
            }

            e.Handled = true;
        }

        private void ParameterControl_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (IsMouseCaptured) ReleaseMouseCapture();
                ResetControlState();
                e.Handled = true;
                return;
            }
        }

    }
}
