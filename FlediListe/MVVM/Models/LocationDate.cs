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
    
    private DateTime? _startTimeStamp;
    public DateTime? StartTimeStamp
    {
        get => _startTimeStamp; 
        set => SetProperty(ref _startTimeStamp, value);
    }
    
    private DateTime? _endTimeStamp;
    public DateTime? EndTimeStamp
    {
        get => _endTimeStamp;
        set => SetProperty(ref _endTimeStamp, value);
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