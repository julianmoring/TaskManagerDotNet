using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class LabelRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();
    private Board _board = null!;
    private Board _other = null!;

    public LabelRepositoryTests()
    {
        var boardRepo = new BoardRepository(_db.Context);
        _board = Board.Create("Work", null, TestClock.CreatedAt);
        _other = Board.Create("Other", null, TestClock.CreatedAt);
        boardRepo.Add(_board);
        boardRepo.Add(_other);
        _db.Context.SaveChanges();
    }

    public void Dispose() => _db.Dispose();

    [Fact]
    public async Task Add_persists_label()
    {
        var repo = new LabelRepository(_db.Context);
        var label = Label.Create(_board.Id, "bug", "#ff0000", TestClock.CreatedAt);

        repo.Add(label);
        await _db.Context.SaveChangesAsync();

        Assert.NotEqual(0, label.Id);
        var fetched = await repo.GetByIdAsync(label.Id);
        Assert.NotNull(fetched);
        Assert.Equal("bug", fetched!.Name);
        Assert.Equal("#ff0000", fetched.Color);
    }

    [Fact]
    public async Task Recolor_persists_color_change()
    {
        var repo = new LabelRepository(_db.Context);
        var label = Label.Create(_board.Id, "bug", "#ff0000", TestClock.CreatedAt);
        repo.Add(label);
        await _db.Context.SaveChangesAsync();

        label.Recolor("#00ff00");
        await _db.Context.SaveChangesAsync();

        var fetched = await repo.GetByIdAsync(label.Id);
        Assert.NotNull(fetched);
        Assert.Equal("#00ff00", fetched!.Color);
    }

    [Fact]
    public async Task ListByBoardAsync_filters_by_board()
    {
        var repo = new LabelRepository(_db.Context);
        repo.Add(Label.Create(_board.Id, "bug", "#ff0000", TestClock.CreatedAt));
        repo.Add(Label.Create(_board.Id, "feature", "#00ff00", TestClock.CreatedAt));
        repo.Add(Label.Create(_other.Id, "chore", "#0000ff", TestClock.CreatedAt));
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListByBoardAsync(_board.Id);

        Assert.Equal(2, list.Count);
        Assert.All(list, l => Assert.Equal(_board.Id, l.BoardId));
    }
}
