using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Labels;

public static class RecolorLabelHandler
{
    public static Task HandleAsync(
        RecolorLabelCommand cmd,
        ILabelRepository repo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
