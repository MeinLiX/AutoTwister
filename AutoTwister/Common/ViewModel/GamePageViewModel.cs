using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutoTwister.Common.ViewModel
{
    public partial class GamePageViewModel : BaseViewModel
    {
        public GamePageViewModel() : base()
        {
        }

        #region commands

        [RelayCommand]
        private async Task NextStepPage()
        {
            Debug.WriteLine($"[{nameof(NextStepPageCommand)}]");

            //ForTest
            Background = (new Random()).Next(0, 9) switch
            {
                0 or 1 => Constants.Colors.GreenBG,
                2 or 3 => Constants.Colors.YellowBG,
                4 or 5 => Constants.Colors.BlueBG,
                6 or 7 => Constants.Colors.RedBG,
                _ => Constants.Colors.AirBG
            };
        }

        #endregion commands

        #region properties

        [ObservableProperty]
        private Microsoft.Maui.Graphics.Color background = Microsoft.Maui.Graphics.Color.FromArgb("#F2F3F4");

        private Timer _timer;//TODO

        #endregion properties
    }
}

