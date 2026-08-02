using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Checklists;

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
