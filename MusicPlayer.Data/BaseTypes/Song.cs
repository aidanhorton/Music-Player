using System.IO;
using Prism.Mvvm;

namespace MusicPlayer.Data.BaseTypes
{
    public class Song : BindableBase
    {
        public string Name { get; }

        public string Artist { get; }

        public string Album { get; }

        public string FilePath { get; }

        public bool IsPlaying
        {
            get => this._isPlaying;
            set
            {
                this._isPlaying = value;
                this.RaisePropertyChanged(nameof(this.IsPlaying));
            }
        }

        public Song(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            var mediaFile = TagLib.File.Create(filePath);

            this.Name = string.IsNullOrEmpty(mediaFile.Tag.Title)
                ? Path.GetFileNameWithoutExtension(filePath)
                : mediaFile.Tag.Title;

            this.Artist = string.IsNullOrEmpty(mediaFile.Tag.FirstAlbumArtist)
                ? "Unknown"
                : mediaFile.Tag.FirstAlbumArtist;

            this.Album = mediaFile.Tag.Album ?? string.Empty;

            this.FilePath = filePath;
        }

        public bool Equals(Song song)
        {
            return song.FilePath == this.FilePath;
        }

        private bool _isPlaying;
    }
}
