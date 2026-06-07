namespace FlediListe.MVVM.Models;

public class Location
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string? Name {get; set;}
}