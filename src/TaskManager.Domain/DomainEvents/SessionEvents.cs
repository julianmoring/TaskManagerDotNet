namespace TaskManager.Domain.DomainEvents;

public sealed record SessionStarted(long BoardId, long CardId, long SessionId, int SpecVersion);

public sealed record SessionStopped(long BoardId, long CardId, long SessionId);

public sealed record SessionEnded(long BoardId, long CardId, long SessionId, int ExitCode);
