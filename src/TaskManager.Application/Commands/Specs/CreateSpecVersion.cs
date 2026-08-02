using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Commands.Specs;

public sealed record CreateSpecVersionCommand(long CardId, string BodyMarkdown);

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
