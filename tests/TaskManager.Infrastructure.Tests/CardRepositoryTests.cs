using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class CardRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();
    private Board _board = null!;
    private Column _column = null!;
    private Card _card = null!;

    public CardRepositoryTests()
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
    public async Task Add_persists_with_fk_to_column()
    {
        var repo = new CardRepository(_db.Context);
        var card = Card.Create(_column.Id, "Second", 1, TestClock.CreatedAt);

        repo.Add(card);
        await _db.Context.SaveChangesAsync();

        Assert.NotEqual(0, card.Id);
        var fetched = await repo.GetByIdAsync(card.Id, withDetails: false);
        Assert.NotNull(fetched);
        Assert.Equal(_column.Id, fetched!.ColumnId);
        Assert.Equal("Second", fetched.Title);
    }

    [Fact]
    public async Task GetByIdAsync_without_details_returns_empty_subcollections()
    {
        var repo = new CardRepository(_db.Context);

        var fetched = await repo.GetByIdAsync(_card.Id, withDetails: false);

        Assert.NotNull(fetched);
        Assert.Empty(fetched!.Checklists);
        Assert.Empty(fetched.Comments);
        Assert.Empty(fetched.Labels);
        Assert.Empty(fetched.Specs);
        Assert.Empty(fetched.Sessions);
    }

    [Fact]
    public async Task GetByIdAsync_with_details_populates_subcollections()
    {
        var checklistRepo = new ChecklistRepository(_db.Context);
        var checklist = Checklist.Create(_card.Id, "List", TestClock.CreatedAt);
        checklistRepo.Add(checklist);
        await _db.Context.SaveChangesAsync();

        var itemRepo = new ChecklistItemRepository(_db.Context);
        itemRepo.Add(ChecklistItem.Create(checklist.Id, "Item 1", 0, TestClock.CreatedAt));
        itemRepo.Add(ChecklistItem.Create(checklist.Id, "Item 2", 1, TestClock.CreatedAt));

        var commentRepo = new CommentRepository(_db.Context);
        commentRepo.Add(Comment.Create(_card.Id, "Hello", TestClock.CreatedAt));

        var specRepo = new CardSpecRepository(_db.Context);
        specRepo.Add(CardSpec.Create(_card.Id, 1, "Body", TestClock.CreatedAt));

        var sessionRepo = new OpenCodeSessionRepository(_db.Context);
        sessionRepo.Add(OpenCodeSession.Create(_card.Id, 1, "spec", "/tmp", TestClock.CreatedAt));

        await _db.Context.SaveChangesAsync();

        var cardRepo = new CardRepository(_db.Context);
        var fetched = await cardRepo.GetByIdAsync(_card.Id, withDetails: true);

        Assert.NotNull(fetched);
        Assert.Single(fetched!.Checklists);
        Assert.Equal(2, fetched.Checklists[0].Items.Count);
        Assert.Single(fetched.Comments);
        Assert.Single(fetched.Specs);
        Assert.Single(fetched.Sessions);
    }

    [Fact]
    public async Task ListByColumnAsync_filters_and_orders_by_position()
    {
        var repo = new CardRepository(_db.Context);
        var other = Column.Create(_board.Id, "Other", 1, TestClock.CreatedAt);
        new ColumnRepository(_db.Context).Add(other);
        _db.Context.SaveChanges();

        _card.MoveTo(_column.Id, 5, TestClock.UpdatedAt);
        repo.Add(Card.Create(_column.Id, "A", 0, TestClock.CreatedAt));
        repo.Add(Card.Create(_column.Id, "B", 1, TestClock.CreatedAt));
        repo.Add(Card.Create(other.Id, "X", 0, TestClock.CreatedAt));
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListByColumnAsync(_column.Id);

        Assert.Equal(3, list.Count);
        Assert.Equal(new[] { "A", "B", "First" }, list.Select(c => c.Title).ToArray());
    }
}
