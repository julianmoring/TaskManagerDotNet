using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Specs;

public sealed record GetActiveSpecQuery(long CardId);

public static class GetActiveSpecHandler
{
    public static Task<CardSpecDto?> HandleAsync(
        GetActiveSpecQuery query,
        ICardSpecRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<CardSpecDto?>(null);
    }
}
