namespace TaskManager.Domain.DomainEvents;

public sealed record CommentAdded(long BoardId, long CardId, long CommentId);
