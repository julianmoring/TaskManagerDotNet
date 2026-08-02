using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Activity;

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
