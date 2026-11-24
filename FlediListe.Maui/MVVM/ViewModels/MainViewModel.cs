using System.Collections.ObjectModel;
using System.Windows.Input;
using FlediListe.Maui.MVVM.Commands;
using FlediListe.Maui.MVVM.Service;
using FlediListe.Maui.MVVM.Views;
using Location = FlediListe.Maui.MVVM.Models.Location;

namespace FlediListe.Maui.MVVM.ViewModels;

public class MainViewModel: ViewModelBase
{
    ILocationService _locationService;
    
    private int _locationsCount;
    public int LocationsCount
    {
        get => _locationsCount;
        private set => SetProperty(ref _locationsCount, value);
    }

    public ObservableCollection<Location> Locations { get; } = new ();
    
    public ICommand OnBorderTapped { get; }
    
    public MainViewModel(ILocationService locationService)
    {
        _locationService = locationService;
        
        OnBorderTapped = new AsyncRelayCommand(NavigateToLocations);
    }

    private Task NavigateToLocations()
    {
        return Shell.Current.GoToAsync(nameof(LocationPage));
    }
    
    public async Task InitializeAsync()
    {
        var locations = await _locationService.GetAsync();
        Locations.Clear();
        
        foreach (var location in locations)
        {
            if(location is not null)
                Locations.Add(location);
        }
        
        LocationsCount = Locations.Count;
    }
    
    
}