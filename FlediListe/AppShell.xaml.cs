using FlediListe.MVVM.Views;

namespace FlediListe;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(LocationPage), typeof(LocationPage));
        Routing.RegisterRoute(nameof(LocationDetailPage), typeof(LocationDetailPage));
        Routing.RegisterRoute(nameof(DatePage), typeof(DatePage));
    }
}