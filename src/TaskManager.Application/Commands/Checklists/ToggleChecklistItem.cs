using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Checklists;

public sealed record ToggleChecklistItemCommand(long ItemId);

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
