using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using AutoTwister.Common.Models;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;

namespace AutoTwister.Common.ViewModel
{
    public partial class UserManagerPageViewModel : BaseViewModel
    {

        public UserManagerPageViewModel()
        {
            UpdateCommand.Execute(null);
        }

        #region commands

        [RelayCommand]
        private void Update()
        {
            Debug.WriteLine($"[{nameof(UpdateCommand)}]");
            Users = new HashSet<UserStatsModel>((Database.GetApplicationSettings()).UserStats);

        }

        [RelayCommand]
        private async Task AddUser()
        {
            Debug.WriteLine($"[{nameof(AddUserCommand)}]");
            string result = await Application.Current.MainPage.DisplayPromptAsync("Add User", "Enter user name.");
            if (string.IsNullOrEmpty(result))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Name can't be empty.", "Cancel");
            }
            else
            {
                Database.AddOrUpdateUser(new UserStatsModel { Name = result });
                UpdateCommand.Execute(null);
            }
        }

        //[RelayCommand(CanExecute = nameof(IsSelectedUser))] //CanExecute not work!!
        [RelayCommand]
        private async Task RemoveUser()
        {
            Debug.WriteLine($"[{nameof(RemoveUserCommand)}]");

            if (await Application.Current.MainPage.DisplayAlert("Delete", $"Remove {SelectedUser.Name} user?", "Remove", "Cancel"))
            {
                Database.RemoveUser(SelectedUser);
                UpdateCommand.Execute(null);
            }
        }

        #endregion commands

        #region properties

        public bool IsSelectedUser => SelectedUser is not null;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSelectedUser))]
        private UserStatsModel _selectedUser;

        [ObservableProperty]
        private HashSet<UserStatsModel> _users;

        #endregion properties
    }
}

