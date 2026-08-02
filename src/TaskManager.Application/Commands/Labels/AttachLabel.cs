using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Labels;

public sealed record AttachLabelCommand(long CardId, long LabelId);

public static class AttachLabelHandler
{
    public static Task HandleAsync(
        AttachLabelCommand cmd,
        ICardRepository cardRepo,
        ILabelRepository labelRepo,
        ICardLabelRepository cardLabelRepo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
