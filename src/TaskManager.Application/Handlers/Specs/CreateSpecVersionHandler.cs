using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Handlers.Specs;

public static class CreateSpecVersionHandler
{
    public static Task<CreateSpecVersionResponse> HandleAsync(
        CreateSpecVersionCommand cmd,
        ICardRepository cardRepo,
        ICardSpecRepository specRepo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.FromResult<CreateSpecVersionResponse>(default!);
    }
}
