namespace TaskManager.Domain.DomainEvents;

public sealed record SpecVersionCreated(long BoardId, long CardId, int Version);
