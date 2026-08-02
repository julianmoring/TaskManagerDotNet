using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Labels;

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
