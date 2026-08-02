using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Sessions;

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
