using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Checklists;

public static class ToggleChecklistItemHandler
{
    public static Task HandleAsync(
        ToggleChecklistItemCommand cmd,
        IChecklistItemRepository repo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
