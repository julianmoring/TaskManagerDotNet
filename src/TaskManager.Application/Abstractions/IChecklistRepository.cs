using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface IChecklistRepository
{
    Task<Checklist?> GetByIdAsync(long id, CancellationToken ct = default);

    Task<IReadOnlyList<Checklist>> ListByCardAsync(long cardId, CancellationToken ct = default);

    void Add(Checklist checklist);

    void Remove(Checklist checklist);
}
