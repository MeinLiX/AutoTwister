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
        /// Get or create settings
        /// </summary>
        public async Task<ApplicationSettingsModel> InitialApplicationSettings()
        {
            await InitialDataBase();

            ApplicationSettingsModel applicationSettings = await connection.Table<ApplicationSettingsModel>().FirstOrDefaultAsync();
            if(applicationSettings is not null)
            {
                return applicationSettings;
            }
            else
            {
                applicationSettings = new ApplicationSettingsModel();

                LocaleModel locale = new LocaleModel();

                //TODO

                return applicationSettings;
            }
        }
        #endregion
    }
}

