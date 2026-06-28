using System.Collections.ObjectModel;
using System.Windows.Input;
using FlediListe.MVVM.Commands;
using FlediListe.MVVM.Models;
using FlediListe.MVVM.Service;
using FlediListe.MVVM.Views;
using Location = FlediListe.MVVM.Models.Location;

namespace FlediListe.MVVM.ViewModels;

[QueryProperty(nameof(LocationId),"locationId")]
public class DatePageViewModel : ViewModelBase
{
    private readonly ILocationService _locationService;
    private readonly ILocationDateService _locationDateService;

    private string? _locationId = string.Empty;
    public string? LocationId
    {
        get => _locationId;
        set => SetProperty(ref _locationId, value, async () => await InitializeAsync());
    }

    private LocationDate? _selectedLocationDate;
    public LocationDate? SelectedLocationDate
    {
        get => _selectedLocationDate;
        set => SetProperty(ref _selectedLocationDate, value);
    }
    
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

    public ObservableCollection<LocationDate> LocationDates { get; } = new();
    
    public ICommand ReturnToLocationPage { get; }
    public ICommand SetEditingMode { get; }
    public ICommand SaveNewDate { get; }
    public ICommand DeleteDate { get; }
    public ICommand UpdateDate { get; }
    public ICommand TapItemCommand { get; }

    public DatePageViewModel(ILocationDateService locationDateService, ILocationService locationService)
    {
        _locationService = locationService;
        _locationDateService = locationDateService;
        
        ReturnToLocationPage = new AsyncRelayCommand(NavigateToLocationPage);
        SetEditingMode = new RelayCommand(() => IsEditMode = !IsEditMode);
        SaveNewDate = new AsyncRelayCommand(NavigateToDateForm);
        DeleteDate = new AsyncRelayCommand<LocationDate>(DeleteDateAsync);
        UpdateDate = new AsyncRelayCommand(UpdateDateAsync);
        TapItemCommand = new RelayCommand<LocationDate>(HandleSelection);
    }

    private void HandleSelection(LocationDate? locationDate)
    {
        if(locationDate is null) return;
        
        SelectedLocationDate = locationDate;
        
        if (IsEditMode)
        {
            NavigateToDateFormEdit(locationDate);
        }
        else
        {
            NavigateToDateDetail();
        }
    }

    private Task NavigateToDateFormEdit(LocationDate locationDate)
    {
        return Shell.Current.GoToAsync($"{nameof(DateFormPage)}?locationId={LocationId}&locationDateId={locationDate.Id}");
    }

    private Task NavigateToDateDetail()
    {
        if(SelectedLocationDate is null) return Task.CompletedTask;
        
        return Shell.Current.GoToAsync($"{nameof(DateDetailPage)}?locationDateId={SelectedLocationDate.Id}");
    }

    private Task NavigateToDateForm()
    {
        return Shell.Current.GoToAsync($"{nameof(DateFormPage)}?locationId={LocationId}");
    }

    private async Task DeleteDateAsync(LocationDate? locationDate)
    {
        if (locationDate is null) return;
        await _locationDateService.DeleteAsync(locationDate);
        await InitializeAsync();
    }

    private async Task UpdateDateAsync()
    {
        if (SelectedLocationDate is null) return;
        await _locationDateService.SaveAsync(SelectedLocationDate);
        await InitializeAsync();
    }

    private Task NavigateToLocationPage()
    {
        return Shell.Current.GoToAsync("..");
    }

    public async Task InitializeAsync()
    {
        if (string.IsNullOrWhiteSpace(LocationId)) return;
        
        var locationId = Guid.Parse(LocationId);
        
        // Location laden für den Header
        SelectedLocation = await _locationService.GetByIdAsync(locationId);
        
        // LocationDates laden
        var locationDates = await _locationDateService.GetByLocationIdAsync(locationId);
        LocationDates.Clear();
        foreach (var locationDate in locationDates)
        {
            LocationDates.Add(locationDate);
        }
    }
    
}