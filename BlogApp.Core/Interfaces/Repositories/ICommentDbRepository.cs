using BlogApp.Core.Models;

namespace BlogApp.Core.Interfaces.Repositories;

public interface ICommentDbRepository : IDbRepository<Comment>
{
    Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
}