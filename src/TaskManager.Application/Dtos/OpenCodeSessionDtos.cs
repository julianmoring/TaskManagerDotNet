using TaskManager.Domain.Enums;

namespace TaskManager.Application.Dtos;

public sealed record OpenCodeSessionDto(
    long Id,
    long CardId,
    int SpecVersion,
    string WorkspacePath,
    SessionStatus Status,
    int? Pid,
    DateTimeOffset? StartedAt,
    DateTimeOffset? EndedAt,
    int? ExitCode,
    DateTimeOffset CreatedAt);

public sealed record SessionEventDto(long Id, long SessionId, EventKind Kind, string Text, DateTimeOffset At);

public sealed record StartSessionResponse(long SessionId);
