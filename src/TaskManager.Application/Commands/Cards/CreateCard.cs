using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Commands.Cards;

public sealed record CreateCardCommand(long ColumnId, string Title);

public static class CreateCardHandler
{
    public static Task<CreateCardResponse> HandleAsync(
        CreateCardCommand cmd,
        IColumnRepository columnRepo,
        ICardRepository cardRepo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.FromResult<CreateCardResponse>(default!);
    }
}
