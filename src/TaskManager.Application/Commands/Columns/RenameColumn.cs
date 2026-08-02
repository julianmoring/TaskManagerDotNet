using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Columns;

public sealed record RenameColumnCommand(long BoardId, long ColumnId, string NewName);

public static class RenameColumnHandler
{
    public static Task HandleAsync(
        RenameColumnCommand cmd,
        IBoardRepository boardRepo,
        IColumnRepository columnRepo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
