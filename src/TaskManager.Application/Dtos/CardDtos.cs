using TaskManager.Domain.Enums;

namespace TaskManager.Application.Dtos;

public sealed record CardDto(
    long Id,
    long ColumnId,
    string Title,
    string? Description,
    int Position,
    Priority Priority,
    DateTimeOffset? DueDate,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt,
    IReadOnlyList<LabelDto> Labels,
    IReadOnlyList<ChecklistDto> Checklists,
    int CommentCount,
    int SpecVersionCount,
    int SessionCount);

public sealed record CreateCardResponse(long Id);
