using System.Collections.ObjectModel;
using System.Windows.Input;
using FlediListe.Maui.MVVM.Commands;
using FlediListe.Maui.MVVM.Service;
using FlediListe.Maui.MVVM.Views;
using Location = FlediListe.Maui.MVVM.Models.Location;
using Models_Location = FlediListe.Maui.MVVM.Models.Location;


namespace FlediListe.Maui.MVVM.ViewModels;

public class LocationViewModel : ViewModelBase
{
    private readonly ILocationService _locationService;
    
    private Models.Location? _selectedLocation;
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

    public ObservableCollection<Models_Location> Locations { get; } = new();
    
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
        SaveNewLocation = new AsyncRelayCommand<Models_Location>(SaveNewLocationAsync);
        DeleteLocation = new AsyncRelayCommand<Models_Location>(DeleteLocationAsync);
        UpdateLocation = new AsyncRelayCommand(UpdateLocationAsync);
        TapItemCommand = new RelayCommand<Models_Location>(HandleSelectionAsync);

    }

    private void HandleSelectionAsync(Models_Location? location)
    {
        SelectedLocation = location;
        if (IsEditMode)
        {
            LocationName = location?.Name ??  string.Empty;
        }
        else
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
        if(SelectedLocation is null) return;

        var locationToUpdate = SelectedLocation;
        
        //Namen aktualisieren
        locationToUpdate.Name = LocationName;
        
        //Speichern
        await _locationService.SaveAsync(locationToUpdate);
        
        // Liste neu laden
        await InitializeAsync();
        
        // die gerade aktualisierte Location wieder auswählen und anzeigen
        SelectedLocation = Locations.FirstOrDefault(l => l.Id ==  locationToUpdate.Id);
        LocationName = string.Empty;
    }

    private async Task DeleteLocationAsync(Models_Location? location)
    {
        if (location is null) return;

        bool confirm = await Shell.Current.DisplayAlertAsync(
            "Löschen bestätigen",
            $"Möchtest du den Standort '{location.Name}' wirklich löschen?",
            "Ja, löschen",
            "Abbrechen");

        if (!confirm) return;
        
        await _locationService.DeleteAsync(location);
        await InitializeAsync();
    }

    private async Task SaveNewLocationAsync(Models_Location? arg)
    {
        if (!string.IsNullOrWhiteSpace(LocationName))
        {
            await _locationService.SaveAsync(new Models_Location()
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
        return Shell.Current.GoToAsync("..");
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