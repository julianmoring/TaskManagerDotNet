using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class LabelRepository : ILabelRepository
{
    private readonly TaskManagerDbContext _db;

    public LabelRepository(TaskManagerDbContext db) => _db = db;

    public Task<Label?> GetByIdAsync(long id, CancellationToken ct = default) =>
        _db.Labels.FirstOrDefaultAsync(l => l.Id == id, ct);

    public async Task<IReadOnlyList<Label>> ListByBoardAsync(long boardId, CancellationToken ct = default) =>
        await _db.Labels
            .Where(l => l.BoardId == boardId)
            .OrderBy(l => l.Name)
            .ToListAsync(ct);

    public void Add(Label label) => _db.Labels.Add(label);

    public void Remove(Label label) => _db.Labels.Remove(label);
}
