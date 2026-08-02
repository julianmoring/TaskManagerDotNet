using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Cards;

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
