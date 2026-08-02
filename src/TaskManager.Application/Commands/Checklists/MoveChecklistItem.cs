using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Checklists;

public sealed record MoveChecklistItemCommand(long ItemId, int NewPosition);

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
