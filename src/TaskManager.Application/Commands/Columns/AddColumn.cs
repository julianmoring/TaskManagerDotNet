using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Commands.Columns;

public sealed record AddColumnCommand(long BoardId, string Name);

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
