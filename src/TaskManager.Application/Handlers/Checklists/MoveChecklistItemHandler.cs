using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Checklists;

public static class MoveChecklistItemHandler
{
    public static Task HandleAsync(
        MoveChecklistItemCommand cmd,
        IChecklistItemRepository repo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
