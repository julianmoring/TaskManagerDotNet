using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Checklists;

public sealed record UpdateChecklistItemCommand(long ItemId, string Text);

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
