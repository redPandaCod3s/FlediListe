using System.Windows.Input;
using FlediListe.MVVM.Commands;
using FlediListe.MVVM.Models;
using FlediListe.MVVM.Service;

namespace FlediListe.MVVM.ViewModels;

[QueryProperty(nameof(LocationDateId), "LocationDateId")]
public class FileEntryFormViewModel : ViewModelBase
{
    private readonly IFileEntryService _fileEntryService;
    
    private string _locationDateId = string.Empty;

    public string LocationDateId
    {
        get => _locationDateId;
        set => SetProperty(ref _locationDateId, value);
    }
    
    private int _fileNumber;
    public int FileNumber
    {
        get => _fileNumber;
        set => SetProperty(ref _fileNumber, value);
    }
    
    private string _individual = string.Empty;
    public string Individual
    {
        get => _individual;
        set => SetProperty(ref _individual, value);
    }
    
    private string _fileComment = string.Empty;
    public string FileComment
    {
        get => _fileComment;
        set => SetProperty(ref _fileComment, value);
    }

    private bool _clipping;
    public bool Clipping
    {
        get => _clipping;
        set => SetProperty(ref _clipping, value);
    }

    private string _video = string.Empty;
    public string Video
    {
        get => _video;
        set => SetProperty(ref _video, value);
    }
    
    private string videoComment = string.Empty;
    public string VideoComment
    {
        get => videoComment;
        set => SetProperty(ref videoComment, value);
    }
    
    private string _dayTime = string.Empty;
    public string DayTime
    {
        get => _dayTime;
        set => SetProperty(ref _dayTime, value);
    }
    
    public ICommand SaveCommand { get;}
    public ICommand CancelCommand { get;}

    public FileEntryFormViewModel(IFileEntryService fileEntryService)
    {
        _fileEntryService = fileEntryService;

        SaveCommand = new AsyncRelayCommand(SaveAsync);
        CancelCommand = new AsyncRelayCommand(CancelAsync);

    }

    private Task CancelAsync()
    {
        return Shell.Current.GoToAsync("..");
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(LocationDateId)) return;

        await _fileEntryService.SaveAsync(new FileEntry()
        {
            Id = Guid.NewGuid(),
            LocationDateId = Guid.Parse(LocationDateId),
            FileNumber = FileNumber,
            Individual = Individual,
            FileComment = FileComment,
            Clipping = Clipping,
            Video = Video,
            VideoComment = VideoComment,
            DayTime = DayTime
        });
        
        await Shell.Current.GoToAsync("..");
    }

}