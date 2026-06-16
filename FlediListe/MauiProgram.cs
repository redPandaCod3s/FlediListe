using FlediListe.MVVM.Service;
using FlediListe.MVVM.ViewModels;
using FlediListe.MVVM.Views;
using Microsoft.Extensions.Logging;

namespace FlediListe;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("RobotoMono-Regular.ttf", "RobotoMono");
            });
        
        // Services
        builder.Services.AddSingleton<ILocationService, DummyLocationService>();
        
        // ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<LocationViewModel>();
        builder.Services.AddTransient<DatePageViewModel>();
        builder.Services.AddTransient<DateFormViewModel>();
        builder.Services.AddTransient<DateDetailViewModel>();
        
        // Pages
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LocationPage>();
        builder.Services.AddTransient<DatePage>();
        builder.Services.AddTransient<DateFormPage>();
        builder.Services.AddTransient<DateDetailPage>();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}