using FlediListe.MVVM.Models;

namespace FlediListe.MVVM.Service;

public class DummyLocationDateService : ILocationDateService
{
    private readonly List<LocationDate> _locationDates = [];
    
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
            locationDateToUpdate.TimeStamp = locationDate.TimeStamp;
            locationDateToUpdate.NumberBats= locationDate.NumberBats;
            locationDateToUpdate.NumberTutors = locationDate.NumberTutors;
        }
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(LocationDate locationDate)
    {
        var locationToDelete = _locationDates.SingleOrDefault(ld => ld.Id == locationDate.Id);
        if (locationToDelete is not null)
        {
            _locationDates.Remove(locationToDelete);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAllAsync()
    {
        _locationDates.Clear();
        return Task.CompletedTask;
    }
}