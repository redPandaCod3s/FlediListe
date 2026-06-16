using System.Windows.Input;
using FlediListe.MVVM.Commands;
using FlediListe.MVVM.Models;
using FlediListe.MVVM.Service;

namespace FlediListe.MVVM.ViewModels;

[QueryProperty(nameof(LocationId), "locationId")]
public class DateFormViewModel : ViewModelBase
{
    private readonly ILocationDateService _locationDateService;
    
    private string _locationId = string.Empty;

    public string LocationId
    {
        get => _locationId;
        set => SetProperty(ref _locationId, value);
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
        if (string.IsNullOrWhiteSpace(_locationId)) return;

        await _locationDateService.SaveAsync(new LocationDate()
        {
            Id = Guid.NewGuid(),
            LocationId = Guid.Parse(_locationId),
            LocDate = LocDate,
            Colony = Colony,
            NumberBats =  NumberBats,
            NumberTutors = NumberTutors,
            TimeStamp = DateTime.Now
        });
        
        await Shell.Current.GoToAsync("..");
    }
    
}