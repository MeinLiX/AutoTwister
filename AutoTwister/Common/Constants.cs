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

        public static class Colors
        {
            public static Microsoft.Maui.Graphics.Color GetColorBG(ColorsEnum @enum) => @enum switch
            {
                ColorsEnum.Green => GreenBG,
                ColorsEnum.Yellow => YellowBG,
                ColorsEnum.Blue => BlueBG,
                ColorsEnum.Red => RedBG,
                ColorsEnum.Air => AirBG
            };
            public readonly static Microsoft.Maui.Graphics.Color GreenBG = Microsoft.Maui.Graphics.Color.FromArgb("#136207");
            public readonly static Microsoft.Maui.Graphics.Color YellowBG = Microsoft.Maui.Graphics.Color.FromArgb("#FADA5E");
            public readonly static Microsoft.Maui.Graphics.Color BlueBG = Microsoft.Maui.Graphics.Color.FromArgb("#4169E1");
            public readonly static Microsoft.Maui.Graphics.Color RedBG = Microsoft.Maui.Graphics.Color.FromArgb("#AB2330");
            public readonly static Microsoft.Maui.Graphics.Color AirBG = Microsoft.Maui.Graphics.Color.FromArgb("#F2F3F4");
        }

        public enum ColorsEnum
        {
            Green = 0,
            Yellow,
            Blue,
            Red,
            Air

        }
    }
}

