using BlogApp.Core.Models;

namespace BlogApp.Core.Interfaces.Services;

public interface ICommentService
{
    Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
    Task<Comment> GetCommentByIdAsync(int id);
    Task<Comment> CreateCommentAsync(Comment comment);
    Task<Comment> UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(int id);
}