using System;
using System.Diagnostics;
using AutoTwister.Common.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AutoTwister.Common.View
{
    public abstract class BasePage<TVM> : BasePage where TVM : BaseViewModel
    {
        protected BasePage() : base(Ioc.Default.GetService<TVM>())
        {
        }
        protected BasePage(TVM viewModel) : base(viewModel)
        {
        }

        public new TVM BindingContext => (TVM)base.BindingContext;
    }

    public abstract class BasePage : ContentPage
    {
        protected BasePage(object viewModel = null)
        {
            BindingContext = viewModel;
            Padding = 4;

            if (string.IsNullOrWhiteSpace(Title))
            {
                Title = GetType().Name;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Debug.WriteLine($"OnAppearing: {Title}");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Debug.WriteLine($"OnDisappearing: {Title}");
        }
    }
}
