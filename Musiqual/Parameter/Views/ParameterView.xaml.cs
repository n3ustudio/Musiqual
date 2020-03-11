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
using Musiqual.Editor.Models;
using Scrosser.Models;
using YDock.Interface;

namespace Musiqual.Parameter.Views
{
    /// <summary>
    /// ParameterView.xaml 的交互逻辑
    /// </summary>
    public partial class ParameterView : UserControl, IDockSource, INotifyPropertyChanged
    {
        public ParameterView(ParameterData data, Scross scross, EditMode editMode)
        {
            if (data is null) data = new ParameterData();
            Header = data.Name + " - Parameter";
            ParameterData = data;
            if (scross is null) scross = new Scross();
            HorizontalScross = scross;
            if (editMode is null) editMode = new EditMode();
            EditMode = editMode;

            InitializeComponent();

            DataContext = this;

        }

        #region DockControl
        
        public IDockControl DockControl { get; set; }
        public string Header { get; }
        public ImageSource Icon => null;

        #endregion

        #region DataContext

        private Scross _verticalScross = new Scross();

        public Scross VerticalScross
        {
            get => _verticalScross;
            set
            {
                _verticalScross = value;
                OnPropertyChanged();
            }
        }

        private Scross _horizontalScross;

        public Scross HorizontalScross
        {
            get => _horizontalScross;
            set
            {
                _horizontalScross = value;
                OnPropertyChanged();
            }
        }

        private ParameterData _parameterData = new ParameterData();

        public ParameterData ParameterData
        {
            get => _parameterData;
            set
            {
                _parameterData = value;
                OnPropertyChanged();
            }
        }

        private EditMode _editMode;

        public EditMode EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
