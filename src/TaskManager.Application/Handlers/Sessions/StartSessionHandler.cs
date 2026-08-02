using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Handlers.Sessions;

public static class StartSessionHandler
{
    public static Task<StartSessionResponse> HandleAsync(
        StartSessionCommand cmd,
        ICardRepository cardRepo,
        ICardSpecRepository specRepo,
        IOpenCodeSessionRepository sessionRepo,
        IOpenCodeHost host,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.FromResult<StartSessionResponse>(default!);
    }
}
