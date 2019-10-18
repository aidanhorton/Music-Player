using System.Collections.ObjectModel;
using System.Linq;
using MusicPlayer.Data.BaseTypes;

namespace Music_Player.ViewModels
{
    public class AlbumViewModel
    {
        private readonly MainWindowViewModel _viewModel;

        /// <summary>
        /// The unique collection of albums represented by the <see cref="Song"/> data type.
        /// </summary>
        public ObservableCollection<Song> AlbumCollection { get; set; } = new ObservableCollection<Song>();

        public AlbumViewModel(MainWindowViewModel viewModel)
        {
            this._viewModel = viewModel;

            this._viewModel.MusicController.SongsAdded += this.UpdateAlbumCollection;

            this.UpdateCollection();
        }

        /// <summary>
        /// Updates the album collection to all albums in the current list of songs..
        /// </summary>
        private void UpdateCollection()
        {
            var songs = this._viewModel.MusicController.SongCollection;
            if (songs == null)
            {
                return;
            }

            foreach (var song in songs)
            {
                if (this.AlbumCollection.Select(x => x.Album).Contains(song.Album))
                {
                    continue;
                }

                this.AlbumCollection.Add(song);
            }
        }

        /// <summary>
        /// Updates the album collection when a new song is added.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="newSongs">The new songs that has been added.</param>
        private void UpdateAlbumCollection(object sender, Song[] newSongs)
        {
            foreach (var song in newSongs)
            {
                if (!this.AlbumCollection.Select(x => x.Album).Contains(song.Album))
                {
                    this.AlbumCollection.Add(song);
                }
            }
        }
    }
}
