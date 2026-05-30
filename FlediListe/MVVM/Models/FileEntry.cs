namespace FlediListe.MVVM.Models;

public class FileEntry
{
    public Guid Id { get; set;} = Guid.NewGuid();
    public string Individual { get; set; }
    public int FileNumber { get; set; }
    public string Comment { get; set; }
    public string DayTime { get; set; }
    public string Override { get; set; }
}
