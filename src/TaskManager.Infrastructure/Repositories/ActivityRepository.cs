using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class ActivityRepository : IActivityRepository
{
    private readonly TaskManagerDbContext _db;

    public ActivityRepository(TaskManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<ActivityLog>> ListByBoardAsync(long boardId, DateTimeOffset? since, CancellationToken ct = default)
    {
        var items = await _db.ActivityLogs
            .Where(a => a.BoardId == boardId)
            .ToListAsync(ct);
        var filtered = since.HasValue ? items.Where(a => a.CreatedAt >= since!.Value) : items;
        return filtered
            .OrderBy(a => a.CreatedAt)
            .ThenBy(a => a.Id)
            .ToList();
    }

    public void Add(ActivityLog log) => _db.ActivityLogs.Add(log);
}
