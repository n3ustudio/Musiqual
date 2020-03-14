using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Musiqual.Editor.Models;
using YDock.Interface;

namespace Musiqual.Parameter.Views
{

    /// <summary>
    /// EditModeView.xaml 的交互逻辑
    /// </summary>
    public partial class EditModeView : UserControl, INotifyPropertyChanged, IDockSource
    {

        public EditModeView(EditMode editMode)
        {

            EditMode = editMode;

            InitializeComponent();

        }

        #region DataContext

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

        #region Dock

        public IDockControl DockControl { get; set; }
        public string Header => "Mode";
        public ImageSource Icon => null;

        #endregion

        private void ToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton button) button.IsChecked = true;
        }

    }

}
