using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Cards;

public static class UpdateCardHandler
{
    public static Task HandleAsync(
        UpdateCardCommand cmd,
        ICardRepository cardRepo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
