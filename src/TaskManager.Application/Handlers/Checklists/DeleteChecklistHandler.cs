using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Checklists;

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
