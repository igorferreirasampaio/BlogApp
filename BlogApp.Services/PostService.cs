using BlogApp.Core.Exceptions;
using BlogApp.Core.Interfaces.Repositories;
using BlogApp.Core.Interfaces.Services;
using BlogApp.Core.Models;

namespace BlogApp.Services;

public class PostService(IPostApiRepository apiRepository, IPostDbRepository dbRepository) : IPostService
{
    private readonly IPostApiRepository _apiRepository = apiRepository;
    private readonly IPostDbRepository _dbRepository = dbRepository;

    public async Task<List<Post>> GetPostsAsync()
    {
        List<Post> posts;

        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            try
            {
                posts = await _apiRepository.GetAllAsync();

                if (posts != null && posts.Count != 0)
                {
                    try
                    {
                        await _dbRepository.DeleteAllAsync();
                        await _dbRepository.SaveAllAsync(posts);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    return posts;
                }
                else
                {
                    try
                    {
                        posts = await _dbRepository.GetAllAsync();

                        if (posts != null && posts.Count != 0)
                        {
                            return posts;
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

    public async Task<Post> GetPostByIdAsync(int id)
    {
        var post = await _dbRepository.GetByIdAsync(id);

        if (post != null)
        {
            return post;
        }

        return post!;
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        var createdPost = await _apiRepository.CreateAsync(post);

        await _dbRepository.SaveAsync(createdPost);

        return createdPost;
    }

    public async Task<Post> UpdatePostAsync(Post post)
    {
        var updatedPost = await _apiRepository.UpdateAsync(post);

        await _dbRepository.SaveAsync(updatedPost);

        return updatedPost;
    }

    public async Task DeletePostAsync(int id)
    {
        await _apiRepository.DeleteAsync(id);

        var postToDelete = await _dbRepository.GetByIdAsync(id);

        if (postToDelete != null)
        {
            await _dbRepository.DeleteAsync(postToDelete);
        }
    }
}