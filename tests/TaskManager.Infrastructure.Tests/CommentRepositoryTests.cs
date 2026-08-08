using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class CommentRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();
    private Board _board = null!;
    private Column _column = null!;
    private Card _card = null!;

    public CommentRepositoryTests()
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
    public async Task Add_persists_with_fk_to_card()
    {
        var repo = new CommentRepository(_db.Context);
        var comment = Comment.Create(_card.Id, "First comment", TestClock.CreatedAt);

        repo.Add(comment);
        await _db.Context.SaveChangesAsync();

        Assert.NotEqual(0, comment.Id);
        var stored = await _db.Context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);
        Assert.NotNull(stored);
        Assert.Equal(_card.Id, stored!.CardId);
        Assert.Equal("First comment", stored.Body);
    }

    [Fact]
    public async Task ListByCardAsync_orders_by_CreatedAt()
    {
        var repo = new CommentRepository(_db.Context);
        var t1 = TestClock.CreatedAt;
        var t2 = t1.AddMinutes(5);
        var t3 = t1.AddMinutes(10);

        repo.Add(Comment.Create(_card.Id, "third", t3));
        repo.Add(Comment.Create(_card.Id, "first", t1));
        repo.Add(Comment.Create(_card.Id, "second", t2));
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListByCardAsync(_card.Id);

        Assert.Equal(new[] { "first", "second", "third" }, list.Select(c => c.Body).ToArray());
    }
}
