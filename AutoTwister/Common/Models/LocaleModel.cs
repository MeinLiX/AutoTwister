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


        public string Pitch { get; set; }

        public string Volume { get; set; }


        [PrimaryKey]
        public string Id { get; set; }

        public LocaleModel()
        {
        }

        public LocaleModel(string language) : this(TextToSpeech.Default.GetLocalesAsync().Result.FirstOrDefault(l => l.Language == language))
        {

        }

        public LocaleModel(Locale locale)
        {
            if (locale is null)
            {
                throw new Exception("Selected locale not supported you device.");
            }

            Language = locale.Language;
            Country = locale.Country;
            Name = locale.Name;
            Id = locale.Id;
        }

        public async Task<Locale> GetLocale()
            => (await TextToSpeech.Default.GetLocalesAsync())
               .FirstOrDefault(l => l.Id == Id)
                ?? throw new Exception("Selected locale not supported you device.");
    }
}

