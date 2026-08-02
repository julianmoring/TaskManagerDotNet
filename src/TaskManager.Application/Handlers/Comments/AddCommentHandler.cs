using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Handlers.Comments;

public static class AddCommentHandler
{
    public static Task<AddCommentResponse> HandleAsync(
        AddCommentCommand cmd,
        ICardRepository cardRepo,
        ICommentRepository commentRepo,
        IUnitOfWork uow,
        IClock clock,
        CancellationToken ct)
    {
        return Task.FromResult<AddCommentResponse>(default!);
    }
}
