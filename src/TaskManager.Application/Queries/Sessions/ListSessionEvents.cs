using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Sessions;

public sealed record ListSessionEventsQuery(long SessionId, long? SinceEventId);

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
