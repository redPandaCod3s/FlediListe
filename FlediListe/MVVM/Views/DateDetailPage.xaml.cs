using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlediListe.MVVM.ViewModels;

namespace FlediListe.MVVM.Views;

public partial class DateDetailPage : ContentPage
{
    private readonly DateDetailViewModel _viewModel;
    public DateDetailPage(DateDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs e)
    {
        _viewModel.InitializeAsync();
        base.OnNavigatedTo(e);
    }
    
}