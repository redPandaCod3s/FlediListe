using FlediListe.MVVM.Models;

namespace FlediListe.MVVM.Service;

public class DummyFileEntryService : IFileEntryService
{
    private readonly List<FileEntry> _fileEntries = [];
    private readonly ILocationDateService _locationDateService;

    public DummyFileEntryService(ILocationDateService locationDateService)
    {
        _locationDateService = locationDateService;
    }
    
    public Task<IEnumerable<FileEntry>> GetByLocationDateIdAsync(Guid locationDateId)
    {
        var fileEntries = _fileEntries
            .Where(fe => fe.LocationDateId == locationDateId)
            .OrderBy(fe => fe.LocationDateId == locationDateId)
            .ToList();
        return Task.FromResult(fileEntries.AsEnumerable());
    }

    public Task<FileEntry?> GetByIdAsync(Guid id)
    {
        var fileEntry = _fileEntries.SingleOrDefault(fe => fe.Id == id);
        return Task.FromResult(fileEntry);
    }

    public async Task SaveAsync(FileEntry fileEntry)
    {
        var isExistingRecord = _fileEntries.Any(fe => fe.Id == fileEntry.Id);

        if (!isExistingRecord)
        {
            _fileEntries.Add(fileEntry);
            
            // prüfen: ist dies das erste FileEntry für diesen LocationDate?
            var otherEntries = _fileEntries.Count(fe => fe.LocationDateId == fileEntry.LocationDateId);
            if (otherEntries == 1)
            {
                // startTimeStamp setzen
                var locationDate = await _locationDateService.GetByIdAsync(fileEntry.LocationDateId);
                if (locationDate is not null && locationDate.StartTimeStamp == default)
                {
                    locationDate.StartTimeStamp = DateTime.Now;
                    await _locationDateService.SaveAsync(locationDate);
                }
            }
        }
        else
        {
            var fileEntryToUpdate = _fileEntries.Single(fe => fe.Id == fileEntry.Id);
            fileEntryToUpdate.FileNumber = fileEntry.FileNumber;
            fileEntryToUpdate.Individual = fileEntry.Individual;
            fileEntryToUpdate.FileComment = fileEntry.FileComment;
            fileEntryToUpdate.Clipping = fileEntry.Clipping;
            fileEntryToUpdate.Video = fileEntry.Video;
            fileEntryToUpdate.VideoComment = fileEntry.VideoComment;
            fileEntryToUpdate.DayTime = fileEntry.DayTime;
            
            // endTimeStamp setzen
            var locationDate = await _locationDateService.GetByIdAsync(fileEntry.LocationDateId);
            if (locationDate is not null && locationDate.EndTimeStamp == default)
            {
                locationDate.EndTimeStamp = DateTime.Now;
                await _locationDateService.SaveAsync(locationDate);
            }
        }
    }

    public Task DeleteAsync(FileEntry fileEntry)
    {
        var fileEntryToDelete = _fileEntries.SingleOrDefault(fe => fe.Id == fileEntry.Id);
        if (fileEntryToDelete != null)
        {
            _fileEntries.Remove(fileEntryToDelete);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAllAsync()
    {
        _fileEntries.Clear();
        return Task.CompletedTask;
    }
}