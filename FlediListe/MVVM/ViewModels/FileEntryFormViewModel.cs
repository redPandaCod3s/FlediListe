using System.Windows.Input;
using FlediListe.MVVM.Commands;
using FlediListe.MVVM.Models;
using FlediListe.MVVM.Service;

namespace FlediListe.MVVM.ViewModels;

[QueryProperty(nameof(FileEntryId), "fileEntryId")]
[QueryProperty(nameof(LocationDateId), "locationDateId")]
public class FileEntryFormViewModel : ViewModelBase
{
    private readonly IFileEntryService _fileEntryService;
    
    private string? _fileEntryId = string.Empty;
    public string? FileEntryId
    {
        get => _fileEntryId;
        set => SetProperty(ref _fileEntryId, value, async () => await InitializeAsync());
    }
    
    private string? _locationDateId = string.Empty;
    public string? LocationDateId
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
    
    private string? _individual = string.Empty;
    public string? Individual
    {
        get => _individual;
        set => SetProperty(ref _individual, value);
    }
    
    private string? _fileComment = string.Empty;
    public string? FileComment
    {
        get => _fileComment;
        set => SetProperty(ref _fileComment, value);
    }

    private bool? _clipping;
    public bool? Clipping
    {
        get => _clipping;
        set => SetProperty(ref _clipping, value);
    }

    private string? _video = string.Empty;
    public string? Video
    {
        get => _video;
        set => SetProperty(ref _video, value);
    }
    
    private string? _videoComment = string.Empty;
    public string? VideoComment
    {
        get => _videoComment;
        set => SetProperty(ref _videoComment, value);
    }
    
    private TimeOnly? _dayTime;
    public TimeOnly? DayTime
    {
        get => _dayTime;
        set => SetProperty(ref _dayTime, value);
    }

    private bool _isEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set => SetProperty(ref _isEditMode, value);
    }
    
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

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
        
        if (string.IsNullOrWhiteSpace(LocationDateId))
        {
            return;
        }

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
            DayTime = TimeOnly.FromDateTime(DateTime.Now)
        });
        
        await Shell.Current.GoToAsync("..");
    }

    private async Task InitializeAsync()
    {
        if (!string.IsNullOrWhiteSpace(FileEntryId))
        {

            var fileEntry = await _fileEntryService.GetByIdAsync(Guid.Parse(FileEntryId));
            if (fileEntry is not null)
            {
                FileNumber = fileEntry.FileNumber;
                Individual = fileEntry.Individual ?? string.Empty;
                FileComment = fileEntry.FileComment ?? string.Empty;
                Clipping = fileEntry.Clipping ?? false;
                Video = fileEntry.Video ?? string.Empty;
                VideoComment = fileEntry.VideoComment ?? string.Empty;
                DayTime = fileEntry.DayTime;
            }
        }
        else
        {

            if (!string.IsNullOrWhiteSpace(LocationDateId))
            {
                FileNumber = await _fileEntryService.GetNextFileNumberAsync(Guid.Parse(LocationDateId));
            }
        }
        
    }
}