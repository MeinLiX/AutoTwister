using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AutoTwister.Common.Models
{
    [Table(nameof(LocaleModel))]
    public class UserStatsModel
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public int WinCount { get; set; } = 0;

        [ForeignKey(typeof(ApplicationSettingsModel))]
        public int ApplicationSettingsId { get; set; }

        public UserStatsModel()
		{
		}
	}
}

