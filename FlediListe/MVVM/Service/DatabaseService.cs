using FlediListe.MVVM.Models;
using SQLite;
using Location = FlediListe.MVVM.Models.Location;

namespace FlediListe.MVVM.Service;

public class DatabaseService
{
    private SQLiteAsyncConnection? _connection;
    private readonly string _dbPath;

    public DatabaseService()
    {
        _dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "FlediListe.db");
    }

    private async Task IntializeAsync()
    {
        if (_connection is not null) return;
        
        _connection = new SQLiteAsyncConnection(_dbPath);
        
        // Tabellen erstellen
        await _connection.CreateTableAsync<Location>();
        await _connection.CreateTableAsync<LocationDate>();
        await _connection.CreateTableAsync<FileEntry>();
    }

    public async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        await  IntializeAsync();
        return _connection!;
    }
    
}