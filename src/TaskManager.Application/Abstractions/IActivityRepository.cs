using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface IActivityRepository
{
    Task<IReadOnlyList<ActivityLog>> ListByBoardAsync(long boardId, DateTimeOffset? since, CancellationToken ct = default);

    void Add(ActivityLog log);
}
