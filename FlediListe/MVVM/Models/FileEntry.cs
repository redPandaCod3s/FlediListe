namespace FlediListe.MVVM.Models;

public class FileEntry
{
    public Guid Id { get; set;} = Guid.NewGuid();
    public int FileNumber { get; set; }
    public string Individual { get; set; }
    public string FileComment { get; set; }     // soll Vorschläge enthalten aufgrund Eingaben
    public bool Clipping { get; set; }          // Checkbox wenn übersteuert
    public string Video { get; set; }           // soll auch Vorschläge auf Basis letzter Enträge
    public string VideoComment { get; set; }    // bei den Kommentaren sollen die Vorschläge auch löschbar sein
    public string DayTime { get; set; }
    
}
