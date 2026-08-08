using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class CardSpecRepository : ICardSpecRepository
{
    private readonly TaskManagerDbContext _db;

    public CardSpecRepository(TaskManagerDbContext db) => _db = db;

    public Task<CardSpec?> GetLatestAsync(long cardId, CancellationToken ct = default) =>
        _db.CardSpecs
            .Where(s => s.CardId == cardId)
            .OrderByDescending(s => s.Version)
            .FirstOrDefaultAsync(ct);

    public async Task<IReadOnlyList<CardSpec>> ListVersionsAsync(long cardId, CancellationToken ct = default) =>
        await _db.CardSpecs
            .Where(s => s.CardId == cardId)
            .OrderBy(s => s.Version)
            .ToListAsync(ct);

    public Task<CardSpec?> GetByVersionAsync(long cardId, int version, CancellationToken ct = default) =>
        _db.CardSpecs.FirstOrDefaultAsync(s => s.CardId == cardId && s.Version == version, ct);

    public void Add(CardSpec spec) => _db.CardSpecs.Add(spec);
}
