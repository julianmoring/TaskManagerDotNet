using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Checklists;

public sealed record DeleteChecklistCommand(long ChecklistId);

public static class DeleteChecklistHandler
{
    public static Task HandleAsync(
        DeleteChecklistCommand cmd,
        IChecklistRepository repo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
