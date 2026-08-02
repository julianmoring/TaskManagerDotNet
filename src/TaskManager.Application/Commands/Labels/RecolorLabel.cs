using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Labels;

public sealed record RecolorLabelCommand(long LabelId, string NewColor);

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
