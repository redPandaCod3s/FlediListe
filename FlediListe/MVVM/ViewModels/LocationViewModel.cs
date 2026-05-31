using System.Windows.Input;
using FlediListe.MVVM.Commands;

namespace FlediListe.MVVM.ViewModels;

public class LocationViewModel : ViewModelBase
{


    public ICommand ReturnToMainPage { get; }
    
    public LocationViewModel()
    {
        ReturnToMainPage = new AsyncRelayCommand(NavigateToMainPage);
    }

    private Task NavigateToMainPage()
    {
        return Shell.Current.GoToAsync("//MainPage");
    }

    public void Refresh()
    {
        
    }
    
}