using System;
using SQLite;
using AutoTwister.Common.Models;
using SQLiteNetExtensions.Extensions;

namespace AutoTwister.Common.Services
{
    public class Database
    {
        private SQLiteConnection connection;

        public Database()
        {
        }

        private void InitialDataBase()
        {
            if (connection is not null)
                return;

            connection = new SQLiteConnection(Constants.DatabasePath, Constants.SQLiteFlags);
            connection.EnableWriteAheadLogging();

            connection.CreateTable<ApplicationSettingsModel>();
            connection.CreateTable<LocaleModel>();
            connection.CreateTable<UserStatsModel>();
        }

        #region ApplicationSettings
        /// <summary>
        /// Get or create settings.
        /// Only one settings object can be in db.
        /// </summary>
        public ApplicationSettingsModel GetApplicationSettings()
        {
            InitialDataBase();

            ApplicationSettingsModel applicationSettings = connection.GetAllWithChildren<ApplicationSettingsModel>().FirstOrDefault();
            if (applicationSettings is not null)
            {
                return applicationSettings;
            }
            else
            {
                applicationSettings = new ApplicationSettingsModel();

                connection.InsertOrReplace(applicationSettings);

                return applicationSettings;
            }
        }

        public ApplicationSettingsModel SaveLocale(LocaleModel newlocale)
        {
            if (newlocale is null) throw new NullReferenceException(nameof(newlocale));
            if (string.IsNullOrEmpty(newlocale.Id))
            {
                newlocale.Id = Guid.NewGuid().ToString();
            }

            var appSettings = GetApplicationSettings();

            if (appSettings.Locale is not null && !string.Equals(appSettings?.Locale?.Id, newlocale?.Id))
            {
                connection.Delete(appSettings.Locale);
            }

            connection.InsertOrReplace(newlocale);
            appSettings.LocaleId = newlocale.Id;
            appSettings.Locale = newlocale;
            connection.Update(appSettings);
            return appSettings;

        }

        public ApplicationSettingsModel AddOrUpdateUser(UserStatsModel userModel)
        {
            if (userModel is null) throw new NullReferenceException(nameof(userModel));

            var appSettings = GetApplicationSettings();

            userModel.ApplicationSettingsId = appSettings.Id;

            connection.InsertOrReplace(userModel);

            var userFounded = appSettings.UserStats.FirstOrDefault(u => u.Id == userModel.Id);

            if (userFounded is not null)
            {
                appSettings.UserStats.Remove(userFounded);
                connection.Delete(userFounded);
            }

            appSettings.UserStats.Append(userModel);
            connection.Update(appSettings);
            return appSettings;
        }

        public ApplicationSettingsModel RemoveUser(UserStatsModel userModel)
        {
            if (userModel is null) throw new NullReferenceException(nameof(userModel));

            var appSettings = GetApplicationSettings();

            var userFounded = appSettings.UserStats.FirstOrDefault(u => u.Id == userModel.Id);

            if (userFounded is null)
            {
                return appSettings;
            }

            appSettings.UserStats.Remove(userFounded);
            connection.UpdateWithChildren(appSettings);
            return appSettings;
        }
        #endregion
    }
}

