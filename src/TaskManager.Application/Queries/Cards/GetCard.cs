using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Cards;

public sealed record GetCardQuery(long CardId);

public static class GetCardHandler
{
    public static Task<CardDto?> HandleAsync(
        GetCardQuery query,
        ICardRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<CardDto?>(null);
    }
}
