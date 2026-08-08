using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class CardRepository : ICardRepository
{
    private readonly TaskManagerDbContext _db;

    public CardRepository(TaskManagerDbContext db) => _db = db;

    public Task<Card?> GetByIdAsync(long id, bool withDetails, CancellationToken ct = default)
    {
        var query = _db.Cards.AsQueryable();
        if (withDetails)
        {
            query = query
                .Include(c => c.Checklists)
                    .ThenInclude(cl => cl.Items)
                .Include(c => c.Comments)
                .Include(c => c.Labels)
                .Include(c => c.Specs)
                .Include(c => c.Sessions);
        }

        return query.FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IReadOnlyList<Card>> ListByColumnAsync(long columnId, CancellationToken ct = default) =>
        await _db.Cards
            .Where(c => c.ColumnId == columnId)
            .OrderBy(c => c.Position)
            .ThenBy(c => c.Id)
            .ToListAsync(ct);

    public void Add(Card card) => _db.Cards.Add(card);

    public void Remove(Card card) => _db.Cards.Remove(card);
}
