namespace TaskManager.Application.Dtos;

public sealed record BoardDto(
    long Id,
    string Name,
    string? Description,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt);

public sealed record CreateBoardResponse(long Id);
