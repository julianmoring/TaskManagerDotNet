using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Labels;

public static class DetachLabelHandler
{
    public static Task HandleAsync(
        DetachLabelCommand cmd,
        ICardLabelRepository repo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
