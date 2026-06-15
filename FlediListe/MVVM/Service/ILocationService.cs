using Location = FlediListe.MVVM.Models.Location;

namespace FlediListe.MVVM.Service;

public interface ILocationService
{
    Task<IEnumerable<Location?>> GetAsync();
    Task<Location?> GetByIdAsync(Guid id);
    Task SaveAsync(Location location);
    Task DeleteAsync(Location location);
    Task DeleteAllAsync();

}