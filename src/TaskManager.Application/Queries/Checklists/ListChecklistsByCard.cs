using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Checklists;

public sealed record ListChecklistsByCardQuery(long CardId);

public static class ListChecklistsByCardHandler
{
    public static Task<IReadOnlyList<ChecklistDto>> HandleAsync(
        ListChecklistsByCardQuery query,
        IChecklistRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyList<ChecklistDto>>(Array.Empty<ChecklistDto>());
    }
}
