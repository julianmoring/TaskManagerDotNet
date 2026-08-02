namespace TaskManager.Application.Queries;

public sealed record ListActivityQuery(long BoardId, DateTimeOffset? Since);
