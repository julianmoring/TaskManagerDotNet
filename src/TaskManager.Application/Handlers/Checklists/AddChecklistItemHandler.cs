using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Checklists;

public static class AddChecklistItemHandler
{
    public static Task HandleAsync(
        AddChecklistItemCommand cmd,
        IChecklistRepository checklistRepo,
        IChecklistItemRepository itemRepo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
