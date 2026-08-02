using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class SessionEventRepository : ISessionEventRepository
{
    private readonly TaskManagerDbContext _db;

    public SessionEventRepository(TaskManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<SessionEvent>> ListBySessionAsync(long sessionId, CancellationToken ct = default) =>
        await _db.SessionEvents
            .Where(e => e.SessionId == sessionId)
            .OrderBy(e => e.Id)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<SessionEvent>> ListBySessionSinceAsync(long sessionId, long sinceEventId, CancellationToken ct = default) =>
        await _db.SessionEvents
            .Where(e => e.SessionId == sessionId && e.Id > sinceEventId)
            .OrderBy(e => e.Id)
            .ToListAsync(ct);

    public void Add(SessionEvent @event) => _db.SessionEvents.Add(@event);
}
