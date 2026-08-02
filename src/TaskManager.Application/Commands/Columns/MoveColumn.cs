using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Columns;

public sealed record MoveColumnCommand(long BoardId, long ColumnId, int NewPosition);

public static class MoveColumnHandler
{
    public static Task HandleAsync(
        MoveColumnCommand cmd,
        IBoardRepository boardRepo,
        IColumnRepository columnRepo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
