using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Cards;

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
