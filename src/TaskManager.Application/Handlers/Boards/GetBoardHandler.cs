using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Boards;

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
