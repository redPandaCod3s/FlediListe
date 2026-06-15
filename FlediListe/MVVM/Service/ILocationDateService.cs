using FlediListe.MVVM.Models;

namespace FlediListe.MVVM.Service;

public interface ILocationDateService
{
    Task<IEnumerable<LocationDate>> GetByLocationIdAsync(Guid locationId);
    Task<LocationDate?> GetByIdAsync(Guid id);
    Task SaveAsync(LocationDate locationDate);
    Task DeleteAsync(LocationDate locationDate);
    Task DeleteAllAsync();

}