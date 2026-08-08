using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class CommentRepository : ICommentRepository
{
    private readonly TaskManagerDbContext _db;

    public CommentRepository(TaskManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<Comment>> ListByCardAsync(long cardId, CancellationToken ct = default)
    {
        var items = await _db.Comments
            .Where(c => c.CardId == cardId)
            .ToListAsync(ct);
        return items
            .OrderBy(c => c.CreatedAt)
            .ThenBy(c => c.Id)
            .ToList();
    }

    public void Add(Comment comment) => _db.Comments.Add(comment);

    public void Remove(Comment comment) => _db.Comments.Remove(comment);
}
