using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Labels;

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
