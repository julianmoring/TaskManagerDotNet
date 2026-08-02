using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Sessions;

public sealed record ListSessionsByCardQuery(long CardId);

public static class ListSessionsByCardHandler
{
    public static Task<IReadOnlyList<OpenCodeSessionDto>> HandleAsync(
        ListSessionsByCardQuery query,
        IOpenCodeSessionRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyList<OpenCodeSessionDto>>(Array.Empty<OpenCodeSessionDto>());
    }
}
