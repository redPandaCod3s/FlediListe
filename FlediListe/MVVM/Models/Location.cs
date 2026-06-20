using System.ComponentModel;
using System.Runtime.CompilerServices;
using FlediListe.MVVM.Helper;

namespace FlediListe.MVVM.Models;

public class Location : NotifyPropertyChangedBase
{
    private Guid _id =  Guid.NewGuid();
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