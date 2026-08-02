namespace TaskManager.Application.Dtos;

public sealed record CardSpecDto(long Id, long CardId, int Version, string BodyMarkdown, DateTimeOffset CreatedAt);

public sealed record CardSpecSummaryDto(long Id, long CardId, int Version, DateTimeOffset CreatedAt);

public sealed record CreateSpecVersionResponse(int Version);
