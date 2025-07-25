using SQLite;
using BlogApp.Core.Models;

namespace BlogApp.Data;
public class DatabaseContext
{
    private readonly Lazy<SQLiteAsyncConnection> _database;
    private readonly string _dbPath;

    public DatabaseContext()
    {
        _dbPath = Path.Combine(FileSystem.AppDataDirectory, "BlogData.db3");
        _database = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(_dbPath));
    }

    public SQLiteAsyncConnection Connection => _database.Value;

    public async Task InitializeAsync()
    {
        await _database.Value.CreateTableAsync<Post>();
        await _database.Value.CreateTableAsync<Comment>();
    }
}