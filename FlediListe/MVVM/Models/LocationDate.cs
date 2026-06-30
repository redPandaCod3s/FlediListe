using FlediListe.MVVM.Helper;
using SQLite;

namespace FlediListe.MVVM.Models;

[Table("LocationDates")]
public class LocationDate : NotifyPropertyChangedBase
{
    private Guid _id = Guid.NewGuid();
    [PrimaryKey]
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
    
    //SQLite speichert als string
    private string _locDateString = string.Empty;
    public string LocDateString
    {
        get => _locDateString;
        set
        {
            _locDateString = value;
            OnPropertyChanged(nameof(LocDate));
        }
    }
    
    
    private DateOnly _locDate;
    [Ignore]
    public DateOnly LocDate
    {
        get => DateOnly.TryParse(_locDateString, out var date) ? date : DateOnly.MinValue;
        set => LocDateString = value.ToString("yyyy-MM-dd");
    }

    private string? _colony;
    public string? Colony
    {
        get => _colony; 
        set => SetProperty(ref _colony, value);
    }
    
    private string? _startTimeStampString;

    public string? StartTimeStampString
    {
        get => _startTimeStampString;
        set
        {
            _startTimeStampString = value;
            OnPropertyChanged(nameof(StartTimeStamp));
        }
    }
    
    private TimeOnly? _startTimeStamp;
    [Ignore]
    public TimeOnly? StartTimeStamp
    {
        get => _startTimeStamp; 
        set => SetProperty(ref _startTimeStamp, value);
    }
    
    private string? _endTimeStampString;

    public string? EndTimeStampString
    {
        get => _endTimeStampString;
        set
        {
            _endTimeStampString = value;
            OnPropertyChanged(nameof(EndTimeStamp));
        }
    }
    
    private TimeOnly? _endTimeStamp;
    [Ignore]
    public TimeOnly? EndTimeStamp
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