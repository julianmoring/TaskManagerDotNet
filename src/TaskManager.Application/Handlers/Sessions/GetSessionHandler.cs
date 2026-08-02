using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Sessions;

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
