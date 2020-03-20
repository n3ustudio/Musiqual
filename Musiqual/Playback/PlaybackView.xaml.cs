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
using System.Windows.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;
using Scrosser.Models;
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

            _timer = new DispatcherTimer(
                TimeSpan.FromSeconds(0.1),
                DispatcherPriority.Normal,
                (sender, args) =>
                {
                    if (IsSoundLoaded)
                    {
                        PlaybackSlider.Value = Player.Position.TotalMilliseconds;
                        try
                        {
                            SoundPosition =
                                new Posit<double>(Player.NaturalDuration.TimeSpan.TotalMilliseconds,
                                    Player.Position.TotalMilliseconds, 0);
                        }
                        catch (Exception e)
                        {
                            // Ignore
                        }
                    }
                },
                Dispatcher.CurrentDispatcher);

        }

        #region Current

        public static PlaybackView Current { get; } = new PlaybackView();

        #endregion

        #region DataContext

        private bool _isSoundLoaded;

        public bool IsSoundLoaded
        {
            get => _isSoundLoaded;
            set
            {
                if (!_isSoundLoaded && value) LoadSound();
                else if (!value && _isSoundLoaded) UnloadSound();
                _isSoundLoaded = value;
                OnPropertyChanged();
            }
        }

        private bool _isSoundPlaying;

        public bool IsSoundPlaying
        {
            get => _isSoundPlaying;
            set
            {
                if (!_isSoundPlaying && value) PlaySound();
                if (!value && _isSoundPlaying) PauseSound();
                _isSoundPlaying = value;
                OnPropertyChanged();
            }
        }

        private string _soundPath = "";

        public string SoundPath
        {
            get => _soundPath;
            set
            {
                _soundPath = value;
                Player.Source = new Uri(value);
                OnPropertyChanged();
            }
        }

        private Posit<double> _soundPosition = new Posit<double>(0, 0, 0);

        public Posit<double> SoundPosition
        {
            get => _soundPosition;
            set
            {
                _soundPosition = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region LoadSound

        private void LoadSound()
        {

            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog
            {
                Title = "Choose Wave Files",
                DefaultDirectory = Environment.CurrentDirectory,
                IsFolderPicker = false,
                AllowNonFileSystemItems = true,
                EnsurePathExists = true,
                Multiselect = false,
                Filters = { new CommonFileDialogFilter("Wave", ".wav") },
                EnsureFileExists = true
            };

            if (fileDialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                IsSoundLoaded = false;
                return;
            }
            SoundPath = fileDialog.FileName;

        }

        private void UnloadSound()
        {
            if (!Player.IsLoaded) return;
            Player.Stop();
            PlaybackSlider.Value = 0;
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
        public string Header => "Playback";
        public ImageSource Icon => null;

        #endregion

        #region Event Handler

        private void PlaybackSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.Position = new TimeSpan(0, 0, 0, 0, (int)PlaybackSlider.Value);
        }

        private void Player_OnMediaOpened(object sender, RoutedEventArgs e)
        {
            PlaybackSlider.Maximum = Player.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        #endregion

        #region Utilities

        private void PlaySound()
        {
            _timer.Start();
            Player.Play();
        }

        private void PauseSound()
        {
            _timer.Stop();
            Player.Pause();
        }

        private void ReloadButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string tmp = SoundPath;
            Player.Close();
            SoundPath = tmp;
            Player.Volume = VolumeSlider.Value;
        }

        #endregion

        #region Interface

        public void SetPosition(Posit<int> posit)
        {
            if (!IsSoundLoaded) return;
            //Dispatcher.CurrentDispatcher.Invoke(() =>
            //{
            //    Player.Position = new TimeSpan(0, 0, 0, 0,
            //        (int)Math.Floor(posit.Position * Player.NaturalDuration.TimeSpan.TotalMilliseconds / posit.Total));
            //    PlaybackSlider.Value = Player.Position.TotalMilliseconds;
            //});
            try
            {
                Player.Position = new TimeSpan(0, 0, 0, 0,
                    (int)Math.Floor(posit.Position * Player.NaturalDuration.TimeSpan.TotalMilliseconds / posit.Total));
            }
            catch (Exception e)
            {
                // Ignore
            }
            PlaybackSlider.Value = Player.Position.TotalMilliseconds;
            SoundPosition = new Posit<double>(posit.Total, posit.Position, 0);
        }

        #endregion

        #region Timer

        private DispatcherTimer _timer;

        #endregion

    }

}
