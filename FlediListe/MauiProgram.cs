using FlediListe.MVVM.Models;
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
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<IFileEntryService, SqliteFileEntryService>();
        builder.Services.AddSingleton<ILocationDateService>( sp =>
            new SqliteLocationDateService(
                sp.GetRequiredService<DatabaseService>(),
                sp.GetRequiredService<IFileEntryService>()));
        builder.Services.AddSingleton<ILocationService>( SP =>
            new SqliteLocationService(
                SP.GetRequiredService<DatabaseService>(),
                SP.GetRequiredService<ILocationDateService>()));
        builder.Services.AddSingleton<IExportService, CsvExportService>();
        
        // ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<LocationViewModel>();
        
        builder.Services.AddTransient<DatePageViewModel>();
        builder.Services.AddTransient<DateFormViewModel>();
        builder.Services.AddTransient<DateDetailViewModel>(sp =>
            new DateDetailViewModel(
                sp.GetRequiredService<IFileEntryService>(),
                sp.GetRequiredService<ILocationDateService>(),
                sp.GetRequiredService<IExportService>(),
                sp.GetRequiredService<ILocationService>()
                )
        );
        builder.Services.AddTransient<FileEntryFormViewModel>();
        
        // Pages
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LocationPage>();
        
        builder.Services.AddTransient<DatePage>();
        builder.Services.AddTransient<DateFormPage>();
        builder.Services.AddTransient<DateDetailPage>();
        builder.Services.AddTransient<FileEntryFormPage>();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}