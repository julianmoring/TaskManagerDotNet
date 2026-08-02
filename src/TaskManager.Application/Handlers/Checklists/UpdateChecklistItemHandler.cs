using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Checklists;

public static class UpdateChecklistItemHandler
{
    public static Task HandleAsync(
        UpdateChecklistItemCommand cmd,
        IChecklistItemRepository repo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
