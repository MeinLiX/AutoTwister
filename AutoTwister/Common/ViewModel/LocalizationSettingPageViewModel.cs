using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutoTwister.Common.ViewModel
{
    public class LocalizationSettingPageViewModel : BaseViewModel
    {
        private IEnumerable<Locale> _avaliableLocales = new ObservableCollection<Locale>();
        public LocalizationSettingPageViewModel() : base()
        {
            SaveCommand = new AsyncRelayCommand(SaveExecuteAsync);
            RunTestSpeechCommand = new AsyncRelayCommand(RunTestSpeechExecuteAsync);
            UpdateAndResetCommand = new AsyncRelayCommand(UpdateAndResetExecuteAsync);
            UpdateAndResetCommand.Execute(null);
        }

        #region commands

        public AsyncRelayCommand SaveCommand { get; private set; }

        private async Task SaveExecuteAsync()
        {
            Debug.WriteLine($"[{nameof(SaveCommand)}]");

            if (await Application.Current.MainPage.DisplayAlert("Save", $"Save locale {SelectedLocale.Language}?", "Save", "Cancel"))
            {
                Database.SaveLocale(new Models.LocaleModel(SelectedLocale) { Pitch = this.Pitch, Volume = this.Volume });
                await Application.Current.MainPage.DisplayAlert("Result!", "Save is done!", "OK");
                FilterLocale = string.Empty;
                GoRootPageCommand.Execute(null);
            }
        }

        public AsyncRelayCommand RunTestSpeechCommand { get; private set; }

        private async Task RunTestSpeechExecuteAsync()
        {
            Debug.WriteLine($"[{nameof(RunTestSpeechCommand)}]");
            await TextToSpeech.Default.SpeakAsync(TextForTestSpeech, new SpeechOptions()
            {
                Pitch = Pitch,
                Volume = Volume,
                Locale = SelectedLocale
            });
        }

        public AsyncRelayCommand UpdateAndResetCommand { get; private set; }

        private async Task UpdateAndResetExecuteAsync()
        {
            Debug.WriteLine($"[{nameof(UpdateAndResetCommand)}]");

            _avaliableLocales = await TextToSpeech.Default.GetLocalesAsync();
            OnPropertyChanged(nameof(LocalesWithFilter));

            var appSettings = Database.GetApplicationSettings();
            if (appSettings.Locale is not null)
            {
                Pitch = appSettings.Locale.Pitch;
                Volume = appSettings.Locale.Volume;
                SelectedLocale = await appSettings.Locale.GetLocale(); //Перевірити, чомусь не працює збережений голос, поки не жмякнути кнопку апдейт
            }
            else
            {
                Pitch = 1f;
                Volume = 1f;
            }
        }

        #endregion commands

        #region properties

        private string textForTestSpeech = string.Empty;

        public string TextForTestSpeech
        {
            get => this.textForTestSpeech;
            set => SetProperty(ref this.textForTestSpeech, value);
        }

        //0.0-2.0
        private float pitch = 1f;

        public float Pitch
        {
            get => this.pitch;
            set => SetProperty(ref this.pitch, value);
        }

        //0.0-1.0
        private float volume = 1f;

        public float Volume
        {
            get => this.volume;
            set => SetProperty(ref this.volume, value);
        }

        public bool IsLocaleSelected => SelectedLocale is not null; 

        private Locale selectedLocale = null;

        public Locale SelectedLocale
        {
            get => this.selectedLocale;
            set
            {
                SetProperty(ref this.selectedLocale, value);
                base.OnPropertyChanged(nameof(IsLocaleSelected));
                base.OnPropertyChanged(nameof(TextForTestSpeech));
            }
        }

        private string filterLocale = string.Empty;

        public string FilterLocale
        {
            get => this.filterLocale;
            set => SetProperty(ref this.filterLocale, value, nameof(LocalesWithFilter));
        }

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

