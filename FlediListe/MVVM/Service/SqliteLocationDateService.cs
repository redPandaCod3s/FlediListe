using FlediListe.MVVM.Models;

namespace FlediListe.MVVM.Service;

public class SqliteLocationDateService : ILocationDateService
{
    private readonly DatabaseService _databaseService;
    private readonly IFileEntryService _fileEntryService;

    public SqliteLocationDateService(DatabaseService databaseService, IFileEntryService fileEntryService)
    {
        _databaseService = databaseService; 
        _fileEntryService = fileEntryService;
    }
    
    public async Task<IEnumerable<LocationDate>> GetByLocationIdAsync(Guid locationId)
    {
        var db = await _databaseService.GetConnectionAsync();
        return await db.Table<LocationDate>()
            .Where(ld => ld.LocationId == locationId)
            .OrderBy(ld => ld.LocDateString)
            .ToListAsync();
    }

    public async Task<LocationDate?> GetByIdAsync(Guid id)
    {
        var db = await _databaseService.GetConnectionAsync();
        return await db.Table<LocationDate>()
            .Where(ld => ld.Id == id).FirstOrDefaultAsync();
    }

    public async Task SaveAsync(LocationDate locationDate)
    {
        var db = await _databaseService.GetConnectionAsync();
        var existing = await db.Table<LocationDate>().Where(ld => ld.Id == locationDate.Id).FirstOrDefaultAsync();

        if (existing is null)
        {
            await db.InsertAsync(locationDate);
        }
        else
        {
            await db.UpdateAsync(locationDate);
        }
    }

    public async Task DeleteAsync(LocationDate locationDate)
    {
        var db = await _databaseService.GetConnectionAsync();
        
        // Zuerst alle FileEntries löschen
        var fileEntries = await _fileEntryService.GetByLocationDateIdAsync(locationDate.Id);
        foreach (var fileEntry in fileEntries)
        {
            await _fileEntryService.DeleteAsync(fileEntry);
        }
        
        // dann LocationDate löschen
        await db.DeleteAsync(locationDate);
    }

    public async Task DeleteAllAsync()
    {
        var db = await _databaseService.GetConnectionAsync();
        await db.DeleteAllAsync<LocationDate>();
    }
}