using BlogApp.Core.Exceptions;
using BlogApp.Core.Interfaces.Repositories;
using BlogApp.Core.Interfaces.Services;
using BlogApp.Core.Models;

namespace BlogApp.Services;

public class CommentService(ICommentApiRepository apiRepository, ICommentDbRepository dbRepository) : ICommentService
{
    private readonly ICommentApiRepository _apiRepository = apiRepository;
    private readonly ICommentDbRepository _dbRepository = dbRepository;

    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        List<Comment> comments;

        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            try
            {
                comments = await _apiRepository.GetCommentsByPostIdAsync(postId);

                if (comments != null && comments.Count != 0)
                {
                    try
                    {
                        await _dbRepository.DeleteAllAsync();
                        await _dbRepository.SaveAllAsync(comments);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    return comments;
                }
                else
                {
                    try
                    {
                        comments = await _dbRepository.GetCommentsByPostIdAsync(postId);

                        if (comments != null && comments.Count != 0)
                        {
                            return comments;
                        }
                        else
                        {
                            throw new NoLocalDataException("Não há dados locais disponíveis.");
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        else
        {
            throw new NoConnectivityAndNoLocalDataException("Você está offline e não há dados salvos localmente. Por favor, conecte-se à internet para carregar as informações.");
        }
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        var comment = await _dbRepository.GetByIdAsync(id);

        if (comment != null)
        {
            return comment;
        }

        return comment!;
    }

    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        var createdComment = await _apiRepository.CreateAsync(comment);

        await _dbRepository.SaveAsync(createdComment);

        return createdComment;
    }

    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        var updatedComment = await _apiRepository.UpdateAsync(comment);

        await _dbRepository.SaveAsync(updatedComment);

        return updatedComment;
    }

    public async Task DeleteCommentAsync(int id)
    {
        await _apiRepository.DeleteAsync(id);

        var commentToDelete = await _dbRepository.GetByIdAsync(id);

        if (commentToDelete != null)
        {
            await _dbRepository.DeleteAsync(commentToDelete);
        }
    }
}