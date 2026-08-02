using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class OpenCodeSessionRepository : IOpenCodeSessionRepository
{
    private readonly TaskManagerDbContext _db;

    public OpenCodeSessionRepository(TaskManagerDbContext db) => _db = db;

    public Task<OpenCodeSession?> GetByIdAsync(long id, CancellationToken ct = default) =>
        _db.OpenCodeSessions.FirstOrDefaultAsync(s => s.Id == id, ct);

    public async Task<IReadOnlyList<OpenCodeSession>> ListByCardAsync(long cardId, CancellationToken ct = default) =>
        await _db.OpenCodeSessions
            .Where(s => s.CardId == cardId)
            .OrderByDescending(s => s.CreatedAt)
            .ThenByDescending(s => s.Id)
            .ToListAsync(ct);

    public void Add(OpenCodeSession session) => _db.OpenCodeSessions.Add(session);
}
