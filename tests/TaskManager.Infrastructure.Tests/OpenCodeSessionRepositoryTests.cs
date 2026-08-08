using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class OpenCodeSessionRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();
    private Board _board = null!;
    private Column _column = null!;
    private Card _card = null!;

    public OpenCodeSessionRepositoryTests()
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
    public async Task Add_persists_with_Pending_status()
    {
        var repo = new OpenCodeSessionRepository(_db.Context);
        var session = OpenCodeSession.Create(_card.Id, 1, "spec body", "/tmp/ws", TestClock.CreatedAt);

        repo.Add(session);
        await _db.Context.SaveChangesAsync();

        Assert.NotEqual(0, session.Id);
        var fetched = await repo.GetByIdAsync(session.Id);
        Assert.NotNull(fetched);
        Assert.Equal(SessionStatus.Pending, fetched!.Status);
        Assert.Equal(1, fetched.SpecVersion);
        Assert.Equal("spec body", fetched.SpecBodySnapshot);
        Assert.Equal("/tmp/ws", fetched.WorkspacePath);
    }

    [Fact]
    public async Task MarkRunning_persists_status_pid_and_startedAt()
    {
        var repo = new OpenCodeSessionRepository(_db.Context);
        var session = OpenCodeSession.Create(_card.Id, 1, "spec body", "/tmp/ws", TestClock.CreatedAt);
        repo.Add(session);
        await _db.Context.SaveChangesAsync();

        var startedAt = TestClock.UpdatedAt;
        session.MarkRunning(1234, startedAt);
        await _db.Context.SaveChangesAsync();

        var fetched = await repo.GetByIdAsync(session.Id);
        Assert.NotNull(fetched);
        Assert.Equal(SessionStatus.Running, fetched!.Status);
        Assert.Equal(1234, fetched.Pid);
        Assert.Equal(startedAt, fetched.StartedAt);
    }

    [Fact]
    public async Task ListByCardAsync_returns_sessions_ordered_by_CreatedAt_descending()
    {
        var repo = new OpenCodeSessionRepository(_db.Context);
        var t1 = TestClock.CreatedAt;
        var t2 = t1.AddMinutes(5);
        var t3 = t1.AddMinutes(10);

        repo.Add(OpenCodeSession.Create(_card.Id, 1, "a", "/a", t1));
        repo.Add(OpenCodeSession.Create(_card.Id, 1, "b", "/b", t2));
        repo.Add(OpenCodeSession.Create(_card.Id, 1, "c", "/c", t3));
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListByCardAsync(_card.Id);

        Assert.Equal(new[] { "c", "b", "a" }, list.Select(s => s.SpecBodySnapshot).ToArray());
    }
}
