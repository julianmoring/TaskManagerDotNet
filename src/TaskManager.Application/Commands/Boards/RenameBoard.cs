using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Boards;

public sealed record RenameBoardCommand(long BoardId, string NewName);

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
