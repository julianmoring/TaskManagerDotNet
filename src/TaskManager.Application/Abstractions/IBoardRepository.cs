using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface IBoardRepository
{
    Task<Board?> GetByIdAsync(long id, CancellationToken ct = default);

    Task<IReadOnlyList<Board>> ListAsync(CancellationToken ct = default);

    void Add(Board board);

    void Remove(Board board);
}
