using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Boards;

public sealed record ListBoardsQuery();

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
