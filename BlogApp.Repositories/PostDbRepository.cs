using BlogApp.Data;
using BlogApp.Core.Interfaces.Repositories;
using BlogApp.Core.Models;

namespace BlogApp.Repositories;

public class PostDbRepository(DatabaseContext dbContext) : IPostDbRepository
{
    private readonly DatabaseContext _dbContext = dbContext;

    public async Task<List<Post>> GetAllAsync()
    {
        return await _dbContext.Connection.Table<Post>().ToListAsync();
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        return await _dbContext.Connection.Table<Post>().Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public Task SaveAsync(Post entity)
    {
        if (entity.Id != 0)
        {
            return _dbContext.Connection.UpdateAsync(entity);
        }

        return _dbContext.Connection.InsertAsync(entity);
    }

    public async Task SaveAllAsync(List<Post> entities)
    {
        await _dbContext.Connection.InsertAllAsync(entities);
    }

    public Task DeleteAsync(Post entity)
    {
        return _dbContext.Connection.DeleteAsync<Post>(entity);
    }

    public Task DeleteAllAsync()
    {
        return _dbContext.Connection.DeleteAllAsync<Post>();
    }
}