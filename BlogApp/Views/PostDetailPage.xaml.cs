using BlogApp.Core.Interfaces.Services;
using BlogApp.Core.Models;
using BlogApp.ViewModels;

namespace BlogApp.Views;

public partial class PostDetailPage : ContentPage
{
    private readonly PostDetailViewModel _viewModel;
    private readonly IAlertService _alertService;

    public PostDetailPage(PostDetailViewModel viewModel, IAlertService alertService)
    {
        InitializeComponent();

        _viewModel = viewModel;
        _alertService = alertService;

        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.AlertRequested += OnViewModelAlertRequested;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _viewModel.AlertRequested -= OnViewModelAlertRequested;
    }

    private async void OnViewModelAlertRequested(object sender, AlertRequestedEventArgs e)
    {
        if (e.IsConfirmation)
        {
            await _alertService.SendConfirmationMessageAsync(
                this,
                e.Title,
                e.Message,
                e.AcceptButton,
                e.CancelButton
            );
        }
        else
        {
            await _alertService.SendAlertMessageAsync(
                this,
                e.Title,
                e.Message,
                e.CancelButton
            );
        }
    }
}