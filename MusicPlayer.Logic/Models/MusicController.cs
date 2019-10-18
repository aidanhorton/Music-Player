using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MusicPlayer.Data.BaseTypes;
using MusicPlayer.Data.Databases;
using MusicPlayer.Data.Helpers;
using MusicPlayer.Data.Interfaces;
using MusicPlayer.Logic.Interfaces;
using WMPLib;

namespace MusicPlayer.Logic.Models
{
    public class MusicController : IMusicController
    {
        private const string DefaultTableName = "All songs";

        /// <summary>
        /// The instance of the media player API.
        /// </summary>
        public WindowsMediaPlayer MediaPlayer = new WindowsMediaPlayer();

        /// <summary>
        /// An event fired every time a new song starts playing.
        /// </summary>
        public event EventHandler<Song> SongStarted;

        /// <summary>
        /// An event fired when a new song is added to the database.
        /// </summary>
        public event EventHandler<Song[]> SongsAdded;

        /// <summary>
        /// The instance of the song database.
        /// </summary>
        public SongDatabase Database = new SongDatabase();

        /// <summary>
        /// The currently playing song.
        /// </summary>
        public Song CurrentSong { get; set; }

        /// <summary>
        /// The current collection of songs in the database.
        /// </summary>
        public IList<Song> SongCollection { get; set; }

        /// <summary>
        /// The current playlist collection in the database.
        /// </summary>
        public IList<IPlaylist> PlaylistCollection { get; } = new List<IPlaylist>();

        /// <summary>
        /// The current position into the current song in seconds.
        /// </summary>
        public double CurrentSongPosition
        {
            get => this.MediaPlayer.controls.currentPosition;
            set => this.MediaPlayer.controls.currentPosition = this.GetValidSongPosition(value);
        }

        /// <summary>
        /// Gets the current song length in seconds.
        /// </summary>
        public double CurrentSongLength => this.MediaPlayer.currentMedia.duration;

        /// <summary>
        /// The current playing playlist.
        /// </summary>
        public IPlaylist CurrentPlaylist 
        {
            get => new Playlist(this.MediaPlayer.currentPlaylist);
            set
            {
                if (value.MediaPlaylist.count > 0)
                {
                    this.MediaPlayer.currentPlaylist = value.MediaPlaylist;
                }
            }
        }

        public MusicController()
        {
            this.MediaPlayer.PlayStateChange += this.MusicPlayerOnPlayStateChange;

            this.SetMediaSettings();
            this.PopulateCollections();

            this.CurrentPlaylist = this.PlaylistCollection.First(playlist => playlist.Name == DefaultTableName);
        }

        /// <summary>
        /// Plays the specified song.
        /// </summary>
        /// <param name="song">The song to play.</param>
        /// <param name="playlistName">The playlist to play the song from.</param>
        public void PlaySong(Song song, string playlistName = "All songs")
        {
            if (this.CurrentPlaylist.Name == playlistName)
            {
                this.PlaySongInCurrentPlaylist(song);
                return;
            }

            foreach (var playlist in this.PlaylistCollection)
            {
                if (!string.Equals(playlist.Name, playlistName))
                {
                    continue;
                }

                this.CurrentPlaylist = playlist;

                this.PlaySongInCurrentPlaylist(song);
                return;
            }
        }

        /// <summary>
        /// Resumes playing of the current music track.
        /// </summary>
        public void Play()
        {
            this.MediaPlayer.controls.play();
        }

        /// <summary>
        /// Pauses the currently playing music track.
        /// </summary>
        public void Pause()
        {
            this.MediaPlayer.controls.pause();
        }

        /// <summary>
        /// Skips to and plays the next song in the current playlist.
        /// </summary>
        public void NextSong()
        {
            this.MediaPlayer.controls.play();
            this.MediaPlayer.controls.next();
        }

        /// <summary>
        /// Plays the previous song in the current playlist.
        /// </summary>
        public void PreviousSong()
        {
            this.MediaPlayer.controls.play();
            this.MediaPlayer.controls.previous();
        }

        /// <summary>
        /// Sets the shuffle setting for the current playlist
        /// </summary>
        /// <param name="shuffle">Boolean indicating whether to shuffle or un-shuffle.</param>
        public void SetShuffle(bool shuffle)
        {
            this.MediaPlayer.settings.setMode("shuffle", shuffle);
        }

