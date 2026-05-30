using System.Windows.Input;
using FlediListe.MVVM.Commands;
using FlediListe.MVVM.Views;

namespace FlediListe.MVVM.ViewModels;

public class MainViewModel: ViewModelBase
{
    
    private int _locationsCount;

    public int LocationsCount
    {
        get => _locationsCount;
        private set => SetProperty(ref _locationsCount, value);
    }
    
    public ICommand OnBorderTapped { get; }

    public MainViewModel()
    {
        OnBorderTapped = new AsyncRelayCommand(NavigateToLocations);
    }

    private Task NavigateToLocations()
    {
        return Shell.Current.GoToAsync(nameof(LocationPage));
    }
    
    public void Refresh()
    {
        
    }
    
    
}