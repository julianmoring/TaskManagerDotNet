using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Commands.Checklists;

public sealed record CreateChecklistCommand(long CardId, string Title);

public static class CreateChecklistHandler
{
    public static Task<CreateChecklistResponse> HandleAsync(
        CreateChecklistCommand cmd,
        ICardRepository cardRepo,
        IChecklistRepository checklistRepo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.FromResult<CreateChecklistResponse>(default!);
    }
}
