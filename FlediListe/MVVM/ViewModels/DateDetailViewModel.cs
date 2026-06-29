using System.Collections.ObjectModel;
using System.Windows.Input;
using FlediListe.MVVM.Commands;
using FlediListe.MVVM.Models;
using FlediListe.MVVM.Service;
using FlediListe.MVVM.Views;
using Location = FlediListe.MVVM.Models.Location;

namespace FlediListe.MVVM.ViewModels;

[QueryProperty(nameof(LocationDateId), "locationDateId")]
public class DateDetailViewModel : ViewModelBase
{
    private readonly IFileEntryService _fileEntryService;
    private readonly  ILocationDateService _locationDateService;
    private readonly  IExportService _exportService;
    private readonly ILocationService _locationService;
    
    private TimeOnly? _startTimeStamp;
    public TimeOnly? StartTimeStamp
    {
        get => _startTimeStamp;
        set => SetProperty(ref _startTimeStamp, value);
    }
    
    private  TimeOnly? _endTimeStamp;
    public TimeOnly? EndTimeStamp
    {
        get => _endTimeStamp;
        set => SetProperty(ref _endTimeStamp, value);
    }

    private string? _locationDateId = string.Empty;
    public string? LocationDateId
    {
        get => _locationDateId;
        set => SetProperty(ref _locationDateId, value, async () => await InitializeAsync());
    }

    private FileEntry? _selectedFileEntry;
    public FileEntry? SelectedFileEntry
    {
        get => _selectedFileEntry;
        set => SetProperty(ref _selectedFileEntry, value);
    }
    
    private Location? _selectedLocation;
    public Location? SelectedLocation
    {
        get => _selectedLocation;
        set => SetProperty(ref _selectedLocation, value);
    }
    
    private LocationDate? _selectedLocationDate;
    public LocationDate? SelectedLocationDate
    {
        get => _selectedLocationDate;
        set => SetProperty(ref _selectedLocationDate, value);
    }

