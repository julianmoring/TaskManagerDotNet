namespace TaskManager.Application.Dtos;

public sealed record ColumnDto(long Id, long BoardId, string Name, int Position);

public sealed record CreateColumnResponse(long Id);
