using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface IChecklistItemRepository
{
    Task<ChecklistItem?> GetByIdAsync(long id, CancellationToken ct = default);

    void Add(ChecklistItem item);

    void Remove(ChecklistItem item);
}
