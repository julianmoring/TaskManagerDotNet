using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Columns;

public static class ListColumnsHandler
{
    public static Task<IReadOnlyList<ColumnDto>> HandleAsync(
        ListColumnsQuery query,
        IColumnRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyList<ColumnDto>>(Array.Empty<ColumnDto>());
    }
}
