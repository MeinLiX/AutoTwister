using System;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AutoTwister.Common.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Microsoft.Maui.Graphics.Color background = Microsoft.Maui.Graphics.Color.FromArgb("#F2F3F4");

        public ICommand OpenUserManagerPageCommand { get; }
        public ICommand OpenLocalizationSettingPageCommand { get; }

        public MainPageViewModel():base()
        {
            OpenUserManagerPageCommand = new Command(async () =>
            {
                Debug.WriteLine($"[{nameof(OpenUserManagerPageCommand)}]");
                await Shell.Current.GoToAsync(Constants.Route.UserManagerPage);
            });

            OpenLocalizationSettingPageCommand = new Command(async () =>
            {
                Debug.WriteLine($"[{nameof(OpenLocalizationSettingPageCommand)}]");
                await Shell.Current.GoToAsync(Constants.Route.LocalizationSettingPage);
            });
            
        }

        
    }
}

