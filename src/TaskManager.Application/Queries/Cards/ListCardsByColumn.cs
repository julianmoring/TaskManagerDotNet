using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Cards;

public sealed record ListCardsByColumnQuery(long ColumnId);

public static class ListCardsByColumnHandler
{
    public static Task<IReadOnlyList<CardDto>> HandleAsync(
        ListCardsByColumnQuery query,
        ICardRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyList<CardDto>>(Array.Empty<CardDto>());
    }
}
