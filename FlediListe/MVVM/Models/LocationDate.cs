using FlediListe.MVVM.Helper;

namespace FlediListe.MVVM.Models;

public class LocationDate : NotifyPropertyChangedBase
{
    private Guid _id = Guid.NewGuid();
    public Guid Id
    {
        get => _id; 
        set => SetProperty(ref _id, value);
    }

    private Guid _locationId;
    public Guid LocationId
    {
        get => _locationId; 
        set  => SetProperty(ref _locationId, value);
    }
    
    private DateOnly _locDate;
    public DateOnly LocDate
    {
        get => _locDate; 
        set => SetProperty(ref _locDate, value);
    }

    private string? _colony;
    public string? Colony
    {
        get => _colony; 
        set => SetProperty(ref _colony, value);
    }
    
    private DateTime? _timeStamp;
    public DateTime? TimeStamp
    {
        get => _timeStamp; 
        set => SetProperty(ref _timeStamp, value);
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
    
}