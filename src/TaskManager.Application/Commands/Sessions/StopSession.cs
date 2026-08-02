using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Sessions;

public sealed record StopSessionCommand(long SessionId);

public static class StopSessionHandler
{
    public static Task HandleAsync(
        StopSessionCommand cmd,
        IOpenCodeSessionRepository sessionRepo,
        IOpenCodeHost host,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
