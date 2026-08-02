namespace TaskManager.Application.Commands;

public sealed record AddColumnCommand(long BoardId, string Name);

public sealed record RenameColumnCommand(long BoardId, long ColumnId, string NewName);

public sealed record RemoveColumnCommand(long BoardId, long ColumnId);

public sealed record MoveColumnCommand(long BoardId, long ColumnId, int NewPosition);
