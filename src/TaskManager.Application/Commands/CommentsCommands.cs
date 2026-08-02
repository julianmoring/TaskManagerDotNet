namespace TaskManager.Application.Commands;

public sealed record AddCommentCommand(long CardId, string Body);

public sealed record DeleteCommentCommand(long CommentId);
