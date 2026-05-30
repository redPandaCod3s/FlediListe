using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlediListe.MVVM.ViewModels;

namespace FlediListe.MVVM.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel; 
        
    public MainPage()
    {
        InitializeComponent();
        BindingContext  = _viewModel = new MainViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Refresh();
    }
    
}