namespace TaskManager.Application.Queries;

public sealed record ListColumnsQuery(long BoardId);

public sealed record ListCardsByColumnQuery(long ColumnId);
