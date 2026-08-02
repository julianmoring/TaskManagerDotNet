using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Sessions;

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
