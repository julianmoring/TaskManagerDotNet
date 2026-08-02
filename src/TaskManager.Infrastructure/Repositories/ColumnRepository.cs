using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class ColumnRepository : IColumnRepository
{
    private readonly TaskManagerDbContext _db;

    public ColumnRepository(TaskManagerDbContext db) => _db = db;

    public Task<Column?> GetByIdAsync(long id, CancellationToken ct = default) =>
        _db.Columns.FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<IReadOnlyList<Column>> ListByBoardAsync(long boardId, CancellationToken ct = default) =>
        await _db.Columns
            .Where(c => c.BoardId == boardId)
            .OrderBy(c => c.Position)
            .ThenBy(c => c.Id)
            .ToListAsync(ct);

    public void Add(Column column) => _db.Columns.Add(column);

    public void Remove(Column column) => _db.Columns.Remove(column);
}
