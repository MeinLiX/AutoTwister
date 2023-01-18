using System;
using System.Runtime.CompilerServices;
using SQLite;

namespace AutoTwister.Common.Models
{
    [Table(nameof(LocaleModel))]
    public class LocaleModel
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Language { get; set; }

        public string Country { get; set; }

        public string Name { get; set; }


        public float Pitch { get; set; } = 1.0f;

        public float Volume { get; set; } = 1.0f;


        public LocaleModel()
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

        //someone OS prop 'Id' possible null. (auto gen guid for save in db)
        public async Task<Locale> GetLocale()
            => (await TextToSpeech.Default.GetLocalesAsync())
               .FirstOrDefault(l => (l.Id == Id) || (l.Language == Language && l.Country == Country && l.Name == Name))
                ?? throw new Exception("Selected locale not supported you device.");
    }
}

