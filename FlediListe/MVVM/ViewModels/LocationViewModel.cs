using System.Collections.ObjectModel;
using System.Windows.Input;
using FlediListe.MVVM.Commands;
using FlediListe.MVVM.Service;
using FlediListe.MVVM.Views;
using Location = FlediListe.MVVM.Models.Location;


namespace FlediListe.MVVM.ViewModels;

public class LocationViewModel : ViewModelBase
{
    private readonly ILocationService _locationService;
    
    private Models.Location _selectedLocation;
    public Models.Location? SelectedLocation
    {
        get => _selectedLocation;
        set => SetProperty(ref _selectedLocation, value);
    }

    private bool _isEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set => SetProperty(ref _isEditMode, value);
    }

    private string _locationName = string.Empty;
    public string LocationName
    {
        get => _locationName;
        set => SetProperty(ref _locationName, value);
    }

    public ObservableCollection<Location> Locations { get; } = new();
    
    public ICommand ReturnToMainPage { get; }
    public ICommand SetEditingMode { get; }
    public ICommand SaveNewLocation { get; }
    public ICommand DeleteLocation { get; }
    public ICommand UpdateLocation { get; }
    public ICommand TapItemCommand { get; }
    
    public LocationViewModel(ILocationService locationService)
    {
        
        _locationService = locationService ;
        
        ReturnToMainPage = new AsyncRelayCommand(NavigateToMainPage);
        SetEditingMode = new RelayCommand(EditingMode);
        SaveNewLocation = new AsyncRelayCommand<Location>(SaveNewLocationAsync);
        DeleteLocation = new AsyncRelayCommand<Location>(DeleteLocationAsync);
        UpdateLocation = new AsyncRelayCommand(UpdateLocationAsync);
        TapItemCommand = new RelayCommand<Location>(HandleSelectionAsync);

    }

    private void HandleSelectionAsync(Location? location)
    {
        SelectedLocation = location;
        if (!IsEditMode)
        {
            NavigateToLocation();
        }
    }

    private Task NavigateToLocation()
    {
        if(SelectedLocation is null) return Task.CompletedTask;
        return Shell.Current.GoToAsync($"{nameof(DatePage)}?locationId={SelectedLocation.Id}");
    }

    private async Task UpdateLocationAsync()
    {
        
    }

    private Task DeleteLocationAsync(Location? arg)
    {
        throw new NotImplementedException();
    }

    private async Task SaveNewLocationAsync(Location? arg)
    {
        if (!string.IsNullOrWhiteSpace(LocationName))
        {
            await _locationService.SaveAsync(new Location()
            {
                Id = Guid.NewGuid(),
                Name = LocationName
            });
            LocationName = string.Empty;
        }

        await InitializeAsync();
    }

    private void EditingMode()
    {
        
        IsEditMode = !IsEditMode;
        
    }

    private Task NavigateToMainPage()
    {
        return Shell.Current.GoToAsync("//MainPage");
    }

    public async Task InitializeAsync()
    {
        var locations = await _locationService.GetAsync();
        Locations.Clear();
        foreach (var location in locations)
        {
            Locations.Add(location);    
        }

        SelectedLocation = Locations.FirstOrDefault();
    }
    
}