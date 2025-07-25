using BlogApp.Core.Models;

namespace BlogApp.Core.Interfaces.Repositories;

public interface ICommentApiRepository : IApiRepository<Comment>
{
    Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
}