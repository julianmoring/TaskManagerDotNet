using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class SessionEventRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();
    private Board _board = null!;
    private Column _column = null!;
    private Card _card = null!;
    private OpenCodeSession _session1 = null!;
    private OpenCodeSession _session2 = null!;

    public SessionEventRepositoryTests()
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

        var sessionRepo = new OpenCodeSessionRepository(_db.Context);
        _session1 = OpenCodeSession.Create(_card.Id, 1, "spec", "/a", TestClock.CreatedAt);
        _session2 = OpenCodeSession.Create(_card.Id, 1, "spec", "/b", TestClock.CreatedAt);
        sessionRepo.Add(_session1);
        sessionRepo.Add(_session2);
        _db.Context.SaveChanges();
    }

    public void Dispose() => _db.Dispose();

    [Fact]
    public async Task Add_persists_event_with_fk_to_session()
    {
        var repo = new SessionEventRepository(_db.Context);
        var evt = SessionEvent.Create(_session1.Id, EventKind.StdOut, "hello", TestClock.CreatedAt);

        repo.Add(evt);
        await _db.Context.SaveChangesAsync();

        Assert.NotEqual(0, evt.Id);
        var list = await repo.ListBySessionAsync(_session1.Id);
        Assert.Single(list);
        Assert.Equal(EventKind.StdOut, list[0].Kind);
        Assert.Equal("hello", list[0].Text);
    }

    [Fact]
    public async Task ListBySessionAsync_filters_by_session()
    {
        var repo = new SessionEventRepository(_db.Context);
        repo.Add(SessionEvent.Create(_session1.Id, EventKind.StdOut, "s1-1", TestClock.CreatedAt));
        repo.Add(SessionEvent.Create(_session1.Id, EventKind.StdErr, "s1-2", TestClock.CreatedAt));
        repo.Add(SessionEvent.Create(_session2.Id, EventKind.StdOut, "s2-1", TestClock.CreatedAt));
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListBySessionAsync(_session1.Id);

        Assert.Equal(2, list.Count);
        Assert.All(list, e => Assert.Equal(_session1.Id, e.SessionId));
    }

    [Fact]
    public async Task ListBySessionSinceAsync_filters_by_id_ascending()
    {
        var repo = new SessionEventRepository(_db.Context);
        var e1 = SessionEvent.Create(_session1.Id, EventKind.StdOut, "first", TestClock.CreatedAt);
        repo.Add(e1);
        var e2 = SessionEvent.Create(_session1.Id, EventKind.StdOut, "second", TestClock.CreatedAt);
        repo.Add(e2);
        var e3 = SessionEvent.Create(_session1.Id, EventKind.StdOut, "third", TestClock.CreatedAt);
        repo.Add(e3);
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListBySessionSinceAsync(_session1.Id, e1.Id);

        Assert.Equal(2, list.Count);
        Assert.Equal(new[] { "second", "third" }, list.Select(e => e.Text).ToArray());
    }
}
