namespace TaskManager.Domain.DomainEvents;

public sealed record CardCreated(long BoardId, long ColumnId, long CardId, string Title);

public sealed record CardMoved(long BoardId, long CardId, long FromColumnId, long ToColumnId, int NewPosition);

public sealed record CardUpdated(long BoardId, long CardId);

public sealed record CardDeleted(long BoardId, long CardId);
