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
using YDock.Interface;

namespace Musiqual.Parameter.Views
{
    /// <summary>
    /// AutoScrollView.xaml 的交互逻辑
    /// </summary>
    public partial class AutoScrollView : UserControl, INotifyPropertyChanged, IDockSource
    {

        public AutoScrollView()
        {
            InitializeComponent();
        }

        #region Current

        public static AutoScrollView Current { get; } = new AutoScrollView();

        #endregion

        #region DataContext

        private bool _isAutoScrollEnabled;

        public bool IsAutoScrollEnabled
        {
            get => _isAutoScrollEnabled;
            set
            {
                _isAutoScrollEnabled = value;
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

        #region DockSource

        public IDockControl DockControl { get; set; }
        public string Header => "Auto Scroll";
        public ImageSource Icon => null;

        #endregion

    }
}
