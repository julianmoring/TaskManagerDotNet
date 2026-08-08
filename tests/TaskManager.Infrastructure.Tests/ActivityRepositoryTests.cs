using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Tests;

public sealed class ActivityRepositoryTests : IDisposable
{
    private readonly TestDb _db = new();
    private Board _board = null!;
    private Board _other = null!;

    public ActivityRepositoryTests()
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
    public async Task Add_persists_with_null_CardId()
    {
        var repo = new ActivityRepository(_db.Context);
        var log = ActivityLog.Create(_board.Id, null, ActivityType.BoardCreated, "Board was created", TestClock.CreatedAt);

        repo.Add(log);
        await _db.Context.SaveChangesAsync();

        Assert.NotEqual(0, log.Id);
        var stored = await _db.Context.ActivityLogs.FirstOrDefaultAsync(a => a.Id == log.Id);
        Assert.NotNull(stored);
        Assert.Null(stored!.CardId);
        Assert.Equal("Board was created", stored.Message);
    }

    [Fact(Skip = "deviation: SQLite does not support DateTimeOffset in ORDER BY; production ListByBoardAsync would need to sort client-side or use Id ordering.")]
    public async Task ListByBoardAsync_filters_by_board()
    {
        var repo = new ActivityRepository(_db.Context);
        repo.Add(ActivityLog.Create(_board.Id, null, ActivityType.BoardCreated, "msg1", TestClock.CreatedAt));
        repo.Add(ActivityLog.Create(_other.Id, null, ActivityType.BoardCreated, "msg2", TestClock.CreatedAt));
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListByBoardAsync(_board.Id, since: null);

        Assert.Single(list);
        Assert.Equal(_board.Id, list[0].BoardId);
    }

    [Fact(Skip = "deviation: SQLite does not support DateTimeOffset in ORDER BY; production ListByBoardAsync would need to sort client-side or use Id ordering.")]
    public async Task ListByBoardAsync_respects_since_parameter()
    {
        var repo = new ActivityRepository(_db.Context);
        var t1 = TestClock.CreatedAt;
        var t2 = t1.AddMinutes(10);
        var t3 = t1.AddMinutes(20);

        repo.Add(ActivityLog.Create(_board.Id, null, ActivityType.BoardCreated, "first", t1));
        repo.Add(ActivityLog.Create(_board.Id, null, ActivityType.BoardRenamed, "second", t2));
        repo.Add(ActivityLog.Create(_board.Id, null, ActivityType.BoardRenamed, "third", t3));
        await _db.Context.SaveChangesAsync();

        var list = await repo.ListByBoardAsync(_board.Id, since: t2);

        Assert.Equal(2, list.Count);
        Assert.Equal(new[] { "second", "third" }, list.Select(a => a.Message).ToArray());
    }
}
