using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Comments;

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
