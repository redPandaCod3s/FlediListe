using System.Text;
using FlediListe.MVVM.Service;

namespace FlediListe.MVVM.Models;

public class CsvExportService : IExportService
{
    public async Task<string> ExportFileEntriesToCsvAsync(Location location, LocationDate locationDate,
        IEnumerable<FileEntry> fileEntries)
    {

        string praefix = $"{location.Name};" +
                         $"{locationDate.Colony};{locationDate.LocDate};{locationDate.NumberTutors};{locationDate.NumberBats};"+
                         $"{locationDate.StartTimeStamp};{locationDate.EndTimeStamp}";
        
        var sb = new StringBuilder();
        
        // Header mit Standort- und Termin-Infos
        sb.AppendLine("Standort;Colony;Datum;Anzahl Tutors;Anzahl Bats;Startzeit;Endzeit;File-Nr;Individuum;Kommentar;Uebersteuert;Video;Video-Kommentar;Uhrzeit");
        
        // Daten
        foreach (var entry in fileEntries)
        {
            sb.AppendLine(praefix + ";" + $"{entry.FileNumber};" +
                          $"\"{entry.Individual}\";" +
                          $"\"{entry.FileComment}\";" +
                          $"{(entry.Clipping ?? false ? "Ja" : "Nein")};" +
                          $"\"{entry.Video}\";" +
                          $"\"{entry.VideoComment}\";" +
                          $"\"{entry.DayTime}\"");
        }
        
        // Datei speichern
        var fileName = $"{location.Name}_{locationDate.LocDate:yyyy-MM-dd}_Export.csv";
        

        string filePath;
        #if ANDROID 
            var downloadsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)
            ?.AbsolutePath ?? FileSystem.AppDataDirectory;
            filePath = Path.Combine(downloadsPath, fileName);
        #else
           filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
        #endif
        
        await File.WriteAllTextAsync(filePath, sb.ToString());
        
        return filePath;
    }
    
}