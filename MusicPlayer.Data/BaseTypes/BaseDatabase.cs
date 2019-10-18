using System.Data.SQLite;
using System.IO;

namespace MusicPlayer.Data.BaseTypes
{
    public class BaseDatabase
    {
        internal readonly string DatabaseName;

        internal SQLiteConnection DatabaseConnection;

        protected BaseDatabase(string databaseName)
        {
            this.DatabaseName = databaseName;

            this.OpenDatabaseConnection();
        }

        /// <summary>
        /// Executes an SQL command that doesn't return any value.
        /// </summary>
        /// <param name="sqlCommand">The SQL syntax to execute.</param>
        internal void ExecuteNonQuery(string sqlCommand)
        {
            var command = new SQLiteCommand(sqlCommand, this.DatabaseConnection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes an SQL command that returns a value.
        /// </summary>
        /// <param name="sqlCommand">The SQL syntax to execute.</param>
        /// <returns>A <see cref="SQLiteDataReader"/> with the query result.</returns>
        internal SQLiteDataReader ExecuteQuery(string sqlCommand)
        {
            var command = new SQLiteCommand(sqlCommand, this.DatabaseConnection);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Checks if a database table exists.
        /// </summary>
        /// <param name="tableName">The table name to check.</param>
        /// <returns>True if the table exists, otherwise false.</returns>
        internal bool DoesTableExist(string tableName)
        {
            var createTableSql = $"SELECT name FROM sqlite_master WHERE name='{tableName}'";

            using (var command = this.DatabaseConnection.CreateCommand())
            {
                command.CommandText = createTableSql;

                var name = command.ExecuteScalar();
                if (name != null && name.ToString() == tableName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Opens a database connection to a local database with the name of this database.
        /// </summary>
        internal void OpenDatabaseConnection()
        {
            this.DatabaseConnection = new SQLiteConnection($"Data Source={this.DatabaseName};Version=3;");
            this.DatabaseConnection.Open();
        }
    }
}