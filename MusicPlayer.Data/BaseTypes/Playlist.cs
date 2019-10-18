using MusicPlayer.Data.Interfaces;
using Prism.Mvvm;
using WMPLib;

namespace MusicPlayer.Data.BaseTypes
{
    public class Playlist : BindableBase, IPlaylist
    {
        /// <summary>
        /// The playable media playlist.
        /// </summary>
        public IWMPPlaylist MediaPlaylist { get; set; }

        /// <summary>
        /// Defines if the playlist is currently being played or not.
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
        /// The name of the playlist.
        /// </summary>
        public string Name => this.MediaPlaylist.name;

        public Playlist(IWMPPlaylist playlist)
        {
            this.MediaPlaylist = playlist;
        }

        /// <summary>
        /// Determines if this playlist is equal to the one specified.
        /// </summary>
        /// <param name="playlist">The playlist to compare to.</param>
        /// <returns>True if the playlist are a match, otherwise false.</returns>
        public bool Equals(IPlaylist playlist)
        {
            return playlist.Name == this.Name;
        }

        private bool _isPlaying;
    }
}
