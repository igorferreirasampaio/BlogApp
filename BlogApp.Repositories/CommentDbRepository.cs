using BlogApp.Data;
using BlogApp.Core.Interfaces.Repositories;
using BlogApp.Core.Models;

namespace BlogApp.Repositories;

public class CommentDbRepository(DatabaseContext dbContext) : ICommentDbRepository
{
    private readonly DatabaseContext _dbContext = dbContext;

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _dbContext.Connection.Table<Comment>().ToListAsync();
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        return await _dbContext.Connection.Table<Comment>().Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public Task SaveAsync(Comment entity)
    {
        if (entity.Id != 0)
        {
            return _dbContext.Connection.UpdateAsync(entity);
        }

        return _dbContext.Connection.InsertAsync(entity);
    }

    public async Task SaveAllAsync(List<Comment> entities)
    {
        await _dbContext.Connection.InsertAllAsync(entities);
    }

    public Task DeleteAsync(Comment entity)
    {
        return _dbContext.Connection.DeleteAsync<Comment>(entity);
    }

    public Task DeleteAllAsync()
    {
        return _dbContext.Connection.DeleteAllAsync<Comment>();
    }

    public Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return _dbContext.Connection.Table<Comment>().Where(c => c.PostId == postId).ToListAsync();
    }
}