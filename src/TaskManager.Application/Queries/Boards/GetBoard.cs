using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Queries.Boards;

public sealed record GetBoardQuery(long BoardId);

public static class GetBoardHandler
{
    public static Task<BoardDto?> HandleAsync(
        GetBoardQuery query,
        IBoardRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<BoardDto?>(null);
    }
}
