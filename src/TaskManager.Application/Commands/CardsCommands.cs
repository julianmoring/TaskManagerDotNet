using TaskManager.Domain.Enums;

namespace TaskManager.Application.Commands;

public sealed record CreateCardCommand(long ColumnId, string Title);

public sealed record UpdateCardCommand(long CardId, string Title, string? Description, Priority Priority, DateTimeOffset? DueDate);

public sealed record MoveCardCommand(long CardId, long ToColumnId, int NewPosition);

public sealed record DeleteCardCommand(long CardId);
