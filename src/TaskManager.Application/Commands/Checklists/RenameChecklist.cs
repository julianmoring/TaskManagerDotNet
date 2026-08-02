using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Checklists;

public sealed record RenameChecklistCommand(long ChecklistId, string NewTitle);

public static class RenameChecklistHandler
{
    public static Task HandleAsync(
        RenameChecklistCommand cmd,
        IChecklistRepository repo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
