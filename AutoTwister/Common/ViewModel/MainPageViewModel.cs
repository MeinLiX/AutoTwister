using System;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutoTwister.Common.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {

        public MainPageViewModel() : base()
        {
            UpdateCommand.Execute(null);
        }

        #region commands

        [RelayCommand]
        private async Task Update()
        {
            Debug.WriteLine($"[{nameof(UpdateCommand)}]");
            await Perms.RequestPermissions();
            _ = Database.GetApplicationSettings();
        }

        [RelayCommand]
        private async Task OpenUserManagerPage()
        {
            Debug.WriteLine($"[{nameof(OpenUserManagerPageCommand)}]");
            await Shell.Current.GoToAsync(Constants.Route.UserManagerPage);

        }

        [RelayCommand]
        private async Task OpenLocalizationSettingPage()
        {
            Debug.WriteLine($"[{nameof(OpenLocalizationSettingPageCommand)}]");
            await Shell.Current.GoToAsync(Constants.Route.LocalizationSettingPage);
        }

        #endregion commands

        #region properties

        [ObservableProperty]
        private Microsoft.Maui.Graphics.Color background = Microsoft.Maui.Graphics.Color.FromArgb("#F2F3F4");

        #endregion properties
    }
}

