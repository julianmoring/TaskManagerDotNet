using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface ILabelRepository
{
    Task<Label?> GetByIdAsync(long id, CancellationToken ct = default);

    Task<IReadOnlyList<Label>> ListByBoardAsync(long boardId, CancellationToken ct = default);

    void Add(Label label);

    void Remove(Label label);
}
