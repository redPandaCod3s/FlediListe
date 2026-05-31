using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlediListe.MVVM.ViewModels;

namespace FlediListe.MVVM.Views;

public partial class LocationPage : ContentPage
{
    private readonly LocationViewModel _viewModel;
    public LocationPage()
    {
        InitializeComponent();
        BindingContext = _viewModel = new LocationViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Refresh();
    }
}