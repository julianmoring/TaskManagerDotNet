namespace TaskManager.Application.Dtos;

public sealed record LabelDto(long Id, long BoardId, string Name, string Color);

public sealed record CreateLabelResponse(long Id);