        /// <summary>
        /// Creates playlist if it doesn't already exist, and adds specified songs to the playlist.
        /// </summary>
        /// <param name="playlistName">The name of the playlist to create and/or add to.</param>
        /// <param name="songs">The songs to add to the playlist.</param>
        public void SetUpPlaylist(string playlistName, params Song[] songs)
        {
            if (this.PlaylistCollection.All(playlist => playlist.Name != playlistName))
            {
                this.CreatePlaylist(playlistName);
            }

            if (songs == null)
            {
                return;
            }

            for (var i = 0; i < this.PlaylistCollection.Count; i++)
            {
                if (this.PlaylistCollection[i].Name != playlistName)
                {
                    continue;
                }

                if (playlistName == "All songs")
                {
                    this.PlaylistCollection[i].IsPlaying = true;
                }

                foreach (var song in songs)
                {
                    var media = this.MediaPlayer.newMedia(song.FilePath);
                    this.PlaylistCollection[i].MediaPlaylist.appendItem(media);

                    this.Database.AddSongs(this.PlaylistCollection[i].Name, song.FilePath);
                }

                this.SongsAdded?.Invoke(this, songs.ToArray());

                return;
            }
        }

        /// <summary>
        /// Gets the songs in the specified playlist.
        /// </summary>
        /// <param name="playlist">The playlist to extract songs from.</param>
        /// <returns>An enumerable of <see cref="Song"/>.</returns>
        public IEnumerable<Song> GetSongsInPlaylist(IPlaylist playlist)
        {
            return this.Database.GetSongCollection(playlist.Name);
        }

        /// <summary>
        /// Populates the song and playlist collections.
        /// </summary>
        private void PopulateCollections()
        {
            this.Database.CreateTable(DefaultTableName);

            var songsToAdd = Directory.GetFiles(DirectoryHelper.MusicDirectory, "*.mp3");
            this.SongCollection = Database.AddSongs(DefaultTableName, songsToAdd);

            foreach (var song in this.SongCollection)
            {
                var media = this.MediaPlayer.newMedia(song.FilePath);
                this.CurrentPlaylist.MediaPlaylist.appendItem(media);
            }

            var playlistNames = this.Database.GetPlaylistCollection();
            foreach (var playlist in playlistNames)
            {
                var songs = this.Database.GetSongCollection(playlist);
                this.SetUpPlaylist(playlist, songs.ToArray());
            }
        }

        /// <summary>
        /// Plays a song in the current playlist.
        /// </summary>
        /// <param name="songToPlay">The song to play.</param>
        private void PlaySongInCurrentPlaylist(Song songToPlay)
        {
            var playlist = this.CurrentPlaylist.MediaPlaylist;
            for (var i = 0; i < playlist.count; i++)
            {
                var song = playlist.Item[i];
                if (song.name == songToPlay.Name)
                {
                    this.MediaPlayer.controls.playItem(song);
                    return;
                }
            }
        }

        /// <summary>
        /// Creates a playlist with the specified name if one doesn't exist already.
        /// </summary>
        /// <param name="playlistName">The name of the playlist to create.</param>
        private void CreatePlaylist(string playlistName)
        {
            if (this.PlaylistCollection.Any(playlist => playlist.Name == playlistName))
            {
                return;
            }

            this.Database.CreateTable(playlistName);

            var newPlaylist = this.MediaPlayer.playlistCollection.newPlaylist(playlistName);
            this.PlaylistCollection.Add(new Playlist(newPlaylist));
        }

        /// <summary>
        /// Gets a valid song position - in this case avoiding too close to the end of the song.
        /// </summary>
        /// <param name="position">The suggested position.</param>
        /// <returns>A valid position in the song that represents the suggested position.</returns>
        private double GetValidSongPosition(double position)
        {
            return Math.Abs(position - this.CurrentSongLength) > 0.5
                ? position
                : this.CurrentSongLength - 0.5;
        }

        /// <summary>
        /// Sets the WMPLib media playback settings.
        /// </summary>
        private void SetMediaSettings()
        {
            this.MediaPlayer.settings.volume = 30;
            this.MediaPlayer.settings.setMode("loop", true);
        }

        /// <summary>
        /// Handles state change of the music player.
        /// </summary>
        /// <param name="newState">The integer value of the new state enum.</param>
        private void MusicPlayerOnPlayStateChange(int newState)
        {
            if (newState == (int)WMPPlayState.wmppsPlaying)
            {
                if (this.CurrentSong != null && this.MediaPlayer.currentMedia.sourceURL == this.CurrentSong.FilePath)
                {
                    return;
                }

                this.CurrentSong = new Song(this.MediaPlayer.currentMedia.sourceURL);

                this.SongStarted?.Invoke(this, this.CurrentSong);
            }
        }
    }
}
