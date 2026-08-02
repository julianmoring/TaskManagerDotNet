using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Boards;

public sealed record DeleteBoardCommand(long BoardId);

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
