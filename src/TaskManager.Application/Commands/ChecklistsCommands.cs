namespace TaskManager.Application.Commands;

public sealed record CreateChecklistCommand(long CardId, string Title);

public sealed record RenameChecklistCommand(long ChecklistId, string NewTitle);

public sealed record DeleteChecklistCommand(long ChecklistId);

public sealed record AddChecklistItemCommand(long ChecklistId, string Text);

public sealed record UpdateChecklistItemCommand(long ItemId, string Text);

public sealed record ToggleChecklistItemCommand(long ItemId);

public sealed record MoveChecklistItemCommand(long ItemId, int NewPosition);

public sealed record DeleteChecklistItemCommand(long ItemId);
