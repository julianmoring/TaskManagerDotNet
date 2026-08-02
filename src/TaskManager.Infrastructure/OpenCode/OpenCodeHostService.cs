using TaskManager.Application.Abstractions;

namespace TaskManager.Infrastructure.OpenCode;

public sealed class OpenCodeHostService : IOpenCodeHost
{
    public Task<StartedSession> StartAsync(StartSessionContext context, CancellationToken ct = default) =>
        Task.FromResult(new StartedSession(Pid: 0, StartedAt: DateTimeOffset.UtcNow));

    public Task StopAsync(long sessionId, CancellationToken ct = default) => Task.CompletedTask;
}
