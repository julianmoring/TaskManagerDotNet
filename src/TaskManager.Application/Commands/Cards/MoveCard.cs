using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Cards;

public sealed record MoveCardCommand(long CardId, long ToColumnId, int NewPosition);

public static class MoveCardHandler
{
    public static Task HandleAsync(
        MoveCardCommand cmd,
        ICardRepository cardRepo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
