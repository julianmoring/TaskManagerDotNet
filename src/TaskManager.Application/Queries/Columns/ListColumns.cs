using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Columns;

public sealed record ListColumnsQuery(long BoardId);

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
