using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class CardSpecRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();
    private Board _board = null!;
    private Column _column = null!;
    private Card _card = null!;

    public CardSpecRepositoryTests()
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
    public async Task Add_persists_spec()
    {
        var repo = new CardSpecRepository(_db.Context);
        var spec = CardSpec.Create(_card.Id, 1, "v1 body", TestClock.CreatedAt);

        repo.Add(spec);
        await _db.Context.SaveChangesAsync();

        Assert.NotEqual(0, spec.Id);
        var fetched = await repo.GetByVersionAsync(_card.Id, 1);
        Assert.NotNull(fetched);
        Assert.Equal("v1 body", fetched!.BodyMarkdown);
    }

    [Fact]
    public async Task Adding_same_CardId_Version_twice_throws_DbUpdateException()
    {
        var repo = new CardSpecRepository(_db.Context);
        repo.Add(CardSpec.Create(_card.Id, 1, "v1 body", TestClock.CreatedAt));
        repo.Add(CardSpec.Create(_card.Id, 1, "duplicate", TestClock.CreatedAt));

        await Assert.ThrowsAnyAsync<DbUpdateException>(() => _db.Context.SaveChangesAsync());
    }

    [Fact]
    public async Task GetLatestAsync_returns_highest_version()
    {
        var repo = new CardSpecRepository(_db.Context);
        repo.Add(CardSpec.Create(_card.Id, 1, "v1 body", TestClock.CreatedAt));
        repo.Add(CardSpec.Create(_card.Id, 3, "v3 body", TestClock.CreatedAt));
        repo.Add(CardSpec.Create(_card.Id, 2, "v2 body", TestClock.CreatedAt));
        await _db.Context.SaveChangesAsync();

        var latest = await repo.GetLatestAsync(_card.Id);

        Assert.NotNull(latest);
        Assert.Equal(3, latest!.Version);
        Assert.Equal("v3 body", latest.BodyMarkdown);
    }

    [Fact]
    public async Task ListVersionsAsync_orders_by_Version_ascending()
    {
        var repo = new CardSpecRepository(_db.Context);
        repo.Add(CardSpec.Create(_card.Id, 1, "v1", TestClock.CreatedAt));
        repo.Add(CardSpec.Create(_card.Id, 3, "v3", TestClock.CreatedAt));
        repo.Add(CardSpec.Create(_card.Id, 2, "v2", TestClock.CreatedAt));
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListVersionsAsync(_card.Id);

        Assert.Equal(new[] { 1, 2, 3 }, list.Select(s => s.Version).ToArray());
    }

    [Fact]
    public async Task GetByVersionAsync_returns_specific_version()
    {
        var repo = new CardSpecRepository(_db.Context);
        repo.Add(CardSpec.Create(_card.Id, 1, "v1", TestClock.CreatedAt));
        repo.Add(CardSpec.Create(_card.Id, 2, "v2", TestClock.CreatedAt));
        await _db.Context.SaveChangesAsync();

        var fetched = await repo.GetByVersionAsync(_card.Id, 2);

        Assert.NotNull(fetched);
        Assert.Equal("v2", fetched!.BodyMarkdown);
    }
}
