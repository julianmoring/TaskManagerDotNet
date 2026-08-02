using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Tests;

public sealed class SessionEventTests
{
    [Fact]
    public void Create_SetsAllProperties()
    {
        var evt = SessionEvent.Create(sessionId: 1, EventKind.StdOut, "line", Clock.CreatedAt);

        Assert.Equal(1L, evt.SessionId);
        Assert.Equal(EventKind.StdOut, evt.Kind);
        Assert.Equal("line", evt.Text);
        Assert.Equal(Clock.CreatedAt, evt.CreatedAt);
        Assert.Equal(0L, evt.Id);
    }

    [Fact]
    public void Create_WithEmptyText_Succeeds()
    {
        var evt = SessionEvent.Create(1, EventKind.StdOut, "", Clock.CreatedAt);

        Assert.Equal(string.Empty, evt.Text);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    public void Create_WithNonPositiveSessionId_Throws(long sessionId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => SessionEvent.Create(sessionId, EventKind.StdOut, "line", Clock.CreatedAt));
    }

    [Fact]
    public void Create_WithNullText_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => SessionEvent.Create(1, EventKind.StdOut, null!, Clock.CreatedAt));
    }
}
