using FlediListe.Maui.MVVM.Models;

namespace FlediListe.Maui.MVVM.Service;

public interface IFileEntryService
{
    Task<IEnumerable<FileEntry>> GetByLocationDateIdAsync(Guid locationDateId);
    Task<FileEntry?> GetByIdAsync(Guid id);
    Task SaveAsync(FileEntry fileEntry);
    Task DeleteAsync(FileEntry fileEntry);
    Task<int> GetNextFileNumberAsync(Guid locationDateId);
    Task DeleteAllAsync();
}