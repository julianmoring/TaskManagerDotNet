using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Tests;

public sealed class OpenCodeSessionTests
{
    [Fact]
    public void Create_SetsAllPropertiesAndDefaults()
    {
        var session = OpenCodeSession.Create(cardId: 1, specVersion: 1, "body", "/path", Clock.CreatedAt);

        Assert.Equal(1L, session.CardId);
        Assert.Equal(1, session.SpecVersion);
        Assert.Equal("body", session.SpecBodySnapshot);
        Assert.Equal("/path", session.WorkspacePath);
        Assert.Equal(SessionStatus.Pending, session.Status);
        Assert.Null(session.Pid);
        Assert.Null(session.StartedAt);
        Assert.Null(session.EndedAt);
        Assert.Null(session.ExitCode);
        Assert.Equal(Clock.CreatedAt, session.CreatedAt);
        Assert.Equal(0L, session.Id);
        Assert.Empty(session.Events);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    public void Create_WithNonPositiveCardId_Throws(long cardId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => OpenCodeSession.Create(cardId, 1, "body", "/path", Clock.CreatedAt));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Create_WithSpecVersionLessThanOne_Throws(int specVersion)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => OpenCodeSession.Create(1, specVersion, "body", "/path", Clock.CreatedAt));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidSpecBodySnapshot_Throws(string? body)
    {
        Assert.Throws<ArgumentException>(() => OpenCodeSession.Create(1, 1, body!, "/path", Clock.CreatedAt));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidWorkspacePath_Throws(string? path)
    {
        Assert.Throws<ArgumentException>(() => OpenCodeSession.Create(1, 1, "body", path!, Clock.CreatedAt));
    }

    [Fact]
    public void MarkRunning_SetsStatusPidAndStartedAt()
    {
        var session = OpenCodeSession.Create(1, 1, "body", "/path", Clock.CreatedAt);

        session.MarkRunning(pid: 1234, Clock.UpdatedAt);

        Assert.Equal(SessionStatus.Running, session.Status);
        Assert.Equal(1234, session.Pid);
        Assert.Equal(Clock.UpdatedAt, session.StartedAt);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void MarkRunning_WithNonPositivePid_Throws(int pid)
    {
        var session = OpenCodeSession.Create(1, 1, "body", "/path", Clock.CreatedAt);

        Assert.Throws<ArgumentOutOfRangeException>(() => session.MarkRunning(pid, Clock.UpdatedAt));
    }

    [Fact]
    public void MarkEnded_WithExitCodeZero_SetsStatusCompleted()
    {
        var session = OpenCodeSession.Create(1, 1, "body", "/path", Clock.CreatedAt);

        session.MarkEnded(exitCode: 0, Clock.UpdatedAt);

        Assert.Equal(SessionStatus.Completed, session.Status);
        Assert.Equal(0, session.ExitCode);
        Assert.Equal(Clock.UpdatedAt, session.EndedAt);
    }

    [Fact]
    public void MarkEnded_WithNonZeroExitCode_SetsStatusFailed()
    {
        var session = OpenCodeSession.Create(1, 1, "body", "/path", Clock.CreatedAt);

        session.MarkEnded(exitCode: 1, Clock.UpdatedAt);

        Assert.Equal(SessionStatus.Failed, session.Status);
        Assert.Equal(1, session.ExitCode);
        Assert.Equal(Clock.UpdatedAt, session.EndedAt);
    }

    [Fact]
    public void MarkStopped_SetsStatusStoppedAndEndedAt()
    {
        var session = OpenCodeSession.Create(1, 1, "body", "/path", Clock.CreatedAt);

        session.MarkStopped(Clock.UpdatedAt);

        Assert.Equal(SessionStatus.Stopped, session.Status);
        Assert.Equal(Clock.UpdatedAt, session.EndedAt);
    }
}
