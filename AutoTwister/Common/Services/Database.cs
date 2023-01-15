using System;
using SQLite;
using AutoTwister.Common.Models;

namespace AutoTwister.Common.Services
{
    public class Database
    {
        private SQLiteAsyncConnection connection;

        public Database()
        {
        }

        private async Task InitialDataBase()
        {
            if (connection is not null)
                return;

            connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.SQLiteFlags);

            await connection.CreateTableAsync<ApplicationSettingsModel>();
            await connection.CreateTableAsync<LocaleModel>();
            await connection.CreateTableAsync<UserStatsModel>();
        }

        #region ApplicationSettings
        /// <summary>
        /// Get or create settings.
        /// Only one settings object can be in db.
        /// </summary>
        public async Task<ApplicationSettingsModel> GetApplicationSettings()
        {
            await InitialDataBase();

            ApplicationSettingsModel applicationSettings = await connection.Table<ApplicationSettingsModel>().FirstOrDefaultAsync();
            if (applicationSettings is not null)
            {
                return applicationSettings;
            }
            else
            {
                applicationSettings = new ApplicationSettingsModel();

                await connection.InsertAsync(applicationSettings);

                return applicationSettings;
            }
        }

        public async Task<ApplicationSettingsModel> SaveLocale(LocaleModel newlocale)
        {
            if (newlocale is null) throw new NullReferenceException(nameof(newlocale));
            if (string.IsNullOrEmpty(newlocale.Id))
            {
                newlocale.Id = Guid.NewGuid().ToString();
            }

            var appSettings = await GetApplicationSettings();

            if (appSettings.Locale is not null && !string.Equals(appSettings.Locale.Id, newlocale.Id))
            {
                await connection.DeleteAsync(appSettings.Locale);
            }

            await connection.InsertOrReplaceAsync(newlocale);

            appSettings.Locale = newlocale;
            await connection.UpdateAsync(appSettings);
            return appSettings;

        }

        public async Task<ApplicationSettingsModel> AddOrUpdateUser(UserStatsModel userModel)
        {
            if (userModel is null) throw new NullReferenceException(nameof(userModel));

            var appSettings = await GetApplicationSettings();

            await connection.InsertOrReplaceAsync(userModel);

            var userFounded = appSettings.UserStats.FirstOrDefault(u => u.Id == userModel.Id);

            if (userFounded is not null)
            {
                appSettings.UserStats.Remove(userFounded);
            }

            appSettings.UserStats.Append(userModel);
            await connection.UpdateAsync(appSettings);
            return appSettings;
        }

        public async Task<ApplicationSettingsModel> RemoveUser(UserStatsModel userModel)
        {
            if (userModel is null) throw new NullReferenceException(nameof(userModel));

            var appSettings = await GetApplicationSettings();

            var userFounded = appSettings.UserStats.FirstOrDefault(u => u.Id == userModel.Id);

            if (userFounded is null)
            {
                return appSettings;
            }

            appSettings.UserStats.Remove(userFounded);

            await connection.DeleteAsync(userModel);

            await connection.UpdateAsync(appSettings);
            return appSettings;
        }
        #endregion
    }
}

