using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AutoTwister.Common.Models
{
    [Table(nameof(ApplicationSettingsModel))]
    public class ApplicationSettingsModel
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(LocaleModel))]
        public string LocaleId { get; set; }

        [OneToOne]
        public LocaleModel Locale { get; set; }

        [OneToMany]
        public List<UserStatsModel> UserStats { get; set; } = new List<UserStatsModel>();

        public ApplicationSettingsModel()
        {
        }
    }
}

