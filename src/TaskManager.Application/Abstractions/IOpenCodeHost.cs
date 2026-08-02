using TaskManager.Domain.Enums;

namespace TaskManager.Application.Abstractions;

public interface IOpenCodeHost
{
    Task<StartedSession> StartAsync(StartSessionContext context, CancellationToken ct = default);

    Task StopAsync(long sessionId, CancellationToken ct = default);
}

public record StartSessionContext(
    long SessionId,
    long CardId,
    string CardTitle,
    string BoardName,
    string ColumnName,
    Priority Priority,
    DateTimeOffset? DueDate,
    int SpecVersion,
    string SpecBody,
    string WorkspacePath);

public record StartedSession(int Pid, DateTimeOffset StartedAt);
