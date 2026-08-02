namespace TaskManager.Domain.DomainEvents;

public sealed record ColumnAdded(long BoardId, long ColumnId, string Name);

public sealed record ColumnRenamed(long BoardId, long ColumnId, string NewName);

public sealed record ColumnRemoved(long BoardId, long ColumnId);
