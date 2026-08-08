using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class ColumnRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();
    private Board _board = null!;

    public ColumnRepositoryTests()
    {
        var boardRepo = new BoardRepository(_db.Context);
        _board = Board.Create("Work", null, TestClock.CreatedAt);
        boardRepo.Add(_board);
        _db.Context.SaveChanges();
    }

    public void Dispose() => _db.Dispose();

    [Fact]
    public async Task Add_persists_with_fk_to_board()
    {
        var repo = new ColumnRepository(_db.Context);
        var column = Column.Create(_board.Id, "Todo", 0, TestClock.CreatedAt);

        repo.Add(column);
        await _db.Context.SaveChangesAsync();

        Assert.NotEqual(0, column.Id);
        var fetched = await repo.GetByIdAsync(column.Id);
        Assert.NotNull(fetched);
        Assert.Equal(_board.Id, fetched!.BoardId);
        Assert.Equal("Todo", fetched.Name);
    }

    [Fact]
    public async Task ListByBoardAsync_filters_by_board()
    {
        var boardRepo = new BoardRepository(_db.Context);
        var other = Board.Create("Other", null, TestClock.CreatedAt);
        boardRepo.Add(other);
        _db.Context.SaveChanges();

        var repo = new ColumnRepository(_db.Context);
        repo.Add(Column.Create(_board.Id, "A", 0, TestClock.CreatedAt));
        repo.Add(Column.Create(_board.Id, "B", 1, TestClock.CreatedAt));
        repo.Add(Column.Create(other.Id, "X", 0, TestClock.CreatedAt));
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListByBoardAsync(_board.Id);

        Assert.Equal(2, list.Count);
        Assert.All(list, c => Assert.Equal(_board.Id, c.BoardId));
    }

    [Fact]
    public async Task GetByIdAsync_returns_null_for_unknown_id()
    {
        var repo = new ColumnRepository(_db.Context);

        var fetched = await repo.GetByIdAsync(999_999);

        Assert.Null(fetched);
    }

    [Fact]
    public async Task Remove_deletes_column()
    {
        var repo = new ColumnRepository(_db.Context);
        var column = Column.Create(_board.Id, "Doomed", 0, TestClock.CreatedAt);
        repo.Add(column);
        await _db.Context.SaveChangesAsync();

        repo.Remove(column);
        await _db.Context.SaveChangesAsync();

        var fetched = await repo.GetByIdAsync(column.Id);
        Assert.Null(fetched);
    }
}
