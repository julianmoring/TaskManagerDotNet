using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Sessions;

public static class ListSessionEventsHandler
{
    public static Task<IReadOnlyList<SessionEventDto>> HandleAsync(
        ListSessionEventsQuery query,
        ISessionEventRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyList<SessionEventDto>>(Array.Empty<SessionEventDto>());
    }
}
