using System;
using System.Diagnostics;
using System.Windows.Input;
using AutoTwister.Common.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

namespace AutoTwister.Common.ViewModel
{

    public class BaseViewModel : ObservableObject
    {
        protected readonly Database Database;

        public BaseViewModel()
        {
            Database = Ioc.Default.GetService<Database>();
            GoRootPageCommand = new AsyncRelayCommand(GoRootPageExecuteAsync);
        }

        public AsyncRelayCommand GoRootPageCommand { get; private set; }

        private async Task GoRootPageExecuteAsync()
        {
            Debug.WriteLine($"[{nameof(GoRootPageCommand)}]");
            await Shell.Current.GoToAsync(Constants.Route.MainPage);
        }
    }
}

