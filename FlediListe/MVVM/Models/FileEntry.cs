using FlediListe.MVVM.Helper;

namespace FlediListe.MVVM.Models;

public class FileEntry : NotifyPropertyChangedBase
{
    private Guid _id = Guid.NewGuid();
    public Guid Id
    {
        get => _id; 
        set => SetProperty(ref _id, value);
    }

    private Guid _locationDateId;
    public Guid LocationDateId
    {
        get => _locationDateId; 
        set => SetProperty(ref _locationDateId, value);
    }

    private int? _fileNumber;
    public int? FileNumber
    {
        get => _fileNumber;
        set => SetProperty(ref _fileNumber, value);
    }

    private string? _individual;
    public string? Individual
    {
        get  => _individual; 
        set  => SetProperty(ref _individual, value);
    }

    private string? _fileComment;
    public string? FileComment
    {
        get => _fileComment; 
        set => SetProperty(ref _fileComment, value);
    }     // soll Vorschläge enthalten aufgrund Eingaben
    
    private bool? _clipping;
    public bool? Clipping
    {
        get => _clipping; 
        set =>  SetProperty(ref _clipping, value);
    }          // Checkbox wenn übersteuert

    private string? _video;
    public string? Video
    {
        get => _video; 
        set => SetProperty(ref _video, value);
    }           // soll auch Vorschläge auf Basis letzter Enträge

    private string? _videoComment;
    public string? VideoComment
    {
        get => _videoComment; 
        set => SetProperty(ref _videoComment, value);
    }    // bei den Kommentaren sollen die Vorschläge auch löschbar sein

    private string? _dayTime;

    public string? DayTime
    {
        get => _dayTime; 
        set  => SetProperty(ref _dayTime, value);
    }
    
}
