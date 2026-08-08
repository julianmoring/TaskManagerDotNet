using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class ChecklistItemRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();
    private Checklist _checklist = null!;
    private ChecklistItem _item = null!;

    public ChecklistItemRepositoryTests()
    {
        var boardRepo = new BoardRepository(_db.Context);
        var board = Board.Create("Work", null, TestClock.CreatedAt);
        boardRepo.Add(board);
        _db.Context.SaveChanges();

        var columnRepo = new ColumnRepository(_db.Context);
        var column = Column.Create(board.Id, "Todo", 0, TestClock.CreatedAt);
        columnRepo.Add(column);
        _db.Context.SaveChanges();

        var cardRepo = new CardRepository(_db.Context);
        var card = Card.Create(column.Id, "First", 0, TestClock.CreatedAt);
        cardRepo.Add(card);
        _db.Context.SaveChanges();

        var checklistRepo = new ChecklistRepository(_db.Context);
        _checklist = Checklist.Create(card.Id, "Tasks", TestClock.CreatedAt);
        checklistRepo.Add(_checklist);
        _db.Context.SaveChanges();

        var itemRepo = new ChecklistItemRepository(_db.Context);
        _item = ChecklistItem.Create(_checklist.Id, "Item 1", 0, TestClock.CreatedAt);
        itemRepo.Add(_item);
        _db.Context.SaveChanges();
    }

    public void Dispose() => _db.Dispose();

    [Fact]
    public async Task Toggle_flips_IsDone()
    {
        var repo = new ChecklistItemRepository(_db.Context);
        Assert.False(_item.IsDone);

        _item.Toggle();
        await _db.Context.SaveChangesAsync();

        var fetched = await repo.GetByIdAsync(_item.Id);
        Assert.NotNull(fetched);
        Assert.True(fetched!.IsDone);
    }

    [Fact]
    public async Task Update_changes_text()
    {
        var repo = new ChecklistItemRepository(_db.Context);

        _item.Update("Updated text");
        await _db.Context.SaveChangesAsync();

        var fetched = await repo.GetByIdAsync(_item.Id);
        Assert.NotNull(fetched);
        Assert.Equal("Updated text", fetched!.Text);
    }
}
