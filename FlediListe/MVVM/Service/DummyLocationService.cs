namespace FlediListe.MVVM.Service;
using FlediListe.MVVM.Models;

public class DummyLocationService : ILocationService
{
    private readonly List<Location> _locations = [];
    private readonly ILocationDateService _locationDateService;

    public DummyLocationService(ILocationDateService locationDateService)
    {
        _locationDateService = locationDateService;
    }
    
    public Task<IEnumerable<Location?>> GetAsync()
    {
        var locations = _locations.OrderBy(l => l.Name).ToList();
        return Task.FromResult(locations.Cast<Location?>().AsEnumerable());
    }

    public Task<Location?> GetByIdAsync(Guid id)
    {
        var location = _locations.SingleOrDefault(l => l.Id == id);
        return Task.FromResult(location);
    }

    public Task SaveAsync(Location location)
    {
        var hasEmptyId = location.Id == Guid.Empty;
        var isExistingRecord = _locations.Any(l => l.Id == location.Id);

        if (hasEmptyId || !isExistingRecord)
        {
            if (hasEmptyId)
            {
                location.Id = Guid.NewGuid();
            }
            
            _locations.Add(location);
        }
        else
        {
            var locationToUpdate = _locations.SingleOrDefault(l => l.Id == location.Id);
            if (locationToUpdate is not null)
            {
                locationToUpdate.Name = location.Name;
            }
        }
        
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Location location)
    {
        if (_locations.Any(l => l.Id == location.Id))
        {
            var locationToDelete = _locations.Single(l => l.Id == location.Id);
            
            // Zuerst alle LocationDates dieser Location löschen
            var locationDatesToDelete = await _locationDateService.GetByLocationIdAsync(location.Id);
            foreach (var locationDate in locationDatesToDelete)
            {
                await _locationDateService.DeleteAsync(locationDate);
            }
            
            // location an sich löschen
            _locations.Remove(locationToDelete);
        }
    }

    public Task DeleteAllAsync()
    {
        _locations.Clear();
        return Task.CompletedTask;
    }
}