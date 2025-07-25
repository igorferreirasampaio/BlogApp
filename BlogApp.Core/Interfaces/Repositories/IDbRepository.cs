namespace BlogApp.Core.Interfaces.Repositories;

public interface IDbRepository<T> where T : class, new()
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task SaveAsync(T entity);
    Task SaveAllAsync(List<T> entities);
    Task DeleteAsync(T entity);
    Task DeleteAllAsync();
}