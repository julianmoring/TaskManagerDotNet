using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries.Activity;
using Wolverine.Http;

namespace TaskManager.Api.Endpoints;

public static class ActivityEndpoints
{
    [WolverineGet("/api/boards/{boardId}/activity")]
    public static Task<IReadOnlyList<ActivityDto>> List(
        long boardId,
        DateTimeOffset? since,
        IActivityRepository activityRepository,
        CancellationToken cancellationToken)
        => ListActivityHandler.HandleAsync(new ListActivityQuery(boardId, since), activityRepository, cancellationToken);
}
