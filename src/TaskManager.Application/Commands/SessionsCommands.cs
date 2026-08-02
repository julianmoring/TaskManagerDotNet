namespace TaskManager.Application.Commands;

public sealed record StartSessionCommand(long CardId, string WorkspacePath);

public sealed record StopSessionCommand(long SessionId);
