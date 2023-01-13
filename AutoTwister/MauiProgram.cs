using Microsoft.Extensions.Logging;
using AutoTwister.Common.ViewModel;
using AutoTwister.Common.View;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AutoTwister;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("ABeeZee-Regular.ttf", "ABeeZeeRegular");
                fonts.AddFont("ABeeZee-Semibold.ttf", "ABeeZeeSemibold");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif


        builder.Services.RegisterViewModels();



        var mauiApp = builder.Build();

        Ioc.Default.ConfigureServices(mauiApp.Services);

        return mauiApp;
    }

    private static void RegisterViewModels(this IServiceCollection services)
    {
        services.AddTransient<MainPageViewModel>();
    }
}

