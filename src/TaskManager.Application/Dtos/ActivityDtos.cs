using TaskManager.Domain.Enums;

namespace TaskManager.Application.Dtos;

public sealed record ActivityDto(long Id, long BoardId, long? CardId, ActivityType Type, string Message, DateTimeOffset CreatedAt);
