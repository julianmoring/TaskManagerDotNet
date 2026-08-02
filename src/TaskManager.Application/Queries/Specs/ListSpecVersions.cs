using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Specs;

public sealed record ListSpecVersionsQuery(long CardId);

public static class ListSpecVersionsHandler
{
    public static Task<IReadOnlyList<CardSpecSummaryDto>> HandleAsync(
        ListSpecVersionsQuery query,
        ICardSpecRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyList<CardSpecSummaryDto>>(Array.Empty<CardSpecSummaryDto>());
    }
}
