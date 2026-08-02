using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Boards;

public static class RenameBoardHandler
{
    public static Task HandleAsync(
        RenameBoardCommand cmd,
        IBoardRepository repo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
