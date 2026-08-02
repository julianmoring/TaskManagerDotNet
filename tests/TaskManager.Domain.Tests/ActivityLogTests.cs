using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Tests;

public sealed class ActivityLogTests
{
    [Fact]
    public void Create_WithCardEvent_SetsAllProperties()
    {
        var log = ActivityLog.Create(boardId: 1, cardId: 1, ActivityType.CardCreated, "msg", Clock.CreatedAt);

        Assert.Equal(1L, log.BoardId);
        Assert.Equal(1L, log.CardId);
        Assert.Equal(ActivityType.CardCreated, log.Type);
        Assert.Equal("msg", log.Message);
        Assert.Equal(Clock.CreatedAt, log.CreatedAt);
        Assert.Equal(0L, log.Id);
    }

    [Fact]
    public void Create_WithBoardLevelEvent_AllowsNullCardId()
    {
        var log = ActivityLog.Create(boardId: 1, cardId: null, ActivityType.BoardCreated, "msg", Clock.CreatedAt);

        Assert.Null(log.CardId);
        Assert.Equal(ActivityType.BoardCreated, log.Type);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    public void Create_WithNonPositiveBoardId_Throws(long boardId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => ActivityLog.Create(boardId, 1, ActivityType.CardCreated, "msg", Clock.CreatedAt));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidMessage_Throws(string? message)
    {
        Assert.Throws<ArgumentException>(() => ActivityLog.Create(1, 1, ActivityType.CardCreated, message!, Clock.CreatedAt));
    }
}
