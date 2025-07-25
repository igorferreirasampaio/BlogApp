using BlogApp.Core.Exceptions;
using BlogApp.Core.Interfaces.Services;
using BlogApp.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BlogApp.ViewModels;

public partial class PostsViewModel : ObservableObject
{
    private readonly IPostService _postService;

    [ObservableProperty]
    private ObservableCollection<Post> _posts;

    [ObservableProperty]
    private bool _isLoading;

    public event EventHandler<AlertRequestedEventArgs> AlertRequested;

    public AsyncRelayCommand LoadPostsCommand { get; }

    public PostsViewModel(IPostService postService)
    {
        _postService = postService;
        LoadPostsCommand = new AsyncRelayCommand(LoadPostsAsync);
    }

    public async Task LoadPostsAsync()
    {
        IsLoading = true;
        Posts = [];

        try
        {
            var fetchedPosts = await _postService.GetPostsAsync();
            Posts.Clear();

            foreach (var post in fetchedPosts)
            {
                Posts.Add(post);
            }
        }
        catch (HttpRequestException)
        {
            OnAlertRequested("Problema de Conexão", "Não foi possível conectar ao servidor. Verifique sua conexão com a internet ou tente novamente mais tarde.", "Fechar");
        }
        catch (NoLocalDataException ex)
        {
            OnAlertRequested("Dados Locais Vazios", ex.Message, "Fechar");
        }
        catch (NoConnectivityAndNoLocalDataException ex)
        {
            OnAlertRequested("Dados Indisponíveis", ex.Message, "Fechar");
        }
        catch (Exception ex)
        {
            OnAlertRequested("Erro Inesperado", $"Ocorreu um erro: {ex.Message}", "Fechar");
        }
        finally
        {
            IsLoading = false;
        }
    }

    protected virtual void OnAlertRequested(string title, string message, string cancelButton = "OK", string acceptButton = null, bool isConfirmation = false)
    {
        AlertRequested?.Invoke(this, new AlertRequestedEventArgs
        {
            Title = title,
            Message = message,
            CancelButton = cancelButton,
            AcceptButton = acceptButton,
            IsConfirmation = isConfirmation
        });
    }
}