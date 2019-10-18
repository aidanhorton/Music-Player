using MusicPlayer.Logic.Interfaces;

namespace Music_Player.ViewModels
{
    public class MainWindowViewModel
    {
        /// <summary>
        /// The main music controller for the view-models.
        /// </summary>
        public IMusicController MusicController;

        public MusicControlViewModel MusicControlViewModel { get; }

        public PlaylistViewModel PlaylistViewModel { get; }

        public AlbumViewModel AlbumViewModel { get; }

        public MainWindowViewModel(IMusicController musicController)
        {
            this.MusicController = musicController;

            this.MusicControlViewModel = new MusicControlViewModel(this);
            this.PlaylistViewModel = new PlaylistViewModel(this);
            this.AlbumViewModel = new AlbumViewModel(this);
        }
    }
}
