using System;
using System.Collections.Generic;
using MusicPlayer.Data.BaseTypes;
using MusicPlayer.Data.Interfaces;

namespace MusicPlayer.Logic.Interfaces
{
    public interface IMusicController
    {
        /// <summary>
        /// An event fired every time a new song starts playing.
        /// </summary>
        event EventHandler<Song> SongStarted;

        /// <summary>
        /// An event fired when a new song is added to the database.
        /// </summary>
        event EventHandler<Song[]> SongsAdded;

        /// <summary>
        /// The currently playing song.
        /// </summary>
        Song CurrentSong { get; set; }

        /// <summary>
        /// The current collection of songs in the playlist.
        /// </summary>
        IList<Song> SongCollection { get; set; }

        /// <summary>
        /// The current playlist collection in the database.
        /// </summary>
        IList<IPlaylist> PlaylistCollection { get; }

        /// <summary>
        /// The current position into the current song in seconds.
        /// </summary>
        double CurrentSongPosition { get; set; }

        /// <summary>
        /// Gets the current song length in seconds.
        /// </summary>
        double CurrentSongLength { get; }

        /// <summary>
        /// The current playing playlist.
        /// </summary>
        IPlaylist CurrentPlaylist { get; set; }

        /// <summary>
        /// Plays the specified song.
        /// </summary>
        /// <param name="song">The song to play.</param>
        /// <param name="playlistName">The playlist to play the song from.</param>
        void PlaySong(Song song, string playlistName = "All songs");

        /// <summary>
        /// Resumes playing of the current music track.
        /// </summary>
        void Play();

        /// <summary>
        /// Pauses the currently playing music track.
        /// </summary>
        void Pause();

        /// <summary>
        /// Skips to and plays the next song in the current playlist.
        /// </summary>
        void NextSong();

        /// <summary>
        /// Plays the previous song in the current playlist.
        /// </summary>
        void PreviousSong();

        /// <summary>
        /// Sets the shuffle setting for the current playlist
        /// </summary>
        /// <param name="shuffle">Boolean indicating whether to shuffle or un-shuffle.</param>
        void SetShuffle(bool shuffle);

        /// <summary>
        /// Creates playlist if it doesn't already exist, and adds specified songs to the playlist.
        /// </summary>
        /// <param name="playlistName">The name of the playlist to create and/or add to.</param>
        /// <param name="songs">The songs to add to the playlist.</param>
        void SetUpPlaylist(string playlistName, params Song[] songs);

        /// <summary>
        /// Gets the songs in the specified playlist.
        /// </summary>
        /// <param name="playlist">The playlist to extract songs from.</param>
        /// <returns>An enumerable of <see cref="Song"/>.</returns>
        IEnumerable<Song> GetSongsInPlaylist(IPlaylist playlist);
    }
}
