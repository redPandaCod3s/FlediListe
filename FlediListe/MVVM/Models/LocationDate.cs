namespace FlediListe.MVVM.Models;

public class LocationDate
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid LocationId { get; set; }
    public DateOnly LocDate { get; set;}
    public string? Colony { get; set;}
    public DateTime TimeStamp {get; set;}
    public int? NumberBats { get; set;}
    public int? NumberTutors { get; set;}
    
}