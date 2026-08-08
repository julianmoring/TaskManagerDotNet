using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class BoardRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();

    public void Dispose() => _db.Dispose();

    [Fact]
    public async Task Add_persists_and_GetByIdAsync_returns_it()
    {
        var repo = new BoardRepository(_db.Context);
        var board = Board.Create("Work", "desc", TestClock.CreatedAt);

        repo.Add(board);
        await _db.Context.SaveChangesAsync();

        Assert.NotEqual(0, board.Id);
        var fetched = await repo.GetByIdAsync(board.Id);
        Assert.NotNull(fetched);
        Assert.Equal("Work", fetched!.Name);
    }

    [Fact]
    public async Task GetByIdAsync_returns_null_for_unknown_id()
    {
        var repo = new BoardRepository(_db.Context);

        var fetched = await repo.GetByIdAsync(999_999);

        Assert.Null(fetched);
    }

    [Fact]
    public async Task ListAsync_returns_all_saved_boards()
    {
        var repo = new BoardRepository(_db.Context);
        repo.Add(Board.Create("A", null, TestClock.CreatedAt));
        repo.Add(Board.Create("B", null, TestClock.CreatedAt));
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListAsync();

        Assert.Equal(2, list.Count);
        Assert.Equal(new[] { "A", "B" }, list.Select(b => b.Name).ToArray());
    }

    [Fact]
    public async Task Remove_deletes_and_GetByIdAsync_returns_null()
    {
        var repo = new BoardRepository(_db.Context);
        var board = Board.Create("Doomed", null, TestClock.CreatedAt);
        repo.Add(board);
        await _db.Context.SaveChangesAsync();

        repo.Remove(board);
        await _db.Context.SaveChangesAsync();

        var fetched = await repo.GetByIdAsync(board.Id);
        Assert.Null(fetched);
    }
}
