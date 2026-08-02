using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Labels;

public sealed record RenameLabelCommand(long LabelId, string NewName);

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
