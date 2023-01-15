using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using AutoTwister.Common.Models;
using System.Diagnostics;

namespace AutoTwister.Common.ViewModel
{
    public partial class UserManagerPageViewModel : BaseViewModel
    {
        public ICommand AddUserCommand { get; }
        public ICommand RemoveUserCommand { get; }
        public ICommand UpdateCommand { get; }

        public UserManagerPageViewModel()
        {

            AddUserCommand = new Command(() =>
            {
                Debug.WriteLine($"[{nameof(AddUserCommand)}]");
            });

            RemoveUserCommand = new Command(async () =>
            {
                string acceptbtn = "Remove";
                string cancelbtn = "Cancel";
                string action = await Application.Current.MainPage.DisplayPromptAsync("Delete", $"Remove {SelectedUser.Name} user?", acceptbtn, cancelbtn);

                Debug.WriteLine($"[{nameof(RemoveUserCommand)}] Action: {action};");

                if (string.Equals(action, acceptbtn))
                {
                    await Database.RemoveUser(SelectedUser);
                }
            }, () => IsSelectedUser);

            UpdateCommand = new Command(async () =>
            {
                Debug.WriteLine($"[{nameof(UpdateCommand)}]");
                Users = new HashSet<UserStatsModel>((await Database.GetApplicationSettings()).UserStats);
            });

            UpdateCommand.Execute(null);
        }

        public bool IsSelectedUser { get => SelectedUser is not null; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSelectedUser))]
        private UserStatsModel _selectedUser;

        [ObservableProperty]
        private HashSet<UserStatsModel> _users;
    }
}

