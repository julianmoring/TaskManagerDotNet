namespace TaskManager.Domain.DomainEvents;

public sealed record BoardCreated(long BoardId, string Name);

public sealed record BoardRenamed(long BoardId, string OldName, string NewName);

public sealed record BoardDeleted(long BoardId);
