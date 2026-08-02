namespace TaskManager.Application.Queries;

public sealed record ListLabelsByBoardQuery(long BoardId);

public sealed record ListChecklistsByCardQuery(long CardId);
