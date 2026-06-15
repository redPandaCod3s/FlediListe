using FlediListe.MVVM.Models;

namespace FlediListe.MVVM.Service;

public interface IFileEntryService
{
    Task<IEnumerable<FileEntry>> GetByLocationDateIdAsync(Guid locationDateId);
    Task<FileEntry?> GetByIdAsync(Guid id);
    Task SaveAsync(FileEntry fileEntry);
    Task DeleteAsync(FileEntry fileEntry);
    Task DeleteAllAsync();
}