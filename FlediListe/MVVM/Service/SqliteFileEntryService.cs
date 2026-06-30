using FlediListe.MVVM.Models;

namespace FlediListe.MVVM.Service;

public class SqliteFileEntryService : IFileEntryService
{
    private readonly DatabaseService _databaseService;

    public SqliteFileEntryService(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    
    public async Task<IEnumerable<FileEntry>> GetByLocationDateIdAsync(Guid locationDateId)
    {
        var db = await _databaseService.GetConnectionAsync();
        return await db.Table<FileEntry>()
            .Where(fe => fe.LocationDateId == locationDateId)
            .OrderBy(fe => fe.FileNumber)
            .ToListAsync();
    }

    public async Task<FileEntry?> GetByIdAsync(Guid id)
    {
        var db = await _databaseService.GetConnectionAsync();
        return await db.Table<FileEntry>()
            .Where(fe => fe.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task SaveAsync(FileEntry fileEntry)
    {
        var db = await _databaseService.GetConnectionAsync();
        var existing = await db.Table<FileEntry>()
            .Where(fe => fe.Id == fileEntry.Id)
            .FirstOrDefaultAsync();

        if (existing is null)
        {
            // nur automatisch setzen wenn keine FileNumber eingegeben wurde
            if (fileEntry.FileNumber is null or 0)
            {
                // neue FileEntry - FileNumber automatisch erhöhen
                var maxFileNumber = await db.Table<FileEntry>()
                    .Where(fe => fe.LocationDateId == fileEntry.LocationDateId)
                    .OrderByDescending(fe => fe.FileNumber)
                    .FirstOrDefaultAsync();
            
                fileEntry.FileNumber = (maxFileNumber?.FileNumber ?? 0) + 1;
            }
            
            await db.InsertAsync(fileEntry);
        }
        else
        {
            await db.UpdateAsync(fileEntry);
        }
    }

    public async Task DeleteAsync(FileEntry fileEntry)
    {
        var db = await _databaseService.GetConnectionAsync();
        await db.DeleteAsync(fileEntry);
    }

    public async Task<int> GetNextFileNumberAsync(Guid locationDateId)
    {
        var db = await _databaseService.GetConnectionAsync();
        
        var maxFileNumber = await db.Table<FileEntry>()
            .Where(fe => fe.LocationDateId == locationDateId)
            .OrderByDescending(fe => fe.FileNumber)
            .FirstOrDefaultAsync();
        
        return (maxFileNumber?.FileNumber ?? 0) + 1;
    }

    public async Task DeleteAllAsync()
    {
        var db = await _databaseService.GetConnectionAsync();
        await db.DeleteAllAsync<FileEntry>();
    }
}