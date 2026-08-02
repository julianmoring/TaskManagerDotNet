using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Checklists;

public sealed record DeleteChecklistItemCommand(long ItemId);

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
