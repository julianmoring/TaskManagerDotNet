using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Handlers.Boards;

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
