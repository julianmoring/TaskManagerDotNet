using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Columns;

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
