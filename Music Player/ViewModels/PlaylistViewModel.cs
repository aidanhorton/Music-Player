using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Music_Player.Views;
using MusicPlayer.Data.BaseTypes;
using MusicPlayer.Data.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace Music_Player.ViewModels
{
    public class PlaylistViewModel : BindableBase
    {
        private readonly MainWindowViewModel _viewModel;

        /// <summary>
        /// The playlist collection loaded from the database.
        /// </summary>
        public ObservableCollection<IPlaylist> PlaylistCollection { get; set; } = new ObservableCollection<IPlaylist>();

        /// <summary>
        /// The songs in the currently selected playlist.
        /// </summary>
        public ObservableCollection<Song> CurrentPlaylistSongCollection { get; set; } = new ObservableCollection<Song>();

        /// <summary>
        /// The song to add to the playlist.
        /// </summary>
        public Song AddToPlaylistSong
        {
            get => this._addToPlaylistSong;
            set
            {
                this._addToPlaylistSong = value;
                this.RaisePropertyChanged(nameof(this.AddToPlaylistSong));
            }
        }

        /// <summary>
        /// Specifies if the current state is user adding a song to a playlist.
        /// </summary>
        public bool AddingSongToPlaylist
        {
            get => this._addingSongToPlaylist;
            set
            {
                this._addingSongToPlaylist = value;
                this.RaisePropertyChanged(nameof(this.AddingSongToPlaylist));
            }
        }

        /// <summary>
        /// True if the selected song can be added to the selected playlist.
        /// </summary>
        public bool CanAddToSelectedPlaylist
        {
            get => this._canAddToSelectedPlaylist;
            set
            {
                this._canAddToSelectedPlaylist = value;
                this.RaisePropertyChanged(nameof(this.CanAddToSelectedPlaylist));
            }
        }

        /// <summary>
        /// The search text to filter the songs by.
        /// </summary>
        public string SongSearchText
        {
            get => this._songSearchText;
            set
            {
                this._songSearchText = value;
                this.UpdateSongCollection();
            }
        }

        /// <summary>
        /// Plays the song passed as an argument.
        /// </summary>
        public RelayCommand<Song> PlaySongCommand { get; set; }

        /// <summary>
        /// Plays the playlist passed as an argument.
        /// </summary>
        public RelayCommand<IPlaylist> PlayPlaylistCommand { get; set; }

        /// <summary>
        /// The command called when the selected playlist is changed.
        /// </summary>
        public RelayCommand<IPlaylist> SelectedPlaylistChangedCommand { get; set; }

        /// <summary>
        /// The command called when a song is selected to add to a playlist.
        /// </summary>
        public RelayCommand<Song> OpenAddToPlaylistCommand { get; set; }

        /// <summary>
        /// The command to add the song to the selected playlist.
        /// </summary>
        public RelayCommand<IPlaylist> AddToPlaylistCommand { get; set; }

        /// <summary>
        /// The command for creating a new playlist.
        /// </summary>
        public ICommand CreatePlaylistCommand { get; set; }
        
        /// <summary>
        /// The command for cancelling the add to playlist command.
        /// </summary>
        public ICommand CancelAddToPlaylistCommand { get; set; }

        public PlaylistViewModel(MainWindowViewModel viewModel)
        {
            this._viewModel = viewModel;

            this._viewModel.MusicController.SongStarted += this.NewSongLoaded;

            this.PlaySongCommand = new RelayCommand<Song>(this.PlaySong);
            this.PlayPlaylistCommand = new RelayCommand<IPlaylist>(this.PlayPlaylist);
            this.SelectedPlaylistChangedCommand = new RelayCommand<IPlaylist>(this.SelectedPlaylistChanged);
            this.OpenAddToPlaylistCommand = new RelayCommand<Song>(this.OpenAddToPlaylist);
            this.CreatePlaylistCommand = new DelegateCommand(this.CreatePlaylist);
            this.CancelAddToPlaylistCommand = new DelegateCommand(() => this.AddingSongToPlaylist = false);
            this.AddToPlaylistCommand = new RelayCommand<IPlaylist>(this.AddToPlaylist);

            this.PlaylistCollection.AddRange(this._viewModel.MusicController.PlaylistCollection);
        }

        /// <summary>
        /// Updates the song collection based on the searched text.
        /// </summary>
        private void UpdateSongCollection()
        {
            if (this.SelectedPlaylist == null)
            {
                return;
            }

            var songCollection = this._viewModel.MusicController.GetSongsInPlaylist(this.SelectedPlaylist);
            songCollection = songCollection.Where(song => song.Name.ToLower().Contains(this.SongSearchText.ToLower()));

            this.CurrentPlaylistSongCollection.Clear();
            this.CurrentPlaylistSongCollection.AddRange(songCollection);
        }

        /// <summary>
        /// Handles when the selected playlist is changed.
        /// </summary>
        /// <param name="newPlaylist">The newly selected playlist.</param>
        private void SelectedPlaylistChanged(IPlaylist newPlaylist)
        {
            this.SelectedPlaylist = newPlaylist;

            if (newPlaylist == null)
            {
                if (!this.AddingSongToPlaylist)
                {
                    this.CurrentPlaylistSongCollection.Clear();
                }

                this.CanAddToSelectedPlaylist = false;
                return;
            }

            if (this.AddingSongToPlaylist)
            {
                this.CanAddToSelectedPlaylist = newPlaylist.Name != "All songs";
                return;
            }

            this.CurrentPlaylistSongCollection.Clear();
            this.CurrentPlaylistSongCollection.AddRange(this._viewModel.MusicController.GetSongsInPlaylist(newPlaylist));

            if (newPlaylist.Equals(this._viewModel.MusicController.CurrentPlaylist))
            {
                this.UpdateSongIsPlaying();
            }
        }

        /// <summary>
        /// Handles creation of a new playlist.
        /// </summary>
        private void CreatePlaylist()
        {
            var playlistDialog = new NewPlaylistDialog();

            if (playlistDialog.ShowDialog() != true)
            {
                return;
            }

            this._viewModel.MusicController.SetUpPlaylist(playlistDialog.Answer);

            this.PlaylistCollection.AddRange(this._viewModel.MusicController.PlaylistCollection.Where(playlist =>
                this.PlaylistCollection.All(x => x.Name != playlist.Name)));
        }

        /// <summary>
        /// Plays the specified playlist.
        /// </summary>
        /// <param name="playlist">The playlist to play.</param>
        private void PlayPlaylist(IPlaylist playlist)
        {
            if (playlist.MediaPlaylist.count <= 0)
            {
                return;
            }

            for (var i = 0; i < this.PlaylistCollection.Count; i++)
            {
                this.PlaylistCollection[i].IsPlaying = this.PlaylistCollection[i].Equals(playlist);
            }

            this._viewModel.MusicController.CurrentPlaylist = playlist;

            for (var i = 0; i < this.CurrentPlaylistSongCollection.Count; i++)
            {
                this.CurrentPlaylistSongCollection[i].IsPlaying = false;
            }

            if (playlist.Equals(this._viewModel.MusicController.CurrentPlaylist))
            {
                this.UpdateSongIsPlaying();
            }
        }

        /// <summary>
        /// Plays the selected song.
        /// </summary>
        /// <param name="song">The selected song.</param>
        private void PlaySong(Song song)
        {
            this.UpdateSongIsPlaying(song);

            this._viewModel.MusicController.PlaySong(song, this.SelectedPlaylist.Name);
        }

        /// <summary>
        /// Opens the add to playlist UI.
        /// </summary>
        /// <param name="song">The song to add to a playlist.</param>
        private void OpenAddToPlaylist(Song song)
        {
            this.AddToPlaylistSong = song;
            this.AddingSongToPlaylist = true;
        }

        /// <summary>
        /// Adds the selected song to the selected playlist.
        /// </summary>
        /// <param name="selectedPlaylist">The selected playlist.</param>
        private void AddToPlaylist(IPlaylist selectedPlaylist)
        {
            this._viewModel.MusicController.SetUpPlaylist(selectedPlaylist.Name, this.AddToPlaylistSong);
            this.AddingSongToPlaylist = false;
        }

        /// <summary>
        /// Updates the currently playing song.
        /// </summary>
        /// <param name="song"></param>
        private void UpdateSongIsPlaying(Song song = null)
        {
            if (this.CurrentPlaylistSongCollection.Count <= 0 || !this.SelectedPlaylist.Equals(this._viewModel.MusicController.CurrentPlaylist) && !this.AddingSongToPlaylist)
            {
                return;
            }

            if (song == null)
            {
                song = this._viewModel.MusicController.CurrentSong;
            }

            for (var i = 0; i < this.CurrentPlaylistSongCollection.Count; i++)
            {
                this.CurrentPlaylistSongCollection[i].IsPlaying = song.Equals(this.CurrentPlaylistSongCollection[i]);
            }
        }

        private void NewSongLoaded(object sender, Song newSong)
        {
            this.UpdateSongIsPlaying(newSong);
        }

        internal IPlaylist SelectedPlaylist;

        private Song _addToPlaylistSong;

        private bool _addingSongToPlaylist;

        private bool _canAddToSelectedPlaylist;

        private string _songSearchText;
    }
}