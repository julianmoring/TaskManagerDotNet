using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Infrastructure.Services;

namespace TaskManager.Infrastructure.Tests;

public sealed class UnitOfWorkTests : IDisposable
{
    private readonly TestDb _db = new();

    public void Dispose() => _db.Dispose();

    [Fact]
    public async Task SaveChangesAsync_persists_entities()
    {
        var uow = new UnitOfWork(_db.Context);
        var boardRepo = new BoardRepository(_db.Context);
        var board = Board.Create("Work", "desc", TestClock.CreatedAt);
        boardRepo.Add(board);

        await uow.SaveChangesAsync();

        var fetched = await boardRepo.GetByIdAsync(board.Id);
        Assert.NotNull(fetched);
        Assert.Equal("Work", fetched!.Name);
    }
}
