using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Cards;

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
