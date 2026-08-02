using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface ICardRepository
{
    Task<Card?> GetByIdAsync(long id, bool withDetails, CancellationToken ct = default);

    Task<IReadOnlyList<Card>> ListByColumnAsync(long columnId, CancellationToken ct = default);

    void Add(Card card);

    void Remove(Card card);
}
