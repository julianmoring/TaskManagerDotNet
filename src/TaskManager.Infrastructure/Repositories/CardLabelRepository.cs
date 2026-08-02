using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class CardLabelRepository : ICardLabelRepository
{
    private readonly TaskManagerDbContext _db;

    public CardLabelRepository(TaskManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<CardLabel>> ListByCardAsync(long cardId, CancellationToken ct = default) =>
        await _db.CardLabels
            .Where(cl => cl.CardId == cardId)
            .ToListAsync(ct);

    public void Add(CardLabel cardLabel) => _db.CardLabels.Add(cardLabel);

    public void Remove(CardLabel cardLabel) => _db.CardLabels.Remove(cardLabel);
}
