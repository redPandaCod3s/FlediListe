using Location = FlediListe.Maui.MVVM.Models.Location;
using Models_Location = FlediListe.Maui.MVVM.Models.Location;

namespace FlediListe.Maui.MVVM.Service;

public class SqliteLocationService : ILocationService
{
    private readonly DatabaseService _databaseService;
    private readonly ILocationDateService _locationDateService;

    public SqliteLocationService(DatabaseService databaseService,  ILocationDateService locationDateService)
    {
        _databaseService = databaseService;
        _locationDateService  = locationDateService;
    }
    
    public async Task<IEnumerable<Models_Location?>> GetAsync()
    {
        var db = await _databaseService.GetConnectionAsync();
        var locattions = await db.Table<Models_Location>().OrderBy(x => x.Name).ToListAsync();
        return locattions;
    }

    public async Task<Models_Location?> GetByIdAsync(Guid id)
    {
        var db = await _databaseService.GetConnectionAsync();
        return await db.Table<Models_Location>().Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task SaveAsync(Models_Location location)
    {
        var db = await _databaseService.GetConnectionAsync();
        var existing =  await db.Table<Models_Location>().Where(l => l.Id == location.Id).FirstOrDefaultAsync();

        if (existing is null)
        {
            await db.InsertAsync(location);
        }
        else
        {
            await db.UpdateAsync(location);
        }
    }

    public async Task DeleteAsync(Models_Location location)
    {
        var db = await _databaseService.GetConnectionAsync();
        var locationDates = await _locationDateService.GetByLocationIdAsync(location.Id);
        foreach (var locationDate in locationDates)
        {
            await _locationDateService.DeleteAsync(locationDate);
        }
        
        // dann Location löschen
        await db.DeleteAsync(location);
    }

    public async Task DeleteAllAsync()
    {
        var db = await _databaseService.GetConnectionAsync();
        await db.DeleteAllAsync<Models_Location>();
    }
}