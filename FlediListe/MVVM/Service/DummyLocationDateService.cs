using FlediListe.MVVM.Models;

namespace FlediListe.MVVM.Service;

public class DummyLocationDateService : ILocationDateService
{
    private readonly List<LocationDate> _locationDates = [];
    private readonly IFileEntryService _fileEntryService;

    public DummyLocationDateService(IFileEntryService fileEntryService)
    {
        _fileEntryService = fileEntryService;
    }
    
    public Task<IEnumerable<LocationDate>> GetByLocationIdAsync(Guid locationId)
    {
        var locationDates = _locationDates
            .Where(ld => ld.LocationId == locationId)
            .OrderBy(ld => ld.LocDate)
            .ToList();
        return Task.FromResult(locationDates.AsEnumerable());
    }

    public Task<LocationDate?> GetByIdAsync(Guid id)
    {
        var locationDate = _locationDates.SingleOrDefault(ld => ld.Id == id);
        return Task.FromResult(locationDate);
    }

    public Task SaveAsync(LocationDate locationDate)
    {
        var isExistingRecord = _locationDates.Any(ld => ld.Id == locationDate.Id);

        if (!isExistingRecord)
        {
            _locationDates.Add(locationDate);
        }
        else
        {
            var locationDateToUpdate = _locationDates.Single(ld => ld.Id == locationDate.Id);
            locationDateToUpdate.LocDate = locationDate.LocDate;
            locationDateToUpdate.Colony = locationDate.Colony;
            locationDateToUpdate.StartTimeStamp = locationDate.StartTimeStamp;
            locationDateToUpdate.EndTimeStamp = locationDate.EndTimeStamp;
            locationDateToUpdate.NumberBats= locationDate.NumberBats;
            locationDateToUpdate.NumberTutors = locationDate.NumberTutors;
        }
        
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(LocationDate locationDate)
    {
        var locationDateToDelete = _locationDates.SingleOrDefault(ld => ld.Id == locationDate.Id);
        if (locationDateToDelete is not null)
        {
            // zuerst alle FileEntries dieses LocationDate löschen
            var fileEntriesToDelete = await _fileEntryService.GetByLocationDateIdAsync(locationDate.Id);
            foreach (var fileEntry in fileEntriesToDelete)
            {
                await _fileEntryService.DeleteAsync(fileEntry);
            }
            
            // dann das LocationDate selbst löschen
            _locationDates.Remove(locationDateToDelete);
        }
    }

    public Task DeleteAllAsync()
    {
        _locationDates.Clear();
        return Task.CompletedTask;
    }
    
}