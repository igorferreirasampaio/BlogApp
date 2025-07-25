using System.Collections.ObjectModel;
using BlogApp.Core.Exceptions;
using BlogApp.Core.Interfaces.Services;
using BlogApp.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlogApp.ViewModels;

public partial class PostDetailViewModel : ObservableObject, IQueryAttributable
{
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;

    [ObservableProperty]
    private Post? _post;

    [ObservableProperty]
    private ObservableCollection<Comment> _comments;

    [ObservableProperty]
    private bool _isLoadingComments;

    public event EventHandler<AlertRequestedEventArgs> AlertRequested;

    public AsyncRelayCommand LoadCommentsCommand { get; }

    public PostDetailViewModel(IPostService postService, ICommentService commentService)
    {
        _postService = postService;
        _commentService = commentService;

        LoadCommentsCommand = new AsyncRelayCommand(LoadCommentsAsync);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("PostId", out object? value))
        {
            int postId = int.Parse(value.ToString()!);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                Post = await _postService.GetPostByIdAsync(postId);
                LoadCommentsCommand.Execute(null);
            });
        }
    }

    public async Task LoadCommentsAsync()
    {
        if (Post == null) return;

        IsLoadingComments = true;
        Comments = [];

        try
        {
            var fetchedComments = await _commentService.GetCommentsByPostIdAsync(Post.Id);
            Comments.Clear();

            foreach (var comment in fetchedComments)
            {
                Comments.Add(comment);
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
            IsLoadingComments = false;
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