using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Tests;

public sealed class DbContextMigrationTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly TaskManagerDbContext _db;

    public DbContextMigrationTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseSqlite(_connection)
            .Options;
        _db = new TaskManagerDbContext(options);
    }

    public void Dispose()
    {
        _db.Dispose();
        _connection.Dispose();
    }

    [Fact]
    public void Migrate_creates_all_12_tables()
    {
        _db.Database.Migrate();

        var expected = new[]
        {
            "Board", "Column", "Card", "Label", "CardLabel",
            "Checklist", "ChecklistItem", "Comment", "ActivityLog",
            "CardSpec", "OpenCodeSession", "SessionEvent"
        };

        using var cmd = _connection.CreateCommand();
        cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table'";
        using var reader = cmd.ExecuteReader();
        var present = new HashSet<string>();
        while (reader.Read())
        {
            present.Add(reader.GetString(0));
        }

        foreach (var table in expected)
        {
            Assert.Contains(table, present);
        }
    }

    [Fact]
    public void Migrate_is_idempotent()
    {
        _db.Database.Migrate();
        _db.Database.Migrate();
    }
}
