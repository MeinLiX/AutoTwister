using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutoTwister.Common.ViewModel
{
    public class GamePageViewModel : BaseViewModel
    {
        public GamePageViewModel() : base()
        {
            NextStepPageCommand = new AsyncRelayCommand(NextStepPageExecuteAsync);
        }

        #region commands

        public AsyncRelayCommand NextStepPageCommand { get; private set; }

        private Task NextStepPageExecuteAsync()
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

            return Task.CompletedTask;
        }

        #endregion commands

        #region properties

        private Color background = Microsoft.Maui.Graphics.Color.FromArgb("#F2F3F4");

        public Color Background
        {
            get => this.background;
            set => SetProperty(ref this.background, value);
        }

        private Timer _timer;//TODO

        #endregion properties
    }
}

