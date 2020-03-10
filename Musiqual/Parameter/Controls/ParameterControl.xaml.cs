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
using Scrosser.Models;

namespace Musiqual.Parameter.Controls
{
    /// <summary>
    /// ParameterControl.xaml 的交互逻辑
    /// </summary>
    public partial class ParameterControl : UserControl
    {

        public ParameterControl()
        {
            InitializeComponent();
        }

        #region DependencyProperty

        public static readonly DependencyProperty ParameterDataProperty = DependencyProperty.Register(
            "ParameterData", typeof(IParameterData), typeof(ParameterControl), new PropertyMetadata(new NormalParameterData()));

        public IParameterData ParameterData
        {
            get => (IParameterData)GetValue(ParameterDataProperty);
            set => SetValue(ParameterDataProperty, value);
        }

        public static readonly DependencyProperty VerticalScrossProperty = DependencyProperty.Register(
            "VerticalScross", typeof(Scross), typeof(ParameterControl), new PropertyMetadata(default(Scross)));

        public Scross VerticalScross
        {
            get => (Scross)GetValue(VerticalScrossProperty);
            set => SetValue(VerticalScrossProperty, value);
        }

        public static readonly DependencyProperty HorizontalScrossProperty = DependencyProperty.Register(
            "HorizontalScross", typeof(Scross), typeof(ParameterControl), new PropertyMetadata(default(Scross)));

        public Scross HorizontalScross
        {
            get => (Scross)GetValue(HorizontalScrossProperty);
            set => SetValue(HorizontalScrossProperty, value);
        }

        public static readonly DependencyProperty EditModeProperty = DependencyProperty.Register(
            "EditMode", typeof(EditMode), typeof(ParameterControl), new PropertyMetadata(default(EditMode)));

        public EditMode EditMode
        {
            get => (EditMode) GetValue(EditModeProperty);
            set
            {
                EditMode old = (EditMode) GetValue(EditModeProperty);
                if (!(old is null)) old.PropertyChanged -= EditModeOnPropertyChanged;
                SetValue(EditModeProperty, value);
                EditMode.PropertyChanged += EditModeOnPropertyChanged;
            }
        }

        #endregion

        #region DataContext

        public bool IsMouseDown { get; set; } = false;

        #endregion

    }
}
