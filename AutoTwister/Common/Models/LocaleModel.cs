using System;
using System.Runtime.CompilerServices;
using SQLite;

namespace AutoTwister.Common.Models
{
    [Table(nameof(LocaleModel))]
    public class LocaleModel
    {
        public string Language { get; set; }

        public string Country { get; set; }

        public string Name { get; set; }

        [PrimaryKey]
        public string Id { get; set; }

        public LocaleModel()
        {
        }

        public LocaleModel(Locale locale)
        {
            Language = locale.Language;
            Country = locale.Country;
            Name = locale.Name;
            Id = locale.Id;
        }

        public async Task<Locale> GetLocale()
            => (await TextToSpeech.GetLocalesAsync()).First(l => l.Id == Id);


    }
}