    private bool _isEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set => SetProperty(ref _isEditMode, value);
    }
    
    // Felder für neuen FileEntry
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
    
    private string _videoComment = string.Empty;
    public string VideoComment
    {
        get => _videoComment;
        set => SetProperty(ref _videoComment, value);
    }
    
    private string _dayTime = string.Empty;
    public string DayTime
    {
        get => _dayTime;
        set => SetProperty(ref _dayTime, value);
    }

    public ObservableCollection<FileEntry> FileEntries { get; } = new();
    
    public ICommand ReturnToDatePage { get; }
    public ICommand SetEditingMode { get; }
    public ICommand SaveNewFileEntry { get; }
    public ICommand DeleteFileEntry { get; }
    public ICommand UpdateFileEntry { get; }
    public ICommand TapItemCommand { get; }
    public ICommand SetStartTimeStamp { get; }
    public ICommand SetEndTimeStamp { get; }
    public ICommand ExportCsvCommand { get; }
    

    public DateDetailViewModel(
        IFileEntryService fileEntryService, 
        ILocationDateService locationDateService, 
        IExportService exportService,
        ILocationService locationService)
    {
        
        _fileEntryService = fileEntryService;
        _locationDateService = locationDateService;
        _exportService = exportService;
        _locationService = locationService;

        ReturnToDatePage = new AsyncRelayCommand(NavigateToDatePage);
        SetEditingMode = new RelayCommand(() => IsEditMode = ! IsEditMode);
        SaveNewFileEntry = new AsyncRelayCommand(NavigateToFileEntryForm);
        DeleteFileEntry = new AsyncRelayCommand<FileEntry>(DeleteFileEntryAsync);
        UpdateFileEntry = new AsyncRelayCommand(UpdateFileEntryAsync);
        TapItemCommand = new RelayCommand<FileEntry>(HandleSelection);
        SetStartTimeStamp = new AsyncRelayCommand(SetStartTimeStampAsync);
        SetEndTimeStamp = new AsyncRelayCommand(SetEndTimeStampAsync);
        ExportCsvCommand = new AsyncRelayCommand(ExportCsvAsync);
    }

    private async Task ExportCsvAsync()
    {
        if (!FileEntries.Any())
        {
            await Shell.Current.DisplayAlertAsync("Fehler", "Keine Einträge zum Exportieren", "OK");
            return;
        }

        if (SelectedLocation is null || SelectedLocationDate is null)
        {
            await Shell.Current.DisplayAlertAsync("Fehler", "Standort oder Termin nicht geladen", "OK");
        }

        try
        {
            var filePath = await _exportService.ExportFileEntriesToCsvAsync(SelectedLocation, SelectedLocationDate, FileEntries);
            await Shell.Current.DisplayAlertAsync("Erfolg",$"CSV exportiert: {Path.GetFileName(filePath)}", "OK");
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlertAsync("Fehler", $"Export fehlgeschlagen: {e.Message}", "OK");
        }
        
    }

    private async Task SetEndTimeStampAsync()
    {
        if(string.IsNullOrWhiteSpace(LocationDateId)) return;
        
        var locationDate = await _locationDateService.GetByIdAsync(Guid.Parse(LocationDateId));
        if (locationDate is not null)
        {
            var timeStamp = TimeOnly.FromDateTime(DateTime.Now);
            locationDate.EndTimeStamp = timeStamp;
            await _locationDateService.SaveAsync(locationDate);
            EndTimeStamp = timeStamp;
        }
    }

    private async Task SetStartTimeStampAsync()
    {
        if (string.IsNullOrWhiteSpace(LocationDateId)) return;
        
        var locationDate = await _locationDateService.GetByIdAsync(Guid.Parse(LocationDateId));
        if (locationDate is not null)
        {
            var timeStamp = TimeOnly.FromDateTime(DateTime.Now);
            locationDate.StartTimeStamp = timeStamp;
            await _locationDateService.SaveAsync(locationDate);
            StartTimeStamp = timeStamp;
        }
    }

    private Task NavigateToFileEntryForm()
    {
        return Shell.Current.GoToAsync($"{nameof(FileEntryFormPage)}?locationDateId={LocationDateId}");
    }

    private void HandleSelection(FileEntry? fileEntry)
    {
        
        if(fileEntry is null) return;
        
        SelectedFileEntry = fileEntry;
        
        NavigateToFileEntryFormEdit(fileEntry);
        
    }

    private Task NavigateToFileEntryFormEdit(FileEntry fileEntry)
    {
        return Shell.Current.GoToAsync(
            $"{nameof(FileEntryFormPage)}?locationDateId={LocationDateId}&fileEntryId={fileEntry.Id}");
    }

    private async Task DeleteFileEntryAsync(FileEntry? fileEntry)
    {
        if (fileEntry is null) return;
        await _fileEntryService.DeleteAsync(fileEntry);
        await InitializeAsync();
    }

    private async Task UpdateFileEntryAsync()
    {
        if (SelectedFileEntry is null) return;
        await _fileEntryService.SaveAsync(SelectedFileEntry);
        await InitializeAsync();
    }

    private Task NavigateToDatePage()
    {
        return Shell.Current.GoToAsync("..");
    }

    private void ResetFields()
    {
        FileNumber = 0;
        Individual = string.Empty;
        FileComment = string.Empty;
        Clipping = false;
        Video = string.Empty;
        VideoComment = string.Empty;
        DayTime = string.Empty;
    }

    public async Task InitializeAsync()
    {
        if (string.IsNullOrWhiteSpace(LocationDateId)) return;
        
        var locationDateId = Guid.Parse(LocationDateId);
        
        // LocationDate laden
        var locationDate =  await _locationDateService.GetByIdAsync(locationDateId);
        if (locationDate is not null)
        {
            SelectedLocationDate = locationDate;
            StartTimeStamp = locationDate.StartTimeStamp;
            EndTimeStamp = locationDate.EndTimeStamp;
            
            // Location laden 
            SelectedLocationDate = locationDate;
            StartTimeStamp = locationDate.StartTimeStamp;
            EndTimeStamp = locationDate.EndTimeStamp;
            
            // Location laden
            SelectedLocation = await _locationService.GetByIdAsync(locationDate.LocationId);
        }
        
        // FileEntries laden
        var fileEntries = await _fileEntryService.GetByLocationDateIdAsync(locationDateId);
        FileEntries.Clear();
        foreach (var fileEntry in fileEntries)
        {
            FileEntries.Add(fileEntry);
        }

        SelectedFileEntry = fileEntries.FirstOrDefault();
    }
    
}