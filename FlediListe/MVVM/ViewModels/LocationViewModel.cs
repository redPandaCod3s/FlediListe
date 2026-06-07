using System.Collections.ObjectModel;
using System.Windows.Input;
using FlediListe.MVVM.Commands;
using FlediListe.MVVM.Views;


namespace FlediListe.MVVM.ViewModels;

public class LocationViewModel : ViewModelBase
{
    
    
    private Location? _selectedLocation;
    public Location? SelectedLocation
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
    public ICommand SaveLocation { get; }
    public ICommand TapItemCommand { get; }
    
    public LocationViewModel()
    {
        
        //_service = service;
        
        ReturnToMainPage = new AsyncRelayCommand(NavigateToMainPage);
        SetEditingMode = new RelayCommand(EditingMode);
        SaveNewLocation = new AsyncRelayCommand<Location>(SaveNewLocationAsync);
        DeleteLocation = new AsyncRelayCommand<Location>(DeleteLocationAsync);
        SaveLocation = new AsyncRelayCommand(SaveLocationAsync);
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
        return Shell.Current.GoToAsync($"{nameof(DatePage)}?locationId={SelectedLocation}");
    }

    private Task SaveLocationAsync()
    {
        throw new NotImplementedException();
    }

    private Task DeleteLocationAsync(Location? arg)
    {
        throw new NotImplementedException();
    }

    private Task SaveNewLocationAsync(Location? arg)
    {
        throw new NotImplementedException();
    }

    private void EditingMode()
    {
        
        IsEditMode = !IsEditMode;
        
    }

    private Task NavigateToMainPage()
    {
        return Shell.Current.GoToAsync("//MainPage");
    }

    public void Refresh()
    {
        
    }
    
}