namespace FlediListe.MVVM.Models;

public class LocationDate
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly LocDate { get; set;}
    public Guid LocationId { get; set; }
    public int NumberBats { get; set;}
    public int Tutors { get; set;}
    public string Colony { get; set;}
}