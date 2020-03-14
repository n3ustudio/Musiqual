using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
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

namespace Musiqual.Playback
{

    /// <summary>
    /// PlaybackView.xaml 的交互逻辑
    /// </summary>
    public partial class PlaybackView : UserControl, INotifyPropertyChanged, IDockSource
    {

        public PlaybackView()
        {
            InitializeComponent();
        }

        #region Current

        public PlaybackView Current { get; set; } = new PlaybackView();

        #endregion

        #region DataContext

        private bool _isSoundLoaded;

        public bool IsSoundLoaded
        {
            get => _isSoundLoaded;
            set
            {
                _isSoundLoaded = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Core

        private SoundPlayer _sp = new SoundPlayer();

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
        public string Header => "Playback";
        public ImageSource Icon => null;

        #endregion

    }

}
