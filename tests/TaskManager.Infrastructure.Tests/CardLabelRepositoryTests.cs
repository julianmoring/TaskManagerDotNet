using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class CardLabelRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();
    private Board _board = null!;
    private Column _column = null!;
    private Card _card = null!;
    private Label _label1 = null!;
    private Label _label2 = null!;

    public CardLabelRepositoryTests()
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

        var labelRepo = new LabelRepository(_db.Context);
        _label1 = Label.Create(_board.Id, "bug", "#ff0000", TestClock.CreatedAt);
        _label2 = Label.Create(_board.Id, "feature", "#00ff00", TestClock.CreatedAt);
        labelRepo.Add(_label1);
        labelRepo.Add(_label2);
        _db.Context.SaveChanges();
    }

    public void Dispose() => _db.Dispose();

    [Fact]
    public async Task Add_persists_join()
    {
        var repo = new CardLabelRepository(_db.Context);
        var join = CardLabel.Create(_card.Id, _label1.Id, TestClock.CreatedAt);

        repo.Add(join);
        await _db.Context.SaveChangesAsync();

        Assert.NotEqual(0, join.Id);
        var list = await repo.ListByCardAsync(_card.Id);
        Assert.Single(list);
        Assert.Equal(_label1.Id, list[0].LabelId);
    }

    [Fact]
    public async Task Adding_same_pair_twice_throws_DbUpdateException()
    {
        var repo = new CardLabelRepository(_db.Context);
        repo.Add(CardLabel.Create(_card.Id, _label1.Id, TestClock.CreatedAt));
        repo.Add(CardLabel.Create(_card.Id, _label1.Id, TestClock.CreatedAt));

        await Assert.ThrowsAnyAsync<DbUpdateException>(() => _db.Context.SaveChangesAsync());
    }

    [Fact]
    public async Task Adding_distinct_pairs_succeeds()
    {
        var repo = new CardLabelRepository(_db.Context);
        repo.Add(CardLabel.Create(_card.Id, _label1.Id, TestClock.CreatedAt));
        repo.Add(CardLabel.Create(_card.Id, _label2.Id, TestClock.CreatedAt));

        await _db.Context.SaveChangesAsync();

        var list = await repo.ListByCardAsync(_card.Id);
        Assert.Equal(2, list.Count);
    }
}
