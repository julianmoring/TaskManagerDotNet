using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface ICardLabelRepository
{
    Task<IReadOnlyList<CardLabel>> ListByCardAsync(long cardId, CancellationToken ct = default);

    void Add(CardLabel cardLabel);

    void Remove(CardLabel cardLabel);
}
