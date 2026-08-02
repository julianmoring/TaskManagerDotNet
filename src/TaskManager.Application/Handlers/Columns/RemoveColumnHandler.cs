using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Columns;

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
