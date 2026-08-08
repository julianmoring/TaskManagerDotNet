using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public sealed class BoardRepository : IBoardRepository
{
    private readonly TaskManagerDbContext _db;

    public BoardRepository(TaskManagerDbContext db) => _db = db;

    public Task<Board?> GetByIdAsync(long id, CancellationToken ct = default) =>
        _db.Boards.FirstOrDefaultAsync(b => b.Id == id, ct);

    public async Task<IReadOnlyList<Board>> ListAsync(CancellationToken ct = default) =>
        await _db.Boards.OrderBy(b => b.Id).ToListAsync(ct);

    public void Add(Board board) => _db.Boards.Add(board);

    public void Remove(Board board) => _db.Boards.Remove(board);
}
