using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class ChecklistItemRepository : IChecklistItemRepository
{
    private readonly TaskManagerDbContext _db;

    public ChecklistItemRepository(TaskManagerDbContext db) => _db = db;

    public Task<ChecklistItem?> GetByIdAsync(long id, CancellationToken ct = default) =>
        _db.ChecklistItems.FirstOrDefaultAsync(i => i.Id == id, ct);

    public void Add(ChecklistItem item) => _db.ChecklistItems.Add(item);

    public void Remove(ChecklistItem item) => _db.ChecklistItems.Remove(item);
}
