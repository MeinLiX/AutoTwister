using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AutoTwister.Common.Models
{
    [Table(nameof(LocaleModel))]
    public class UserStatsModel
	{
        [PrimaryKey]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public int WinCount { get; set; } = 0;

        [ForeignKey(typeof(ApplicationSettingsModel))]
        public Guid ApplicationSettingsId { get; set; }

        public UserStatsModel()
		{
		}
	}
}

