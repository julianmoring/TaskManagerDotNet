namespace TaskManager.Application.Queries;

public sealed record ListSpecVersionsQuery(long CardId);

public sealed record GetActiveSpecQuery(long CardId);
