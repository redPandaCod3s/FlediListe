namespace FlediListe.MVVM.Service;
using FlediListe.MVVM.Models;

public class DummyLocationService : ILocationService
{
    private readonly List<Location> _locations = [];

    public Task<IEnumerable<Location>> GetAsync()
    {
        var locations = _locations.OrderBy(l => l.Name).ToList();
        return Task.FromResult(locations.AsEnumerable());
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
            locationToUpdate.Name = location.Name;
        }
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Location location)
    {
        var locationToDelete = _locations.SingleOrDefault(l => l.Id == location.Id);
        if (locationToDelete is not null)
        {
            _locations.Remove(locationToDelete);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAllAsync()
    {
        _locations.Clear();
        return Task.CompletedTask;
    }
}