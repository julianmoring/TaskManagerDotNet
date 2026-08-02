using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Labels;

public sealed record DetachLabelCommand(long CardId, long LabelId);

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
