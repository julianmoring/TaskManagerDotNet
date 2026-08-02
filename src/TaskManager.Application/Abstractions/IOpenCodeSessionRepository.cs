using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface IOpenCodeSessionRepository
{
    Task<OpenCodeSession?> GetByIdAsync(long id, CancellationToken ct = default);

    Task<IReadOnlyList<OpenCodeSession>> ListByCardAsync(long cardId, CancellationToken ct = default);

    void Add(OpenCodeSession session);
}
