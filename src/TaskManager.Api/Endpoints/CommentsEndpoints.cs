using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands.Comments;
using TaskManager.Application.Dtos;
using Wolverine.Http;

namespace TaskManager.Api.Endpoints;

public static class CommentsEndpoints
{
    [WolverinePost("/api/cards/{cardId}/comments")]
    public static Task<AddCommentResponse> Add(
        long cardId,
        AddCommentCommand command,
        ICardRepository cardRepository,
        ICommentRepository commentRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => AddCommentHandler.HandleAsync(command with { CardId = cardId }, cardRepository, commentRepository, unitOfWork, clock, cancellationToken);

    [WolverineDelete("/api/comments/{commentId}")]
    public static Task Delete(
        long commentId,
        ICommentRepository commentRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => DeleteCommentHandler.HandleAsync(new DeleteCommentCommand(commentId), commentRepository, unitOfWork, cancellationToken);
}
