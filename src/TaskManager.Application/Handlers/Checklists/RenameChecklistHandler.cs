using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Checklists;

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
