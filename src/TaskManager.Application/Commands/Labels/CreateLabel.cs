using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Commands.Labels;

public sealed record CreateLabelCommand(long BoardId, string Name, string Color);

public static class CreateLabelHandler
{
    public static Task<CreateLabelResponse> HandleAsync(
        CreateLabelCommand cmd,
        IBoardRepository boardRepo,
        ILabelRepository labelRepo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.FromResult<CreateLabelResponse>(default!);
    }
}
