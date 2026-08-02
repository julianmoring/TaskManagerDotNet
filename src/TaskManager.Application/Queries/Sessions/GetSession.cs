using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Sessions;

public sealed record GetSessionQuery(long SessionId);

public static class GetSessionHandler
{
    public static Task<OpenCodeSessionDto?> HandleAsync(
        GetSessionQuery query,
        IOpenCodeSessionRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<OpenCodeSessionDto?>(null);
    }
}
