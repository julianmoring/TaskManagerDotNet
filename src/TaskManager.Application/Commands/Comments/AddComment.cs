using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Commands.Comments;

public sealed record AddCommentCommand(long CardId, string Body);

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
