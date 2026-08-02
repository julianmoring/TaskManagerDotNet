using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class ChecklistRepository : IChecklistRepository
{
    private readonly TaskManagerDbContext _db;

    public ChecklistRepository(TaskManagerDbContext db) => _db = db;

    public Task<Checklist?> GetByIdAsync(long id, CancellationToken ct = default) =>
        _db.Checklists
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<IReadOnlyList<Checklist>> ListByCardAsync(long cardId, CancellationToken ct = default) =>
        await _db.Checklists
            .Where(c => c.CardId == cardId)
            .Include(c => c.Items)
            .ToListAsync(ct);

    public void Add(Checklist checklist) => _db.Checklists.Add(checklist);

    public void Remove(Checklist checklist) => _db.Checklists.Remove(checklist);
}
