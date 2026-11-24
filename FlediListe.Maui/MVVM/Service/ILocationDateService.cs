using FlediListe.Maui.MVVM.Models;

namespace FlediListe.Maui.MVVM.Service;

public interface ILocationDateService
{
    Task<IEnumerable<LocationDate>> GetByLocationIdAsync(Guid locationId);
    Task<LocationDate?> GetByIdAsync(Guid id);
    Task SaveAsync(LocationDate locationDate);
    Task DeleteAsync(LocationDate locationDate);
    Task DeleteAllAsync();

}