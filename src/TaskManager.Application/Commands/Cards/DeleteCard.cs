using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Cards;

public sealed record DeleteCardCommand(long CardId);

public static class DeleteCardHandler
{
    public static Task HandleAsync(
        DeleteCardCommand cmd,
        ICardRepository cardRepo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
