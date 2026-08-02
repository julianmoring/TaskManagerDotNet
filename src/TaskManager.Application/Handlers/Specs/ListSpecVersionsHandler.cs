using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Specs;

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
