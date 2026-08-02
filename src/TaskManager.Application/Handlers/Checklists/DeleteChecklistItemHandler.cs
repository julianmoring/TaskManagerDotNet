using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Checklists;

public static class DeleteChecklistItemHandler
{
    public static Task HandleAsync(
        DeleteChecklistItemCommand cmd,
        IChecklistItemRepository repo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
