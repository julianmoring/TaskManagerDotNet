using TaskManager.Application.Abstractions;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly TaskManagerDbContext _db;

    public UnitOfWork(TaskManagerDbContext db) => _db = db;

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
