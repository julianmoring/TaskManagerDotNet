using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface ICardSpecRepository
{
    Task<CardSpec?> GetLatestAsync(long cardId, CancellationToken ct = default);

    Task<IReadOnlyList<CardSpec>> ListVersionsAsync(long cardId, CancellationToken ct = default);

    Task<CardSpec?> GetByVersionAsync(long cardId, int version, CancellationToken ct = default);

    void Add(CardSpec spec);
}
