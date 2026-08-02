using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Cards;

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
