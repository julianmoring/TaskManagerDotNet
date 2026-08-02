using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Columns;

public sealed record RemoveColumnCommand(long BoardId, long ColumnId);

public static class RemoveColumnHandler
{
    public static Task HandleAsync(
        RemoveColumnCommand cmd,
        IBoardRepository boardRepo,
        IColumnRepository columnRepo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
