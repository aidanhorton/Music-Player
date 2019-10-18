using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using MusicPlayer.Data.BaseTypes;

namespace MusicPlayer.Data.Databases
{
    public class SongDatabase : BaseDatabase
    {
        private const string SongDatabaseFileName = "MusicDatabase.sqlite";
        private const string FilePathParameterName = "filePath";

        public SongDatabase() : base(SongDatabaseFileName)
        {
            // Uncomment below if database dies
            /*foreach (var playlist in this.GetPlaylistCollection())
            {
                var deleteTableSql = $"DROP Table '{playlist}'";
                this.ExecuteNonQuery(deleteTableSql);
            }*/
        }

        /// <summary>
        /// Creates a table in the database if it doesn't already exist.
        /// </summary>
        /// <param name="tableName">The name of the table to create.</param>
        public void CreateTable(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("Cannot create a database table with no name", nameof(tableName));
            }

            if (this.DoesTableExist(tableName))
            {
                return;
            }

            var createTableSql = $"CREATE TABLE '{tableName}' ({FilePathParameterName} VARCHAR(10), UNIQUE({FilePathParameterName}))";

            var createTableCommand = new SQLiteCommand(createTableSql, this.DatabaseConnection);
            createTableCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets the collection of songs from a table.
        /// </summary>
        /// <param name="tableName">The table name to read.</param>
        /// <returns>A list of the songs found.</returns>
        public IList<Song> GetSongCollection(string tableName)
        {
            var getSongsSql = $"SELECT * FROM '{tableName}'";
            var result = this.ExecuteQuery(getSongsSql);

            var songs = new List<Song>();
            while (result.Read())
            {
                var filePath = (string) result["filePath"];
                if (File.Exists(filePath))
                {
                    songs.Add(new Song(filePath));
                }
            }

            return songs;
        }

        /// <summary>
        /// Gets the current playlist collection.
        /// </summary>
        /// <returns>A list of the playlist names.</returns>
        public IList<string> GetPlaylistCollection()
        {
            var getPlaylistCollectionSql = $"SELECT name FROM sqlite_master WHERE type='table'";
            var result = this.ExecuteQuery(getPlaylistCollectionSql);

            var playlistCollection = new List<string>();
            while (result.Read())
            {
                playlistCollection.Add((string)result["name"]);
            }

            return playlistCollection;
        }

        /// <summary>
        /// Add songs to a database.
        /// </summary>
        /// <param name="tableName">The table to add the songs to.</param>
        /// <param name="filePaths">The file paths of the songs to add.</param>
        /// <returns></returns>
        public IList<Song> AddSongs(string tableName, params string[] filePaths)
        {
            if (!this.DoesTableExist(tableName))
            {
                throw new ArgumentException("Table does not exist");
            }

            foreach (var song in filePaths)
            {
                this.AddSong(tableName, song);
            }

            return this.GetSongCollection(tableName);
        }

        /// <summary>
        /// Adds a single song to the database.
        /// </summary>
        /// <param name="tableName">The table to add the song to.</param>
        /// <param name="filePath">The file path of the song to add.</param>
        private void AddSong(string tableName, string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File path supplied does not exist");
            }

            var fileName = Path.GetFileName(filePath);
            if (fileName == null)
            {
                throw new NullReferenceException("Could not extract file name from song path");
            }

            var addSongSql = $"INSERT OR IGNORE INTO '{tableName}'({FilePathParameterName}) VALUES('{filePath}')";

            this.ExecuteNonQuery(addSongSql);
        }
    }
}
