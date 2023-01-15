using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using AutoTwister.Common.Models;

namespace AutoTwister.Common.ViewModel
{
    public partial class UserManagerPageViewModel : BaseViewModel
    {
        public ICommand AddUserCommand { get; }
        public ICommand RemoveUserCommand { get; }

        public UserManagerPageViewModel()
        {
            AddUserCommand = new Command(() =>
            {

            });

            RemoveUserCommand = new Command(() =>
            {

            });
        }

        public bool IsSelectedUser { get => SelectedUser is not null; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSelectedUser))]
        private UserStatsModel _selectedUser;

        [ObservableProperty]
        private List<UserStatsModel> _users;
    }
}

