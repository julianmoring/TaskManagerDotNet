using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Checklists;

public sealed record AddChecklistItemCommand(long ChecklistId, string Text);

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
