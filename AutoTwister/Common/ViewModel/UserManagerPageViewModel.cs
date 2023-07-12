using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using AutoTwister.Common.Models;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;

namespace AutoTwister.Common.ViewModel
{
    public class UserManagerPageViewModel : BaseViewModel
    {

        public UserManagerPageViewModel()
        {
            AddUserCommand = new AsyncRelayCommand(AddUserExecuteAsync);
            RemoveUserCommand = new AsyncRelayCommand(RemoveUserExecuteAsync,
                new Func<bool>(() => this.IsUserSelected));
            UpdateCommand = new RelayCommand(UpdateExecute);
            UpdateCommand.Execute(null);
        }

        #region commands

        public RelayCommand UpdateCommand { get; private set; }

        private void UpdateExecute()
        {
            Debug.WriteLine($"[{nameof(UpdateCommand)}]");
            Users = new HashSet<UserStatsModel>((Database.GetApplicationSettings()).UserStats);

        }

        public AsyncRelayCommand AddUserCommand { get; set; }

        private async Task AddUserExecuteAsync()
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

        public AsyncRelayCommand RemoveUserCommand { get; private set; }

        private async Task RemoveUserExecuteAsync()
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

        public bool IsUserSelected => SelectedUser is not null;

        private UserStatsModel selectedUser;

        public UserStatsModel SelectedUser
        {
            get => this.selectedUser;
            set => SetProperty(ref this.selectedUser, value, nameof(IsUserSelected));
        }

        private HashSet<UserStatsModel> users;

        public HashSet<UserStatsModel> Users
        {
            get => this.users;
            set => SetProperty(ref this.users, value);
        }

        #endregion properties
    }
}

