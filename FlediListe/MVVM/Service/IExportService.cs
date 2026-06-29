using FlediListe.MVVM.Models;
using Location = FlediListe.MVVM.Models.Location;

namespace FlediListe.MVVM.Service;

public interface IExportService
{
    Task<string> ExportFileEntriesToCsvAsync(Location location, LocationDate locationDate, IEnumerable<FileEntry> fileEntries);
}