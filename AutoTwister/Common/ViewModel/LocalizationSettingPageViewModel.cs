using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutoTwister.Common.ViewModel
{
    public partial class LocalizationSettingPageViewModel : BaseViewModel
    {
        private IEnumerable<Locale> _avaliableLocales = new ObservableCollection<Locale>();
        public LocalizationSettingPageViewModel() : base()
        {
            UpdateAndResetCommand.Execute(null);
        }

        #region commands

        [RelayCommand]
        private async Task Save()
        {
            Debug.WriteLine($"[{nameof(SaveCommand)}]");
            if (IsSelectedLocale)
            {
                Database.SaveLocale(new Models.LocaleModel(SelectedLocale) { Pitch = this.Pitch, Volume = this.Volume });
                await Application.Current.MainPage.DisplayAlert("Result!", "Save is done!", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fail!", "need select locale!", "OK");
            }
        }

        [RelayCommand]
        private async Task RunTestSpeech()
        {
            Debug.WriteLine($"[{nameof(RunTestSpeechCommand)}]");
            await TextToSpeech.Default.SpeakAsync(TextForTestSpeech, new SpeechOptions()
            {
                Pitch = Pitch,
                Volume = Volume,
                Locale = SelectedLocale
            });
        }

        [RelayCommand]
        private async Task UpdateAndReset()
        {
            Debug.WriteLine($"[{nameof(UpdateAndResetCommand)}]");

            _avaliableLocales = await TextToSpeech.Default.GetLocalesAsync();
            OnPropertyChanged(nameof(LocalesWithFilter));

            var appSettings = Database.GetApplicationSettings();
            if (appSettings.Locale is not null)
            {
                Pitch = appSettings.Locale.Pitch;
                Volume = appSettings.Locale.Volume;
                SelectedLocale = await appSettings.Locale.GetLocale(); //Перевірити, чомусь не працює збережений голос
            }
            else
            {
                Pitch = 1f;
                Volume = 1f;
            }
        }

        #endregion commands

        #region properties

        [ObservableProperty]
        private string _textForTestSpeech = string.Empty;

        //0.0-2.0
        [ObservableProperty]
        private float _pitch = 1f;

        //0.0-1.0
        [ObservableProperty]
        private float _volume = 1f;

        public bool IsSelectedLocale { get => SelectedLocale is not null; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSelectedLocale))]
        [NotifyPropertyChangedFor(nameof(TextForTestSpeech))]
        private Locale _selectedLocale = null;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LocalesWithFilter))]
        private string _filterLocale = string.Empty;

        public HashSet<Locale> LocalesWithFilter
        {
            get
            {
                if (string.IsNullOrEmpty(FilterLocale))
                {
                    return new HashSet<Locale>(_avaliableLocales);
                }
                else
                {
                    return new HashSet<Locale>(_avaliableLocales
                                       .Where(locale =>
                                       {
                                           string filterForSearch = FilterLocale.ToLower();
                                           return (locale.Id?.ToLower()?.Contains(filterForSearch) ?? false) ||
                                                  (locale.Country?.ToLower()?.Contains(filterForSearch) ?? false) ||
                                                  (locale.Language?.ToLower()?.Contains(filterForSearch) ?? false) ||
                                                  (locale.Name?.ToLower()?.Contains(filterForSearch) ?? false);
                                       }
                                       /*string.Equals(locale.Id, FilterLocale, StringComparison.OrdinalIgnoreCase) ||
                                       string.Equals(locale.Country, FilterLocale, StringComparison.OrdinalIgnoreCase) ||
                                       string.Equals(locale.Language, FilterLocale, StringComparison.OrdinalIgnoreCase) ||
                                       string.Equals(locale.Name, FilterLocale, StringComparison.OrdinalIgnoreCase)*/
                                       ));
                }
            }
        }

        #endregion properties
    }
}

