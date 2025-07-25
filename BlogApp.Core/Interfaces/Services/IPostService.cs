using BlogApp.Core.Models;

namespace BlogApp.Core.Interfaces.Services;

public interface IPostService
{
    Task<List<Post>> GetPostsAsync();
    Task<Post> GetPostByIdAsync(int id);
    Task<Post> CreatePostAsync(Post post);
    Task<Post> UpdatePostAsync(Post post);
    Task DeletePostAsync(int id);
}