using System.Windows.Input;
using FlediListe.MVVM.Commands;
using FlediListe.MVVM.Models;
using FlediListe.MVVM.Service;

namespace FlediListe.MVVM.ViewModels;

[QueryProperty(nameof(LocationId), "locationId")]
[QueryProperty(nameof(LocationDateId), "locationDateId")]
public class DateFormViewModel : ViewModelBase
{
    private readonly ILocationDateService _locationDateService;
    
    private string _locationId = string.Empty;
    public string LocationId
    {
        get => _locationId;
        set => SetProperty(ref _locationId, value);
    }
    
    private string _locationDateId = string.Empty;

    public string LocationDateId
    {
        get => _locationDateId;
        set => SetProperty(ref _locationDateId, value, async () => await InitializeAsync());
    }

    private DateOnly _locDate = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly LocDate
    {
        get => _locDate;
        set => SetProperty(ref _locDate, value);
    }
    
    private string _colony = string.Empty;
    public string Colony
    {
        get => _colony;
        set => SetProperty(ref _colony, value);
    }

    private int? _numberBats;
    public int? NumberBats
    {
        get => _numberBats;
        set => SetProperty(ref _numberBats, value);
    }

    private int? _numberTutors;
    public int? NumberTutors
    {
        get => _numberTutors;
        set => SetProperty(ref _numberTutors, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    
    public DateFormViewModel(ILocationDateService locationDateService)
    {
        _locationDateService = locationDateService;

        SaveCommand = new AsyncRelayCommand(SaveAsync);
        CancelCommand = new AsyncRelayCommand(CancelAsync);
    }

    private Task CancelAsync()
    {
        return Shell.Current.GoToAsync("..");
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(LocationId)) return;

        LocationDate locationDate;

        if (string.IsNullOrWhiteSpace(LocationDateId))
        {
            locationDate = new LocationDate()
            {
                Id = Guid.NewGuid(),
                LocationId = Guid.Parse(LocationId),
                LocDate = LocDate,
                Colony = Colony,
                NumberBats = NumberBats,
                NumberTutors = NumberTutors,
                TimeStamp = DateTime.Now
            };
        }
        else
        {
            locationDate = new LocationDate()
            {
                Id = Guid.Parse(LocationDateId),
                LocationId = Guid.Parse(LocationId),
                LocDate = LocDate,
                Colony = Colony,
                NumberBats = NumberBats,
                NumberTutors = NumberTutors
            };
        }
        
        await _locationDateService.SaveAsync(locationDate);
        await Shell.Current.GoToAsync("..");
    }
    
    private async Task InitializeAsync()
    {
        if (!string.IsNullOrWhiteSpace(LocationDateId))
        {
            var locationDate = await _locationDateService.GetByIdAsync(Guid.Parse(LocationDateId));
            if (locationDate is not null)
            {
                LocDate = locationDate.LocDate;
                Colony = locationDate.Colony ?? string.Empty;
                NumberBats = locationDate.NumberBats;
                NumberTutors = locationDate.NumberTutors;
            }
        }
    }
    
    
    
}