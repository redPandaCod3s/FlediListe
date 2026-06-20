using FlediListe.MVVM.Views;

namespace FlediListe;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(LocationPage), typeof(LocationPage));
        Routing.RegisterRoute(nameof(DatePage), typeof(DatePage));
        Routing.RegisterRoute(nameof(DateFormPage), typeof(DateFormPage));
        Routing.RegisterRoute(nameof(DateDetailPage), typeof(DateDetailPage));
        Routing.RegisterRoute(nameof(FileEntryFormPage), typeof(FileEntryFormPage));
    }
}