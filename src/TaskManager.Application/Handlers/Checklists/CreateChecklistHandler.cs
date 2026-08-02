using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Handlers.Checklists;

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
