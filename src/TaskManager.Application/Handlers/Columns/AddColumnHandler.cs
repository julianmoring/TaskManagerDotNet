using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Handlers.Columns;

public static class AddColumnHandler
{
    public static Task<CreateColumnResponse> HandleAsync(
        AddColumnCommand cmd,
        IBoardRepository boardRepo,
        IColumnRepository columnRepo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.FromResult<CreateColumnResponse>(default!);
    }
}
