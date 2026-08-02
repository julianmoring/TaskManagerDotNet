using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Activity;

public sealed record ListActivityQuery(long BoardId, DateTimeOffset? Since);

public static class ListActivityHandler
{
    public static Task<IReadOnlyList<ActivityDto>> HandleAsync(
        ListActivityQuery query,
        IActivityRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyList<ActivityDto>>(Array.Empty<ActivityDto>());
    }
}
