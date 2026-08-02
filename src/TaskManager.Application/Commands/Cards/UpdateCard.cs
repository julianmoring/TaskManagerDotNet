using TaskManager.Application.Abstractions;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Commands.Cards;

public sealed record UpdateCardCommand(long CardId, string Title, string? Description, Priority Priority, DateTimeOffset? DueDate);

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
