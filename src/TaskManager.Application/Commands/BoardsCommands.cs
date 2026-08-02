namespace TaskManager.Application.Commands;

public sealed record CreateBoardCommand(string Name, string? Description);

public sealed record RenameBoardCommand(long BoardId, string NewName);

public sealed record DeleteBoardCommand(long BoardId);
