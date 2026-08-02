using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface IColumnRepository
{
    Task<Column?> GetByIdAsync(long id, CancellationToken ct = default);

    Task<IReadOnlyList<Column>> ListByBoardAsync(long boardId, CancellationToken ct = default);

    void Add(Column column);

    void Remove(Column column);
}
