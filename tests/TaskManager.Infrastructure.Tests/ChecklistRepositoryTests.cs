using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class ChecklistRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();
    private Board _board = null!;
    private Column _column = null!;
    private Card _card = null!;

    public ChecklistRepositoryTests()
    {
        var boardRepo = new BoardRepository(_db.Context);
        _board = Board.Create("Work", null, TestClock.CreatedAt);
        boardRepo.Add(_board);
        _db.Context.SaveChanges();

        var columnRepo = new ColumnRepository(_db.Context);
        _column = Column.Create(_board.Id, "Todo", 0, TestClock.CreatedAt);
        columnRepo.Add(_column);
        _db.Context.SaveChanges();

        var cardRepo = new CardRepository(_db.Context);
        _card = Card.Create(_column.Id, "First", 0, TestClock.CreatedAt);
        cardRepo.Add(_card);
        _db.Context.SaveChanges();
    }

    public void Dispose() => _db.Dispose();

    [Fact]
    public async Task Add_persists_with_empty_items()
    {
        var repo = new ChecklistRepository(_db.Context);
        var checklist = Checklist.Create(_card.Id, "Tasks", TestClock.CreatedAt);

        repo.Add(checklist);
        await _db.Context.SaveChangesAsync();

        Assert.NotEqual(0, checklist.Id);
        var fetched = await repo.GetByIdAsync(checklist.Id);
        Assert.NotNull(fetched);
        Assert.Equal("Tasks", fetched!.Title);
        Assert.Empty(fetched.Items);
    }

    [Fact]
    public async Task ListByCardAsync_includes_items()
    {
        var repo = new ChecklistRepository(_db.Context);
        var checklist = Checklist.Create(_card.Id, "Tasks", TestClock.CreatedAt);
        repo.Add(checklist);
        await _db.Context.SaveChangesAsync();

        var itemRepo = new ChecklistItemRepository(_db.Context);
        itemRepo.Add(ChecklistItem.Create(checklist.Id, "Item 1", 0, TestClock.CreatedAt));
        itemRepo.Add(ChecklistItem.Create(checklist.Id, "Item 2", 1, TestClock.CreatedAt));
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListByCardAsync(_card.Id);

        Assert.Single(list);
        Assert.Equal(2, list[0].Items.Count);
        Assert.Equal(new[] { "Item 1", "Item 2" }, list[0].Items.Select(i => i.Text).ToArray());
    }
}
