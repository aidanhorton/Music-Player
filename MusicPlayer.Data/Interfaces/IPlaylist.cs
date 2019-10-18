using WMPLib;

namespace MusicPlayer.Data.Interfaces
{
    public interface IPlaylist
    {
        /// <summary>
        /// The playable media playlist.
        /// </summary>
        IWMPPlaylist MediaPlaylist { get; set; }

        /// <summary>
        /// Defines if the playlist is currently being played or not.
        /// </summary>
        bool IsPlaying { get; set; }

        /// <summary>
        /// The name of the playlist.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Determines if this playlist is equal to the one specified.
        /// </summary>
        /// <param name="playlist">The playlist to compare to.</param>
        /// <returns>True if the playlist are a match, otherwise false.</returns>
        bool Equals(IPlaylist playlist);
    }
}
