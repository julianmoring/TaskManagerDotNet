using TaskManager.Application.Abstractions;

namespace TaskManager.Application.Commands.Comments;

public sealed record DeleteCommentCommand(long CommentId);

public static class DeleteCommentHandler
{
    public static Task HandleAsync(
        DeleteCommentCommand cmd,
        ICommentRepository repo,
        IUnitOfWork uow,
        CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
