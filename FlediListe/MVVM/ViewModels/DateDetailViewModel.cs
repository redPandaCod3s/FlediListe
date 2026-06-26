using System.Collections.ObjectModel;
using System.Windows.Input;
using FlediListe.MVVM.Commands;
using FlediListe.MVVM.Models;
using FlediListe.MVVM.Service;
using FlediListe.MVVM.Views;

namespace FlediListe.MVVM.ViewModels;

[QueryProperty(nameof(LocationDateId), "locationDateId")]
public class DateDetailViewModel : ViewModelBase
{
    private readonly IFileEntryService _fileEntryService;

    private string _locationDateId;
    public string LocationDateId
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

    public DateDetailViewModel(IFileEntryService fileEntryService)
    {
        
        _fileEntryService = fileEntryService;

        ReturnToDatePage = new AsyncRelayCommand(NavigateToDatePage);
        SetEditingMode = new RelayCommand(() => IsEditMode = ! IsEditMode);
        SaveNewFileEntry = new AsyncRelayCommand(NavigateToFileEntryForm);
        DeleteFileEntry = new AsyncRelayCommand<FileEntry>(DeleteFileEntryAsync);
        UpdateFileEntry = new AsyncRelayCommand(UpdateFileEntryAsync);
        TapItemCommand = new RelayCommand<FileEntry>(HandleSelection);

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

    private async Task DeleteFileEntryAsync(FileEntry fileEntry)
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
        
        var fileEntries = await _fileEntryService.GetByLocationDateIdAsync(Guid.Parse(LocationDateId));
        FileEntries.Clear();
        foreach (var fileEntry in fileEntries)
        {
            FileEntries.Add(fileEntry);
        }
        
        SelectedFileEntry = FileEntries.FirstOrDefault();
    }
    
}