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
        
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext  = _viewModel = viewModel;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await _viewModel.InitializeAsync();
        base.OnNavigatedTo(args);
    }
}