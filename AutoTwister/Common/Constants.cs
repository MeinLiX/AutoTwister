using System;
namespace AutoTwister.Common
{
    public static class Constants
    {
        public const string DatabaseFilename = "TAutoTwister.db3";

        public const SQLite.SQLiteOpenFlags SQLiteFlags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        public static class Route
        {
            public const string MainPage = $"//{nameof(View.MainPage)}";
            public const string GamePage = $"//{nameof(View.GamePage)}";
            public const string UserManagerPage = $"//{nameof(View.UserManagerPage)}";
            public const string LocalizationSettingPage = $"//{nameof(View.LocalizationSettingPage)}";
        }
    }
}

