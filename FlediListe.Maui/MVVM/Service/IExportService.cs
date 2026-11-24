using FlediListe.Maui.MVVM.Models;
using Location = FlediListe.Maui.MVVM.Models.Location;

namespace FlediListe.Maui.MVVM.Service;

public interface IExportService
{
    Task<string> ExportFileEntriesToCsvAsync(Location location, LocationDate locationDate, IEnumerable<FileEntry> fileEntries);
}