using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Labels;

public static class RenameLabelHandler
{
    public static Task HandleAsync(
        RenameLabelCommand cmd,
        ILabelRepository repo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
