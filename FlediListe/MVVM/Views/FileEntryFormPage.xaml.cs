using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlediListe.MVVM.ViewModels;

namespace FlediListe.MVVM.Views;

public partial class FileEntryFormPage : ContentPage
{
    private readonly FileEntryFormViewModel _viewModel;
    
    public FileEntryFormPage(FileEntryFormViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel =  viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

    }
    
    
}