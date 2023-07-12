using System;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutoTwister.Common.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {

        public MainPageViewModel() : base()
        {
            OpenGamePageCommand = new AsyncRelayCommand(OpenGamePageExecuteAsync);
            OpenUserManagerPageCommand = new AsyncRelayCommand(OpenUserManagerPageExecuteAsync);
            OpenLocalizationSettingPageCommand = new AsyncRelayCommand(OpenLocalizationSettingPageExecuteAsync);
            UpdateCommand = new AsyncRelayCommand(UpdateExecuteAsync);
            UpdateCommand.Execute(null);
        }

        #region commands

        public AsyncRelayCommand UpdateCommand { get; private set; }

        private async Task UpdateExecuteAsync()
        {
            Debug.WriteLine($"[{nameof(UpdateCommand)}]");
            await Perms.RequestPermissions();
            _ = Database.GetApplicationSettings();
        }

        public AsyncRelayCommand OpenGamePageCommand { get; private set; }

        private async Task OpenGamePageExecuteAsync()
        {
            Debug.WriteLine($"[{nameof(OpenGamePageCommand)}]");
            await Shell.Current.GoToAsync(Constants.Route.GamePage);

        }

        public AsyncRelayCommand OpenUserManagerPageCommand { get; private set; }

        private async Task OpenUserManagerPageExecuteAsync()
        {
            Debug.WriteLine($"[{nameof(OpenUserManagerPageCommand)}]");
            await Shell.Current.GoToAsync(Constants.Route.UserManagerPage);

        }

        public AsyncRelayCommand OpenLocalizationSettingPageCommand { get; private set; }

        private async Task OpenLocalizationSettingPageExecuteAsync()
        {
            Debug.WriteLine($"[{nameof(OpenLocalizationSettingPageCommand)}]");
            await Shell.Current.GoToAsync(Constants.Route.LocalizationSettingPage);
        }

        #endregion commands

    }
}

