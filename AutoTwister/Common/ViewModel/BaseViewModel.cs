using System;
using System.Windows.Input;
using AutoTwister.Common.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AutoTwister.Common.ViewModel
{

    public class BaseViewModel : ObservableObject
    {
        protected readonly Database Database;

        public ICommand GoRootPageCommand;

        public BaseViewModel()
        {
            Database = Ioc.Default.GetService<Database>();

            GoRootPageCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(Constants.Route.MainPage);
            });
        }
    }
}

