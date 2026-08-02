using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Boards;

public static class DeleteBoardHandler
{
    public static Task HandleAsync(
        DeleteBoardCommand cmd,
        IBoardRepository repo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
