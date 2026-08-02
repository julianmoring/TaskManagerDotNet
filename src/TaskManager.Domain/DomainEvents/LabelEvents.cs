namespace TaskManager.Domain.DomainEvents;

public sealed record LabelCreated(long BoardId, long LabelId, string Name, string Color);

public sealed record LabelAttached(long BoardId, long CardId, long LabelId);

public sealed record LabelDetached(long BoardId, long CardId, long LabelId);
