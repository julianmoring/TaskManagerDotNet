namespace TaskManager.Domain.DomainEvents;

public sealed record ChecklistCreated(long BoardId, long CardId, long ChecklistId, string Title);

public sealed record ChecklistItemToggled(long BoardId, long CardId, long ChecklistId, long ItemId, bool IsDone);
