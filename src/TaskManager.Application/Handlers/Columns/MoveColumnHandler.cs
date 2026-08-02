using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Columns;

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
