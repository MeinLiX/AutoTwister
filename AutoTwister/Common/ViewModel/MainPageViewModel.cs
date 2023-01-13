using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AutoTwister.Common.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Microsoft.Maui.Graphics.Color background = Microsoft.Maui.Graphics.Color.FromArgb("#F2F3F4");

        public MainPageViewModel()
        {
        }
    }
}

