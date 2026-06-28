using System.Collections.ObjectModel;
using System.Windows.Input;
using FlediListe.MVVM.Commands;
using FlediListe.MVVM.Service;
using FlediListe.MVVM.Views;
using Location = FlediListe.MVVM.Models.Location;

namespace FlediListe.MVVM.ViewModels;

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