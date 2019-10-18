using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MusicPlayer.Data.BaseTypes;
using Prism.Commands;
using Prism.Mvvm;

namespace Music_Player.ViewModels
{
    public class MusicControlViewModel : BindableBase
    {
        private readonly MainWindowViewModel _viewModel;

        public Song CurrentSong
        {
            get => this._currentSong;
            set
            {
                this._currentSong = value;
                this.RaisePropertyChanged(nameof(this.CurrentSong));
            }
        }

        /// <summary>
        /// Indicates if music is currently playing.
        /// </summary>
        public bool IsPlaying
        {
            get => this._isPlaying;
            set
            {
                this._isPlaying = value;
                this.RaisePropertyChanged(nameof(this.IsPlaying));
            }
        }

        /// <summary>
        /// The current position in seconds into the current song.
        /// </summary>
        public double CurrentSongPosition
        {
            get => this._currentSongPosition;
            set
            {
                this._currentSongPosition = value;
                this.RaisePropertyChanged(nameof(this.CurrentSongPosition));
            }
        }

        /// <summary>
        /// The total length in seconds of the current song.
        /// </summary>
        public double CurrentSongDuration
        {
            get => this._currentSongDuration;
            set
            {
                this._currentSongDuration = value;
                this.RaisePropertyChanged(nameof(this.CurrentSongDuration));
            }
        }

        /// <summary>
        /// The text readout of the song position.
        /// </summary>
        public string SongTimeReadout
        {
            get => this._songTimeReadout;
            set
            {
                this._songTimeReadout = value;
                this.RaisePropertyChanged(nameof(this.SongTimeReadout));
            }
        }

        public bool Shuffle
        {
            get => this._shuffle;
            set
            {
                this._shuffle = value;
                this.RaisePropertyChanged(nameof(this.Shuffle));
            }
        }

        /// <summary>
        /// The command for if the play/pause button is pressed.
        /// </summary>
        public ICommand PlayPauseCommand { get; set; }

        /// <summary>
        /// The command for if the next song button is pressed.
        /// </summary>
        public ICommand NextSongCommand { get; set; }

        /// <summary>
        /// The command for if the previous song button is pressed.
        /// </summary>
        public ICommand PreviousSongCommand { get; set; }

        /// <summary>
        /// The command for changing the shuffle of the current playlist.
        /// </summary>
        public ICommand ShuffleSongsCommand { get; set; }

        /// <summary>
        /// The command for if a drag is started on the slider.
        /// </summary>
        public ICommand SliderDragStartedCommand { get; set; }

        /// <summary>
        /// The command for if a drag is completed on the slider.
        /// </summary>
        public ICommand SliderDragCompletedCommand { get; set; }

        /// <summary>
        /// The command called when the value of the slider is changed.
        /// </summary>
        public ICommand SliderValueChangedCommand { get; set; }

        public MusicControlViewModel(MainWindowViewModel viewModel)
        {
            this._viewModel = viewModel;

            this._viewModel.MusicController.SongStarted += this.NewSongLoaded;

            this.PlayPauseCommand = new DelegateCommand(this.HandlePlayPause);
            this.NextSongCommand = new DelegateCommand(this._viewModel.MusicController.NextSong);
            this.PreviousSongCommand = new DelegateCommand(this._viewModel.MusicController.PreviousSong);
            this.ShuffleSongsCommand = new DelegateCommand(this.ShuffleSongs);
            this.SliderDragStartedCommand = new DelegateCommand(this.SliderDragStarted);
            this.SliderDragCompletedCommand = new DelegateCommand(this.SliderDragCompleted);
            this.SliderValueChangedCommand = new RelayCommand<RoutedPropertyChangedEventArgs<double>>(this.SliderValueChanged);

            this.CreateTimer();
        }

        private void ShuffleSongs()
        {
            this.Shuffle = !this.Shuffle;

            this._viewModel.MusicController.SetShuffle(this.Shuffle);
        }

        /// <summary>
        /// Handles switching between play/pause.
        /// </summary>
        private void HandlePlayPause()
        {
            if (this.IsPlaying)
            {
                this.IsPlaying = false;
                this._viewModel.MusicController.Pause();
                this.UpdateSongPosition(this.CurrentSongPosition);
            }
            else
            {
                this.IsPlaying = true;
                this._viewModel.MusicController.Play();
            }
        }

        /// <summary>
        /// Set the timer readout to a formatted string.
        /// </summary>
        /// <param name="currentSeconds">The current seconds into the song.</param>
        private void SetTimerReadout(double currentSeconds)
        {
            this.SongTimeReadout =
                $@"{TimeSpan.FromSeconds(currentSeconds):mm\:ss} / {TimeSpan.FromSeconds(this._viewModel.MusicController.CurrentSongLength):mm\:ss}";
        }

        /// <summary>
        /// Updates the position of the song currently playing.
        /// </summary>
        /// <param name="songPosition">The new song position.</param>
        private void UpdateSongPosition(double songPosition)
        {
            this.CurrentSongPosition = songPosition;
            SetTimerReadout(this.CurrentSongPosition);

            if (this._sliderDragStarted)
            {
                return;
            }

            this._viewModel.MusicController.CurrentSongPosition = songPosition;
        }

        /// <summary>
        /// Updates the current song position.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The arguments.</param>
        private void SetSongPosition(object sender, EventArgs e)
        {
            this.CurrentSongPosition = this._viewModel.MusicController.CurrentSongPosition;
            this.SetTimerReadout(this.CurrentSongPosition);
        }

        /// <summary>
        /// Handles loading of a new song.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="newSong">The new song that has loaded.</param>
        private void NewSongLoaded(object sender, Song newSong)
        {
            this.CurrentSong = newSong;
            this.IsPlaying = true;
            this.CurrentSongDuration = this._viewModel.MusicController.CurrentSongLength;
            this.UpdateSongPosition(0);
        }

        /// <summary>
        /// Handles change in value of the slider
        /// </summary>
        /// <param name="slider">The new slider value.</param>
        private void SliderValueChanged(RoutedPropertyChangedEventArgs<double> slider)
        {
            if (this._sliderDragStarted)
            {
                this._currentSongPosition = slider.NewValue;
                this.SetTimerReadout(slider.NewValue);
            }
            else if (Math.Abs(this.CurrentSongPosition - slider.NewValue) > 1)
            {
                this.UpdateSongPosition(slider.NewValue);
            }
        }

        /// <summary>
        /// Handles the drag started for the slider.
        /// </summary>
        private void SliderDragStarted()
        {
            this._sliderDragStarted = true;
            this._songPositionTimer.Stop();
        }

        /// <summary>
        /// Handles when the slider drag has completed.
        /// </summary>
        private void SliderDragCompleted()
        {
            this._sliderDragStarted = false;
            this.UpdateSongPosition(this._currentSongPosition);
            this._songPositionTimer.Start();
        }

        /// <summary>
        /// Creates a timer that updates the song position information.
        /// </summary>
        private void CreateTimer()
        {
            this._songPositionTimer = new Timer();
            this._songPositionTimer.Elapsed += this.SetSongPosition;
            this._songPositionTimer.Interval = 500;
            this._songPositionTimer.Start();
        }

        private bool _isPlaying = true;

        private Song _currentSong;

        private double _currentSongPosition;

        private double _currentSongDuration;

        private string _songTimeReadout;

        private Timer _songPositionTimer;

        private bool _sliderDragStarted;

        private bool _shuffle;
    }
}