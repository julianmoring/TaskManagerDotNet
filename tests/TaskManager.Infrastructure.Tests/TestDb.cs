using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Tests;

internal sealed class TestDb : IDisposable
{
    private readonly SqliteConnection _connection;

    public TaskManagerDbContext Context { get; }

    public TestDb()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseSqlite(_connection)
            .Options;
        Context = new TaskManagerDbContext(options);
        Context.Database.Migrate();
    }

    public void Dispose()
    {
        Context.Dispose();
        _connection.Dispose();
    }
}
