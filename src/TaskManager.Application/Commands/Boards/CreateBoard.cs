using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Commands.Boards;

public sealed record CreateBoardCommand(string Name, string? Description);

public static class CreateBoardHandler
{
    public static Task<CreateBoardResponse> HandleAsync(
        CreateBoardCommand cmd,
        IBoardRepository repo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.FromResult<CreateBoardResponse>(default!);
    }
}
