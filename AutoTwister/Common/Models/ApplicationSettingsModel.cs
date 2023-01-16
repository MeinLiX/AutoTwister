using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AutoTwister.Common.Models
{
    [Table(nameof(ApplicationSettingsModel))]
    public class ApplicationSettingsModel
    {
        [PrimaryKey]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(typeof(LocaleModel))]
        public string LocaleId { get; set; } = null;

        [OneToOne]
        public LocaleModel Locale { get; set; } = null;

        [OneToMany]
        public List<UserStatsModel> UserStats { get; set; } = new List<UserStatsModel>();

        public ApplicationSettingsModel()
        {
        }
    }
}

