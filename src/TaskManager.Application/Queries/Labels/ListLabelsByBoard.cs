using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Labels;

public sealed record ListLabelsByBoardQuery(long BoardId);

public static class ListLabelsByBoardHandler
{
    public static Task<IReadOnlyList<LabelDto>> HandleAsync(
        ListLabelsByBoardQuery query,
        ILabelRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyList<LabelDto>>(Array.Empty<LabelDto>());
    }
}
