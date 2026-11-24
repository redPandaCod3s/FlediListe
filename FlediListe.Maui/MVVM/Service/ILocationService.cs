using Location = FlediListe.Maui.MVVM.Models.Location;
using Models_Location = FlediListe.Maui.MVVM.Models.Location;

namespace FlediListe.Maui.MVVM.Service;

public interface ILocationService
{
    Task<IEnumerable<Models_Location?>> GetAsync();
    Task<Models_Location?> GetByIdAsync(Guid id);
    Task SaveAsync(Models_Location location);
    Task DeleteAsync(Models_Location location);
    Task DeleteAllAsync();

}