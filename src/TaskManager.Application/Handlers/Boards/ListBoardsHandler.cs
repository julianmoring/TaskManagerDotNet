using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Boards;

public static class ListBoardsHandler
{
    public static Task<IReadOnlyList<BoardDto>> HandleAsync(
        ListBoardsQuery query,
        IBoardRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyList<BoardDto>>(Array.Empty<BoardDto>());
    }
}
