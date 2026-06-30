using System.ComponentModel;
using System.Runtime.CompilerServices;
using FlediListe.MVVM.Helper;
using SQLite;

namespace FlediListe.MVVM.Models;

[Table("Locations")]
public class Location : NotifyPropertyChangedBase
{
    
    private Guid _id =  Guid.NewGuid();
    [PrimaryKey]
    public Guid Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    private string? _name;
    public string? Name
    {
        get => _name; 
        set => SetProperty(ref _name, value);
    } 
    
}