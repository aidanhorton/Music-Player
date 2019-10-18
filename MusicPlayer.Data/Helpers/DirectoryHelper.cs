using System;

namespace MusicPlayer.Data.Helpers
{
    public static class DirectoryHelper
    {
        public static string MusicDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
    }
}
