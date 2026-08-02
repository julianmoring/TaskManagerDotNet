using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public sealed class OpenCodeSession : Entity
{
    private readonly List<SessionEvent> _events = new();

    internal OpenCodeSession()
    {
    }

    private OpenCodeSession(long cardId, int specVersion, string specBodySnapshot, string workspacePath, DateTimeOffset createdAt)
    {
        CardId = cardId;
        SpecVersion = specVersion;
        SpecBodySnapshot = specBodySnapshot;
        WorkspacePath = workspacePath;
        Status = SessionStatus.Pending;
        CreatedAt = createdAt;
    }

    public long CardId { get; private set; }

    public int SpecVersion { get; private set; }

    public string SpecBodySnapshot { get; private set; } = string.Empty;

    public string WorkspacePath { get; private set; } = string.Empty;

    public SessionStatus Status { get; private set; }

    public int? Pid { get; private set; }

    public DateTimeOffset? StartedAt { get; private set; }

    public DateTimeOffset? EndedAt { get; private set; }

    public int? ExitCode { get; private set; }

    public IReadOnlyList<SessionEvent> Events => _events;

    public static OpenCodeSession Create(long cardId, int specVersion, string specBodySnapshot, string workspacePath, DateTimeOffset createdAt)
    {
        if (cardId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cardId), "CardId must be positive.");
        }

        if (specVersion < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(specVersion), "SpecVersion must be 1 or greater.");
        }

        if (string.IsNullOrWhiteSpace(specBodySnapshot))
        {
            throw new ArgumentException("SpecBodySnapshot is required.", nameof(specBodySnapshot));
        }

        if (string.IsNullOrWhiteSpace(workspacePath))
        {
            throw new ArgumentException("WorkspacePath is required.", nameof(workspacePath));
        }

        return new OpenCodeSession(cardId, specVersion, specBodySnapshot, workspacePath, createdAt);
    }

    public void MarkRunning(int pid, DateTimeOffset startedAt)
    {
        if (pid <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pid), "Pid must be positive.");
        }

        Status = SessionStatus.Running;
        Pid = pid;
        StartedAt = startedAt;
    }

    public void MarkEnded(int exitCode, DateTimeOffset endedAt)
    {
        Status = exitCode == 0 ? SessionStatus.Completed : SessionStatus.Failed;
        ExitCode = exitCode;
        EndedAt = endedAt;
    }

    public void MarkStopped(DateTimeOffset endedAt)
    {
        Status = SessionStatus.Stopped;
        EndedAt = endedAt;
    }
}
