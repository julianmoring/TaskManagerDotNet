using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface ISessionEventRepository
{
    Task<IReadOnlyList<SessionEvent>> ListBySessionAsync(long sessionId, CancellationToken ct = default);

    Task<IReadOnlyList<SessionEvent>> ListBySessionSinceAsync(long sessionId, long sinceEventId, CancellationToken ct = default);

    void Add(SessionEvent @event);
}
