using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AutoTwister.Common.ViewModel
{
    public partial class LocalizationSettingPageViewModel : BaseViewModel
    {
        private IEnumerable<Locale> _avaliableLocales=new ObservableCollection<Locale>();
        public ICommand SaveCommand { get; }
        public ICommand RunTestSpeechCommand { get; }
        public ICommand UpdateAndResetCommand { get; }
        public LocalizationSettingPageViewModel() : base()
        {
            SaveCommand = new Command(async () =>
            {
                Debug.WriteLine($"[{nameof(SaveCommand)}]");
                await Application.Current.MainPage.DisplayAlert("Warn!", "Save function not realised!", "Ok");
            });

            RunTestSpeechCommand = new Command(async () =>
            {
                Debug.WriteLine($"[{nameof(RunTestSpeechCommand)}]");
                await TextToSpeech.Default.SpeakAsync(TextForTestSpeech, new SpeechOptions()
                {
                    Pitch = Pitch,
                    Volume = Volume,
                    Locale = SelectedLocale
                });
            });

            UpdateAndResetCommand = new Command(() =>
            {
                Debug.WriteLine($"[{nameof(UpdateAndResetCommand)}]");
                Task loadAsyncFields = new Task(async () =>
                {
                    _avaliableLocales = await TextToSpeech.Default.GetLocalesAsync();
                    OnPropertyChanged(nameof(LocalesWithFilter));
                });
                loadAsyncFields.Start();

                Pitch = 1f;
                Volume = 1f;
            });
            UpdateAndResetCommand.Execute(null);
        }

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
    }
}

